using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using WaypointCreator.Properties;
using System.Text.RegularExpressions; // Regex
using System.Text;
using System.Net;
using System.IO;

namespace WaypointCreator
{
    class DBHandler
    {
        private static MySqlConnection conn;
        public static int Entry = 0;
        public static string Name = "";

        public static Dictionary<uint, string> GetCreatureTemplateEntryAndNameList()
        {
            if (!Settings.Default.UsingDB)
                return new Dictionary<uint, string>();

            conn = new MySqlConnection();
            conn.ConnectionString = "server=" + Properties.Settings.Default.host + "; port=" + Properties.Settings.Default.port + "; user id=" + Properties.Settings.Default.username + "; password=" + Properties.Settings.Default.password + "; database=" + Properties.Settings.Default.database;
            try
            {
                conn.Open();
                string sqltext = "SELECT `entry`, `name` FROM `creature_template`";
                MySqlCommand sql = new MySqlCommand(sqltext, conn);
                var dataReader = sql.ExecuteReader();
                CreatureTemplate.CreatureTemplateInfoDataTable.Load(dataReader);
            }
            catch (MySqlException myerror)
            {
                MessageBox.Show("Error Connecting to Database: " + myerror.Message, "Database Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return CreatureTemplate.CreatureTemplateInfoDataTable.AsEnumerable().ToDictionary<DataRow, uint, string>(row => Convert.ToUInt32(row[0]), row => row[1].ToString());
        }

        public static void LoadCreatureTemplateVMangos()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = "server=" + Properties.Settings.Default.host + "; port=" + Properties.Settings.Default.port + "; user id=" + Properties.Settings.Default.username + "; password=" + Properties.Settings.Default.password + "; database=" + Properties.Settings.Default.database;
            try
            {
                conn.Open();
                string sqltext = "SELECT * FROM `creature_template`";
                MySqlCommand sql = new MySqlCommand(sqltext, conn);
                var dataReader = sql.ExecuteReader();
                CreatureTemplate.CreatureTemplateDataTable.Load(dataReader);
            }
            catch (MySqlException myerror)
            {
                MessageBox.Show("Error Connecting to Database: " + myerror.Message, "Database Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static void LoadCreatureVMangos()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = "server=" + Properties.Settings.Default.host + "; port=" + Properties.Settings.Default.port + "; user id=" + Properties.Settings.Default.username + "; password=" + Properties.Settings.Default.password + "; database=" + Properties.Settings.Default.database;
            try
            {
                conn.Open();
                string sqltext = "SELECT * FROM `creature`";
                MySqlCommand sql = new MySqlCommand(sqltext, conn);
                var dataReader = sql.ExecuteReader();
                Creature.CreatureDataTable.Load(dataReader);
            }
            catch (MySqlException myerror)
            {
                MessageBox.Show("Error Connecting to Database: " + myerror.Message, "Database Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static void database_write(string sqlstring)
        {
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand myCommand = new MySqlCommand();
            conn.ConnectionString = "server=" + Properties.Settings.Default.host + "; port=" + Properties.Settings.Default.port + "; user id=" + Properties.Settings.Default.username + "; password=" + Properties.Settings.Default.password + "; database=" + Properties.Settings.Default.database;
            myCommand.Connection = conn;
            myCommand.CommandText = sqlstring;

            try
            {
                conn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException myerror)
            {
                MessageBox.Show("There was an error updating the database: " + myerror.Message, "Database Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static object database_conn(string sqlstring)
        {
            conn = new MySqlConnection();
            conn.ConnectionString = "server=" + Properties.Settings.Default.host + "; port=" + Properties.Settings.Default.port + "; user id=" + Properties.Settings.Default.username + "; password=" + Properties.Settings.Default.password + "; database=" + Properties.Settings.Default.database;
            try
            {
                conn.Open();
                MySqlCommand sql = new MySqlCommand(sqlstring, conn);
                DataSet ds = new DataSet();
                MySqlDataAdapter DataAdapter = new MySqlDataAdapter();
                DataAdapter.SelectCommand = sql;
                DataAdapter.Fill(ds, "table1");
                return ds;
            }
            catch (MySqlException myerror)
            {
                MessageBox.Show("Error Connecting to Database: " + myerror.Message, "Database Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return "";
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
