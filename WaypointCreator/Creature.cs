using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WaypointCreator
{
    class Creature
    {
        public static DataTable CreatureDataTable = new DataTable();

        public static DataRow[] GetCreatureInfo(uint id)
        {
            DataRow[] creature = CreatureDataTable.Select("id = '" + id + "'");
            return creature;
        }
    }
}
