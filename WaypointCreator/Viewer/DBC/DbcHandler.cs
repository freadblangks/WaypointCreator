#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using WaypointCreator.Viewer.Serialization;

#endregion

namespace WaypointCreator.Viewer.DBC
{
    public static class DbcHandler
    {
        #region Static Fields

        public static IDictionary<int, AreaTable> AreaTableByIdList = LoadAreaTableByIdList();

        public static IDictionary<int, Map> MapByIdList = LoadMapByIdList();

        public static IDictionary<int, GameObjectDisplayInfo> GameObjectDisplayInfoByIdList =
            LoadGameObjectDisplayInfoByIdList();

        #endregion

        #region Methods

        private static string GetData(string localResourceName)
        {
            string data;
            const string ResourceNameSpace = "WaypointCreator.Viewer.DBC.{0}";
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format(ResourceNameSpace, localResourceName)))
            {
                using (var reader = new StreamReader(stream))
                {
                    data = reader.ReadToEnd();
                }
            }

            return data;
        }

        private static Dictionary<int, AreaTable> LoadAreaTableByIdList()
        {
            return GetData("AreaTable.xml").FromXml<List<AreaTable>>().ToDictionary(x => x.Id, x => x);
        }

        private static Dictionary<int, GameObjectDisplayInfo> LoadGameObjectDisplayInfoByIdList()
        {
            return GetData("GameObjectDisplayInfo.xml").FromXml<List<GameObjectDisplayInfo>>().ToDictionary(x => x.Id, x => x);
        }

        private static Dictionary<int, Map> LoadMapByIdList()
        {
            var result = new Dictionary<int, Map>();

            var list = GetData("Map.xml").FromXml<List<Map>>();

            foreach (var group in list.GroupBy(_ => _.Id))
            {
                var item = group.FirstOrDefault();
                result.Add(item.Id, item);
            }

            return result;
        }

        #endregion
    }
}