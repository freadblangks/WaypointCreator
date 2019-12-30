using System.Data;
using System.Linq;

namespace WaypointCreator
{
    class CreatureTemplate
    {
        public static DataTable CreatureTemplateInfoDataTable = new DataTable();
        public static DataTable CreatureTemplateDataTable = new DataTable();

        public static DataRow[] GetEntryInfo(uint entry)
        {
            DataRow[] creature = CreatureTemplateDataTable.Select("entry = '" + entry + "'");
            return creature;
        }
    }
}
