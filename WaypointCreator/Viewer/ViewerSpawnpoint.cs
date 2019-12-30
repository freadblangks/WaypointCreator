#region

using System;

using Microsoft.DirectX.Direct3D;

#endregion

namespace WaypointCreator.Viewer
{
    public class ViewerSpawnpoint : ViewerObjectBase
    {
        #region Static Fields

        public static int DefaultColor = System.Drawing.Color.DarkOrange.ToArgb();

        #endregion

        #region Constructors and Destructors

        public ViewerSpawnpoint(VertexBuffer vbBuffer)
            : base(vbBuffer)
        {
            Color = DefaultColor;
            NumberOfVertexs = 2;
            Rotate = false;
        }

        #endregion

        #region Public Properties

        public string Guid { get; set; }

        public uint Index { get; set; }

        public Guid SpawnpointContainerGuid { get; set; }

        public TimeSpan Time { get; set; }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            //inverse x and y because viewer and textures works inverse
            return $"{Index} - X: {Position.Y} Y: {Position.X}"; // $"{Index} - X: {Position.Y} Y: {Position.X}";
        }

        #endregion
    }
}
