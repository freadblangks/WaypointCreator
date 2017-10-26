
#region

using System.Collections.Generic;
using System.Linq;

using MySql.Data.MySqlClient;

using WaypointCreator.Properties;

#endregion

namespace WaypointCreator.EntityFramework
{
    public static class DBHandler
    {
        #region Public Methods and Operators

        public static Dictionary<uint, string> GetCreatureTemplateEntryAndNameList()
        {
            if (!Settings.Default.UsingDB)
                return new Dictionary<uint, string>();

            using (var context = SetConfiguredConnection(new CoreDBEntities()))
            {
                return context.creature_template.Select(x => new { x.Entry, x.Name }).ToDictionary(x => (uint)x.Entry, x => x.Name);
            }
        }

        public static creature GetFirstCreatureByid(uint id)
        {
            if (!Settings.Default.UsingDB)
                return null;

            using (var context = SetConfiguredConnection(new CoreDBEntities()))
            {
                return context.creatures.FirstOrDefault(x => x.id == id);
            }
        }

        public static CoreDBEntities SetConfiguredConnection(CoreDBEntities context)
        {
            var builder = new MySqlConnectionStringBuilder(context.Database.Connection.ConnectionString)
                          {
                              UserID = Settings.Default.username,
                              Password = Settings.Default.password,
                              Database = Settings.Default.database,
                              IntegratedSecurity = false,
                              Server = Settings.Default.host,
                              Port = Settings.Default.port.ToUint()
                          };

            context.Database.Connection.ConnectionString = builder.ConnectionString;

            return context;
        }

        #endregion
    }
}