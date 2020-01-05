#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using WaypointCreator.Viewer.DBC;

#endregion

namespace WaypointCreator.Viewer
{
    public partial class ViewerForm : Form
    {
        #region Static Fields

        private static readonly int _textColor = Color.White.ToArgb();

        #endregion

        #region Fields

        private readonly IDictionary<string, MapArea> _areas = new Dictionary<string, MapArea>();

        private readonly List<MapTexture> _minimapCache = new List<MapTexture>();

        private Device _device;

        private Microsoft.DirectX.Direct3D.Font _font;

        private bool _resizing;

        private float _scale = 2f;

        private float _speed = 2f;

        private bool _updateInprogress;

        private bool _linkPaths = true;
        #endregion

        #region Constructors and Destructors

        public ViewerForm()
        {
            InitializeComponent();

            InitializeDxDevice();
        }

        #endregion

        #region Delegates

        public delegate void DelInitializeDxDevice();

        #endregion

        #region Public Properties

        public VertexBuffer VbMapTexture { get; set; }

        public VertexBuffer VbSpectator { get; set; }

        public VertexBuffer VbWaypoint { get; set; }

        public VertexBuffer VbSpawnpoint { get; set; }

        public ViewerSpectator ViewerSpectator { get; set; }

        public List<ViewerWaypoint> Waypoints { get; set; }

        public List<ViewerSpawnpoint> Spawnpoints { get; set; }

        #endregion 

        #region Public Methods and Operators

        public static void OnCreateVbSpectator(VertexBuffer buffer)
        {
            var coloredArray = (CustomVertex.PositionColored[])buffer.Lock(0, LockFlags.None);
            coloredArray[0].X = -2f;
            coloredArray[0].Y = -2f;
            coloredArray[0].Z = 0f;
            coloredArray[0].Color = ViewerSpectator.DefaultColor;
            coloredArray[1].X = 0f;
            coloredArray[1].Y = -1f;
            coloredArray[1].Z = 0f;
            coloredArray[1].Color = ViewerSpectator.DefaultColor;
            coloredArray[2].X = 0f;
            coloredArray[2].Y = 2f;
            coloredArray[2].Z = 0f;
            coloredArray[2].Color = ViewerSpectator.DefaultColor;
            coloredArray[3].X = 2f;
            coloredArray[3].Y = -2f;
            coloredArray[3].Z = 0f;
            coloredArray[3].Color = ViewerSpectator.DefaultColor;
            coloredArray[4].X = 0f;
            coloredArray[4].Y = -1f;
            coloredArray[4].Z = 0f;
            coloredArray[4].Color = ViewerSpectator.DefaultColor;
            coloredArray[5].X = 0f;
            coloredArray[5].Y = 2f;
            coloredArray[5].Z = 0f;
            coloredArray[5].Color = ViewerSpectator.DefaultColor;
            buffer.Unlock();
        }

        public static void OnResetDevice(Device device)
        {
            device.RenderState.CullMode = Cull.None;
            device.RenderState.Lighting = false;
            device.RenderState.ZBufferEnable = true;
            device.RenderState.AntiAliasedLineEnable = true;
        }

        public void AddWayPoints(List<ViewerWaypoint> waypoints)
        {
            Waypoints.AddRange(waypoints);

            UpdateViewer();
        }

        public void AddSpawnPoints(List<ViewerSpawnpoint> spawnpoints)
        {
            Spawnpoints.AddRange(spawnpoints);

            UpdateViewer();
        }

        public void BuildAreas()
        {
            _areas.Clear();
            var dirs = (new DirectoryInfo(".\\Minimap")).GetDirectories();

            foreach (var dir in dirs)
            {
                var maparea = new MapArea
                              {
                                  MapName = dir.Name
                              };
                /*
                #if DEBUG
                                var count = 0;
                                for (var x1 = 0; x1 < 64; x1++)
                                    for (var y1 = 0; y1 < 64; y1++)
                                        if (maparea.MapTextureArray[x1, y1] != null)
                                            count++;
                #endif
                */

                _areas.Add(dir.Name, maparea);

                foreach (
                    var file in dir.GetFiles("*.jpg").Where(_ => Regex.IsMatch(_.Name, @"map([0-9]+)_([0-9]+)\.jpg")))
                {
                    var array = Regex.Replace(file.Name, @"map([0-9]+)_([0-9]+)\.jpg", "$1:$2").Split(':');
                    if (array.Length > 0)
                    {
                        var x = int.Parse(array[0]);
                        var y = int.Parse(array[1]);
                        maparea.MapTextureArray[x, y] = new MapTexture { Filename = file.FullName };
                    }
                }
            }
        }

        public void ClearWayPoints()
        {
            Waypoints = new List<ViewerWaypoint>();

            Task.Factory.StartNew(UpdateAsync);
        }

        public void ClearSpawnPoints()
        {
            Spawnpoints = new List<ViewerSpawnpoint>();

            Task.Factory.StartNew(UpdateAsync);
        }

        public void DrawLine(ViewerObjectBase source, ViewerObjectBase dest)
        {
            var myLine = new Line(_device);

            Vector2[] vertices = { source.Vector2, dest.Vector2 };

            try
            {
                myLine.Width = 1;
                myLine.Draw(vertices, Color.Magenta);
            }
            catch
            {
            }
        }

        public void DrawObject(ViewerObjectBase obj)
        {
            var x = 2f * (2f / _scale);
            _device.Transform.World = Matrix.Scaling(x, x, x);

            if (obj.Rotate)
            {
                var transforms1 = _device.Transform;
                transforms1.World *= Matrix.RotationZ(6.283185f - obj.Orientation);
            }

            var transform = _device.Transform;

            transform.World *= Matrix.Translation(obj.Position);

            _device.RenderState.TextureFactor = obj.Color;
            var textureState = _device.TextureState[0];
            textureState.ColorOperation = TextureOperation.Modulate;
            textureState.ColorArgument0 = TextureArgument.TextureColor;
            textureState.ColorArgument1 = TextureArgument.TFactor;
            _device.VertexFormat = VertexFormats.Diffuse | VertexFormats.Position;
            _device.SetTexture(0, null);
            _device.SetStreamSource(0, obj.VbBuffer, 0);
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, obj.NumberOfVertexs);

            var v = Vector3.Project(new Vector3(0f, 0f, 0f), _device.Viewport, _device.Transform.Projection, _device.Transform.View, _device.Transform.World);
            obj.Vector2 = new Vector2(v.X, v.Y);
            _font.DrawText(null, obj.ToString(), (int)v.X + 10, (int)v.Y - 4, _textColor);
        }

        public void InitializeDxDevice()
        {
            try
            {
                _device = new Device
                (0,
                    DeviceType.Hardware,
                    this,
                    CreateFlags.SoftwareVertexProcessing,
                    new PresentParameters
                    {
                        Windowed = true,
                        SwapEffect = SwapEffect.Discard
                    });

                VbSpectator = new VertexBuffer
                (typeof(CustomVertex.PositionColored),
                    6,
                    _device,
                    Usage.None,
                    VertexFormats.Diffuse | VertexFormats.Position,
                    Pool.Default);
                VbSpectator.Created += OnCreateVbSpectator;
                OnCreateVbSpectator(VbSpectator);

                VbWaypoint = new VertexBuffer
                (typeof(CustomVertex.PositionColored),
                    6,
                    _device,
                    Usage.None,
                    VertexFormats.Diffuse | VertexFormats.Position,
                    Pool.Default);
                VbWaypoint.Created += OnCreateVbWaypoint;
                OnCreateVbWaypoint(VbWaypoint, null);

                VbSpawnpoint = new VertexBuffer
                (typeof(CustomVertex.PositionColored),
                    6,
                    _device,
                    Usage.None,
                    VertexFormats.Diffuse | VertexFormats.Position,
                    Pool.Default);
                VbSpawnpoint.Created += OnCreateVbSpawnpoint;
                OnCreateVbSpawnpoint(VbSpawnpoint, null);

                VbMapTexture = new VertexBuffer
                (typeof(CustomVertex.PositionNormalTextured),
                    6,
                    _device,
                    Usage.None,
                    VertexFormats.Texture1 | VertexFormats.PositionNormal,
                    Pool.Default);
                VbMapTexture.Created += OnCreateVbMapTexture;
                OnCreateVbMapTexture(VbMapTexture);

                _font = new Microsoft.DirectX.Direct3D.Font(_device, new System.Drawing.Font("arial", 8f));

                _device.DeviceReset += OnResetDevice;
                OnResetDevice(_device);
            }
            catch (DirectXException exc)
            {
                OnException(exc);
                Close();
            }
        }

        public void OnCreateVbMapTexture(object sender, EventArgs e)
        {
            OnCreateVbMapTexture(sender as VertexBuffer);
        }

        public void OnCreateVbSpectator(object sender, EventArgs e)
        {
            OnCreateVbSpectator(sender as VertexBuffer);
        }

        public void OnCreateVbWaypoint(object sender, EventArgs e)
        {
            var buffer = sender as VertexBuffer;

            var coloredArray = (CustomVertex.PositionColored[])buffer.Lock(0, LockFlags.None);
            coloredArray[0].X = -1f;
            coloredArray[0].Y = -1f;
            coloredArray[0].Z = 0f;
            coloredArray[0].Color = ViewerWaypoint.DefaultColor;
            coloredArray[1].X = 1f;
            coloredArray[1].Y = -1f;
            coloredArray[1].Z = 0f;
            coloredArray[1].Color = ViewerWaypoint.DefaultColor;
            coloredArray[2].X = -1f;
            coloredArray[2].Y = 1f;
            coloredArray[2].Z = 0f;
            coloredArray[2].Color = ViewerWaypoint.DefaultColor;
            coloredArray[3].X = 1f;
            coloredArray[3].Y = 1f;
            coloredArray[3].Z = 0f;
            coloredArray[3].Color = ViewerWaypoint.DefaultColor;
            coloredArray[4].X = 1f;
            coloredArray[4].Y = -1f;
            coloredArray[4].Z = 0f;
            coloredArray[4].Color = ViewerWaypoint.DefaultColor;
            coloredArray[5].X = -1f;
            coloredArray[5].Y = 1f;
            coloredArray[5].Z = 0f;
            coloredArray[5].Color = ViewerWaypoint.DefaultColor;
            buffer.Unlock();
        }

        public void OnCreateVbSpawnpoint(object sender, EventArgs e)
        {
            var buffer = sender as VertexBuffer;

            var coloredArray = (CustomVertex.PositionColored[])buffer.Lock(0, LockFlags.None);
            coloredArray[0].X = -1f;
            coloredArray[0].Y = -1f;
            coloredArray[0].Z = 0f;
            coloredArray[0].Color = ViewerSpawnpoint.DefaultNPCColor;
            coloredArray[1].X = 1f;
            coloredArray[1].Y = -1f;
            coloredArray[1].Z = 0f;
            coloredArray[1].Color = ViewerSpawnpoint.DefaultNPCColor;
            coloredArray[2].X = -1f;
            coloredArray[2].Y = 1f;
            coloredArray[2].Z = 0f;
            coloredArray[2].Color = ViewerSpawnpoint.DefaultNPCColor;
            coloredArray[3].X = 1f;
            coloredArray[3].Y = 1f;
            coloredArray[3].Z = 0f;
            coloredArray[3].Color = ViewerSpawnpoint.DefaultNPCColor;
            coloredArray[4].X = 1f;
            coloredArray[4].Y = -1f;
            coloredArray[4].Z = 0f;
            coloredArray[4].Color = ViewerSpawnpoint.DefaultNPCColor;
            coloredArray[5].X = -1f;
            coloredArray[5].Y = 1f;
            coloredArray[5].Z = 0f;
            coloredArray[5].Color = ViewerSpawnpoint.DefaultNPCColor;
            buffer.Unlock();
        }

        public void OnResetDevice(object sender, EventArgs e)
        {
            OnResetDevice(sender as Device);
        }

        public void UpdateViewer()
        {
            Task.Factory.StartNew(UpdateAsync);
        }

        #endregion

        #region Methods

        private Vector3 ComputeNormal(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            return Vector3.Normalize(Vector3.Cross(Vector3.Subtract(v2, v1), Vector3.Subtract(v3, v1)));
        }

        private void DrawTextures()
        {
            try
            {
                _minimapCache.ForEach(_ => _.Cache = false);

                var mapName = DbcHandler.MapByIdList[ViewerSpectator.MapId].Name;

                if (!string.IsNullOrEmpty(mapName))
                {
                    var scaleX = ((Constants.MapSize / 2f) - ViewerSpectator.Position.X) + Constants.TileSize;
                    var scaleY = ((Constants.MapSize / 2f) - ViewerSpectator.Position.Y) + Constants.TileSize;

                    var num3 = ((int)(scaleX / Constants.TileSize)) - 1;
                    var num4 = ((int)(scaleY / Constants.TileSize)) - 1;

                    for (var i = num3 - 64; i <= (num3 + 64); i++)
                    {
                        for (var j = num4 - 64; j <= (num4 + 64); j++)
                        {
                            var current = LoadMapTexture(i, j, mapName);
                            if (current != null)
                            {
                                current.Cache = true;
                                if (current.Texture != null)
                                {
                                    var x = -((i * Constants.TileSize) - (Constants.MapSize / 2f)) - (Constants.TileSize / 2f);
                                    var y = -((j * Constants.TileSize) - (Constants.MapSize / 2f)) - (Constants.TileSize / 2f);

                                    _device.Transform.World = (Matrix.RotationX(Constants.Rotation) *
                                                               Matrix.RotationY(Constants.Rotation)) *
                                                              Matrix.Translation(x, y, -1000f);

                                    _device.SetTexture(0, current.Texture);
                                    var textureState = _device.TextureState[0];
                                    textureState.ColorOperation = TextureOperation.Modulate;
                                    textureState.ColorArgument1 = TextureArgument.TextureColor;
                                    textureState.ColorArgument2 = TextureArgument.Diffuse;
                                    textureState.AlphaOperation = TextureOperation.Disable;
                                    _device.VertexFormat = VertexFormats.Texture1 | VertexFormats.PositionNormal;
                                    _device.SetStreamSource(0, VbMapTexture, 0);
                                    _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnException(ex);
                Close();
            }

            foreach (var minimap in _minimapCache.Where(x => x.Cache == false && x.Texture != null))
            {
                minimap.Texture.Dispose();
                minimap.Texture = null;
            }
        }

        private void LoadMapTexture(MapTexture mapTexture)
        {
            if (File.Exists(mapTexture.Filename))
            {
                try
                {
                    using (
                        var imageStreamSource = new FileStream
                        (mapTexture.Filename,
                            FileMode.Open,
                            FileAccess.Read,
                            FileShare.Read))
                    {
                        mapTexture.Texture = TextureLoader.FromStream(_device, imageStreamSource);
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        private MapTexture LoadMapTexture(int x, int y, string mapName)
        {
            MapTexture mapTexture;

            if ((((x > 0x3f) || (y > 0x3f)) || (x < 0)) || (y < 0) || string.IsNullOrEmpty(mapName))
            {
                mapTexture = null;
            }
            else
            {
                MapArea mapArea;
                if (_areas.TryGetValue(mapName, out mapArea))
                {
                    mapTexture = mapArea.MapTextureArray[x, y];

                    if ((mapTexture != null) && (mapTexture.Texture == null))
                    {
                        LoadMapTexture(mapTexture);
                    }
                }
                else
                {
                    mapTexture = null;
                }
            }

            return mapTexture;
        }

        private void OnCreateVbMapTexture(VertexBuffer buffer)
        {
            var texturedArray =
                (CustomVertex.PositionNormalTextured[])buffer.Lock(0, LockFlags.None);
            var vector = new Vector3(-(Constants.TileSize / 2f), -(Constants.TileSize / 2f), 0f);
            var vector2 = new Vector3(Constants.TileSize / 2f, -(Constants.TileSize / 2f), 0f);
            var vector3 = new Vector3(-(Constants.TileSize / 2f), Constants.TileSize / 2f, 0f);
            var nor = ComputeNormal(vector, vector2, vector3);
            texturedArray[0] = new CustomVertex.PositionNormalTextured(vector, nor, 0f, 0f);
            texturedArray[1] = new CustomVertex.PositionNormalTextured(vector2, nor, 1f, 0f);
            texturedArray[2] = new CustomVertex.PositionNormalTextured(vector3, nor, 0f, 1f);
            vector = new Vector3(Constants.TileSize / 2f, Constants.TileSize / 2f, 0f);
            vector2 = new Vector3(-(Constants.TileSize / 2f), Constants.TileSize / 2f, 0f);
            vector3 = new Vector3(Constants.TileSize / 2f, -(Constants.TileSize / 2f), 0f);
            nor = ComputeNormal(vector, vector2, vector3);
            texturedArray[3] = new CustomVertex.PositionNormalTextured(vector, nor, 1f, 1f);
            texturedArray[4] = new CustomVertex.PositionNormalTextured(vector2, nor, 0f, 1f);
            texturedArray[5] = new CustomVertex.PositionNormalTextured(vector3, nor, 1f, 0f);
            buffer.Unlock();
        }

        private void OnException(Exception exc)
        {
            // MessageBox.Show(exc.ToString());
        }

        private void OnSelectMap_Click(object sender, EventArgs e)
        {
            foreach (var item in selectMapToolStripMenuItem.DropDownItems)
            {
                var stripMenuItem = item as ToolStripMenuItem;
                if (stripMenuItem != null) stripMenuItem.Checked = false;
            }

            var toolStripMenuItem = sender as ToolStripMenuItem;

            if (toolStripMenuItem != null)
            {
                toolStripMenuItem.Checked = true;
                ViewerSpectator.MapId = (int)toolStripMenuItem.Tag;

                UpdateViewer();
            }
        }

        private void selectMapToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (selectMapToolStripMenuItem.DropDownItems.Count != 0)
            {
                return;
            }

            foreach (var map in DbcHandler.MapByIdList.Distinct())
            {
                var toolstripitem =
                    selectMapToolStripMenuItem.DropDownItems.Add($"({map.Value.Id}) {map.Value.Name} [{map.Value.LongName}]", null, OnSelectMap_Click) as ToolStripMenuItem;
                if (toolstripitem != null)
                {
                    toolstripitem.Tag = map.Key;

                    toolstripitem.Checked = map.Key == ViewerSpectator.MapId;
                }
            }
        }

        private void UpdateAsync()
        {
            if (_updateInprogress)
                return;

            _updateInprogress = true;
            if (_resizing)
            {
                return;
            }

            if (_device == null)
                return;

            try
            {
                lock (_device)
                {
                    _device.Clear(ClearFlags.Target, Color.Black, 1f, 0);

                    const float ValueZ = 100000f;
                    var cameraPosition = new Vector3(0f, 0f, ValueZ);
                    var cameraTarget = new Vector3(0f, 0f, 0f);
                    var cameraUpVector = new Vector3(0f, -1f, 0f);

                    var sourceMatrix = Matrix.RotationY(Constants.Rotation) * Matrix.RotationX(Constants.Rotation);

                    cameraPosition.TransformCoordinate(sourceMatrix);
                    cameraTarget.TransformCoordinate(sourceMatrix);
                    cameraUpVector.TransformCoordinate(sourceMatrix);
                    cameraTarget.Add(ViewerSpectator.Position);
                    cameraPosition.Add(ViewerSpectator.Position);

                    _device.Transform.View = Matrix.LookAtLH(cameraPosition, cameraTarget, cameraUpVector);
                    _device.Transform.Projection = Matrix.PerspectiveLH
                    (
                        Width / (ValueZ * _scale),
                        Height / (ValueZ * _scale),
                        1f,
                        1000000f);

                    _device.BeginScene();

                    DrawTextures();

                    // Add spawn points to visual
                    foreach (var waypoint in Waypoints)
                    {
                        DrawObject(waypoint);
                    }

                    // Add spawn points to visual
                    foreach (var spawnpoint in Spawnpoints)
                    {
                        DrawObject(spawnpoint);
                    }

                    DrawObject(ViewerSpectator);

                    if (_linkPaths)
                    {
                        foreach (var group in Waypoints.GroupBy(_ => _.Guid))
                        {
                            foreach (var from in group.OrderBy(x => x.Time).ThenBy(x => x.Index))
                            {
                                var to = group.FirstOrDefault(_ => _.Index > from.Index);

                                if (to == null)
                                    break;

                                DrawLine(from, to);
                            }
                        }
                    }

                    _device.EndScene();

                    _device.Present();
                }
            }
            catch (Exception ex)
            {
                OnException(ex);

                if (InvokeRequired)
                {
                    Invoke(new Action(Close));
                }
            }

            _updateInprogress = false;
        }

        private void ViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (var minimap in _minimapCache.Where(x => x.Texture != null))
                {
                    minimap.Texture.Dispose();
                    minimap.Texture = null;
                }

                ViewerSpectator = null;
                Waypoints.Clear();
                Spawnpoints.Clear();

                VbSpectator.Created -= OnCreateVbSpectator;
                VbSpectator.Dispose();
                VbSpectator = null;
                VbWaypoint.Created -= OnCreateVbWaypoint;
                VbWaypoint.Dispose();
                VbWaypoint = null;
                VbSpawnpoint.Created -= OnCreateVbSpawnpoint;
                VbSpawnpoint.Dispose();
                VbSpawnpoint = null;
                VbMapTexture.Created -= OnCreateVbMapTexture;
                VbMapTexture.Dispose();
                VbMapTexture = null;

                _device.Dispose();
            }
            catch 
            {
            }
        }

        private void ViewerForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.PageUp:
                    case Keys.PageDown:
                        ViewerSpectator.Move(e.KeyCode, _speed, UpdateAsync);
                        break;
                }
            }
            catch (Exception exc)
            {
                OnException(exc);
            }
        }

        private void ViewerForm_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        Close();
                        break;
                    case Keys.F2:
                        ZoomIn();

                        break;
                    case Keys.F3:
                        ZoomOut();

                        break;

                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.PageUp:
                    case Keys.PageDown:
                        ViewerSpectator.Move(e.KeyCode, _speed, UpdateAsync);
                        break;
                }
            }
            catch (Exception exc)
            {
                OnException(exc);
            }
        }

        private void ViewerForm_Load(object sender, EventArgs e)
        {
            ViewerSpectator = new ViewerSpectator(VbSpectator);

            Waypoints = new List<ViewerWaypoint>();
            Spawnpoints = new List<ViewerSpawnpoint>();

            BuildAreas();

            UpdateViewer();
        }

        private void ViewerForm_Resize(object sender, EventArgs e)
        {
            if (_device == null)
                return;
            lock (_device)
            {
                _resizing = false;
            }
        }

        private void ViewerForm_ResizeBegin(object sender, EventArgs e)
        {
            if (_device == null)
                return;
            lock (_device)
            {
                _resizing = true;
            }
        }

        private void ViewerForm_ResizeEnd(object sender, EventArgs e)
        {
            if (_device == null)
                return;

            lock (_device)
            {
                _resizing = false;
            }

            Task.Factory.StartNew(UpdateAsync);
        }

        private void ViewerForm_SizeChanged(object sender, EventArgs e)
        {
            UpdateViewer();
        }

        private void ZoomIn()
        {
            if (_device == null)
                return;

            lock (_device)
            {
                _scale *= 0.9f;
                _speed *= 1.1f;
            }

            UpdateViewer();
        }

        private void ZoomOut()
        {
            if (_device == null)
                return;

            lock (_device)
            {
                _scale *= 1.1f;
                _speed *= 0.9f;
            }

            UpdateViewer();
        }

        #endregion

        private void linkPathsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void linkPathsToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            _linkPaths = linkPathsToolStripMenuItem.Checked;
            Task.Factory.StartNew(UpdateAsync);
        }
    }
}