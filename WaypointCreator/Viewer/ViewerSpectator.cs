#region

using System;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

#endregion

namespace WaypointCreator.Viewer
{
    public class ViewerSpectator : ViewerObjectBase
    {
        #region Static Fields

        public static int DefaultColor = System.Drawing.Color.White.ToArgb();

        #endregion

        #region Fields

        private int _mapId;

        #endregion

        #region Constructors and Destructors

        public ViewerSpectator(VertexBuffer vbBuffer)
            : base(vbBuffer)
        {
            _mapId = 1;
            Color = DefaultColor;
            NumberOfVertexs = 2;
            Rotate = true;
        }

        #endregion

        #region Public Properties

        public int MapId
        {
            get
            {
                return _mapId;
            }
            set
            {
                if (_mapId != value)
                    ResetPosition = true;
                _mapId = value;
            }
        }

        public bool ResetPosition { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Move(Keys keyCode, float scale, Action update = null)
        {
            var speed = 5f * scale;
            switch (keyCode)
            {
                case Keys.Up:
                    Position = new Vector3(Position.X, Position.Y + speed, Position.X);
                    Orientation = 0f;
                    break;
                case Keys.Down:
                    Position = new Vector3(Position.X, Position.Y - speed, Position.X);
                    Orientation = (float)Math.PI;
                    break;
                case Keys.Left:
                    Position = new Vector3(Position.X + speed, Position.Y, Position.X);
                    Orientation = (float)Math.PI / 2f;
                    break;
                case Keys.Right:
                    Position = new Vector3(Position.X - speed, Position.Y, Position.X);
                    Orientation = (float)Math.PI * 2f - (float)Math.PI / 2f;
                    break;
                case Keys.PageUp:
                    Position = new Vector3(Position.X, Position.Y, Position.Z + speed);
                    Orientation = (float)Math.PI / 2f;
                    break;
                case Keys.PageDown:
                    Position = new Vector3(Position.X, Position.Y, Position.Z - speed);
                    Orientation = (float)Math.PI * 2f - (float)Math.PI / 2f;
                    break;
            }

            update?.Invoke();
        }

        public override string ToString()
        {
            return "Spectator";
        }

        #endregion
    }
}