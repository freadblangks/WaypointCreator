#region

using System;

#endregion

namespace WaypointCreator.Viewer
{
    public class MapArea
    {
        #region Fields

        public string MapName = String.Empty;

        public MapTexture[,] MapTextureArray = new MapTexture[0x40, 0x40];
 
        #endregion
    }
}