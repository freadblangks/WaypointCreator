#region

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

#endregion

namespace WaypointCreator.Viewer
{
    public abstract class ViewerObjectBase
    {
        #region Constructors and Destructors

        protected ViewerObjectBase(VertexBuffer vbBuffer)
        {
            VbBuffer = vbBuffer;
        }

        #endregion

        #region Public Properties

        public int Color { get; set; }

        public int NumberOfVertexs { get; set; }

        public float Orientation { get; set; }

        public Vector3 Position { get; set; }

        public bool Rotate { get; set; }

        public VertexBuffer VbBuffer { get; set; }

        public Vector2 Vector2 { get; set; }

        #endregion
    }
}