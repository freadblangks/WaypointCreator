#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.DirectX;

using Newtonsoft.Json;

using WaypointCreator.EntityFramework;
using WaypointCreator.Properties;
using WaypointCreator.Viewer;

#endregion

namespace WaypointCreator
{
    public partial class WaypointForm : Form
    {
        #region Fields

        private readonly Dictionary<uint, string> _creatureTemplateEntryAndNameList = DBHandler.GetCreatureTemplateEntryAndNameList();

        private bool _suspectUpdates;

        private ViewerForm _viewerForm;

        private SortableBindingList<WaypointContainer> _waypointContainers = new SortableBindingList<WaypointContainer>();

        #endregion

        #region Constructors and Destructors

        public WaypointForm()
        {
            InitializeComponent();

            WaypointContainerGridView.AutoGenerateColumns = false;
            WaypointGridView.AutoGenerateColumns = false;
        }

        #endregion

        #region Public Methods and Operators

        public void MergeSelection(bool unique)
        {
            if (WaypointContainerGridView.SelectedRows.Count > 0)
            {
                var items = WaypointContainerGridView.SelectedRows.Cast<DataGridViewRow>().Select(x => x.DataBoundItem as WaypointContainer);

                var groups = items.Where(x => x != null).OrderBy(x => x.Index).GroupBy(x => x.GUID + x.InCombat.ToString() + x.WaypointSouceType.ToString()).ToArray(); /*_waypointContainers.Where(x => x.Selected)*/

                foreach (var group in groups)
                {
                    var first = group.First();

                    var container = new WaypointContainer
                                    {
                                        Index = first.Index,
                                        Entry = first.Entry,
                                        GUID = first.GUID,
                                        Name = first.Name,
                                        WaypointSouceType = first.WaypointSouceType,
                                        InCombat = first.InCombat

                                        //Selected = true
                                    };

                    var index = 0;

                    var temp = group.SelectMany(x => x.Waypoints).OrderBy(x => x.Time).ThenBy(x => x.Index);

                    var subgroup = unique ? temp.GroupBy(x => x.X.ToString() + x.Y.ToString() + x.Z.ToString()) : temp.GroupBy(x => x.WaypointGuid.ToString());

                    foreach (var subGroup in subgroup)
                    {
                        var waypoint = subGroup.First();

                        waypoint.Index = (uint)container.Waypoints.Count + 1;
                        container.Waypoints.Add(waypoint);
                    }

                    _waypointContainers.Add(container);

                    foreach (var item in group)
                        _waypointContainers.Remove(item);
                }

                UpdateWaypointContainerGridView();
            }
        }

        #endregion

        #region Methods

        private void ConsolidateWaypointContainers(List<WaypointContainer> list)
        {
            var groups = list.GroupBy(x => x.GUID + x.InCombat.ToString() + x.WaypointSouceType.ToString()).ToArray();
            if (InvokeRequired)
            {
                Invoke
                (new Action
                (() =>
                {
                    toolStripStatusLabel.Text = "Start Consolidating...";
                    toolStripProgressBar1.ProgressBar.Maximum = groups.Length;
                    toolStripProgressBar1.ProgressBar.Minimum = 0;
                }));
            }

            for (var i = 0; i < groups.Length; i++)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => { toolStripProgressBar1.ProgressBar.Value = i; }));
                }
                var group = groups[i];

                foreach (var item in group.ToArray())
                {
                    if (item == null)
                        continue;

                    foreach (var item2 in group.ToArray())
                    {
                        if (item2 == null)
                            continue;

                        if (item != item2 && item.Waypoints.Count == item2.Waypoints.Count)
                        {
                            var equal = true;

                            for (var j = 0; j < item.Waypoints.Count; j++)
                            {
                                var a = item.Waypoints[j];
                                var b = item2.Waypoints[j];
                                if (a.X != b.X || a.Y != b.Y || a.Z != b.Z)
                                {
                                    equal = false;
                                    break;
                                }
                            }

                            if (equal)
                            {
                                list.Remove(item2);
                            }
                        }
                    }
                }
            }

            //foreach (var group in list.GroupBy(x => x.GUID))
            //{
            //    group.OrderBy(x => x.Index).SelectMany(x => x.Waypoints).OrderBy(x => x.Time).ThenBy(x => x.Index)
            //}

            if (InvokeRequired)
            {
                Invoke
                (new Action
                (() =>
                {
                    toolStripStatusLabel.Text = "Completed Consolidating...";
                    exportToolStripMenuItem.Enabled = false;
                }));
            }
        }

        private void copyGoXYZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selection = GetSelectedWaypoint();

            if (selection == null)
                return;

            Clipboard.SetText($".go xyz {selection.X} {selection.Y} {selection.Z}");
        }

        private void copySpawnOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var val = ExportSpawnOnly();

            if (string.IsNullOrWhiteSpace(val))
                return;

            Clipboard.SetText(val);
        }

        private void copySpawnWithCreatureMovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var val = ExportSpawnWithCreatureMovement();

            if (string.IsNullOrWhiteSpace(val))
                return;

            Clipboard.SetText(val);
        }

        private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = WaypointContainerGridView.DataSource as SortableBindingList<WaypointContainer>;

            foreach (var waypointContainer in list)
            {
                waypointContainer.Selected = false;
            }
        }

        private string ExportSpawnOnly()
        {
            if (WaypointContainerGridView.SelectedRows.Count > 0)
            {
                var items = WaypointContainerGridView.SelectedRows.Cast<DataGridViewRow>().Select(x => x.DataBoundItem as WaypointContainer).Where(x => x != null);

                string result = null;
                if (Functions.InputBox("Enter Starting Guid", "Starting Guid", ref result) != DialogResult.OK)
                    return null;

                var sb = new StringBuilder();

                var guid = result.ToLong();

                foreach (var group in items.GroupBy(x => x.Entry))
                {
                    var first = group.First();
                    var creature = DBHandler.GetFirstCreatureByid(first.Entry);

                    if (creature == null)
                        break;

                    sb.AppendLine();
                    sb.AppendLine($"-- Creature SPawn for Entry: \"{first.Entry}\" - \"{first.Name}\"");
                    if (group.Count() == 1)
                    {
                        sb.AppendLine("DELETE FROM `creature` WHERE `guid` = '{guid}';");
                    }
                    else
                    {
                        sb.AppendLine($"DELETE FROM `creature` WHERE `guid` BETWEEN '{guid}' AND '{guid + items.Count()}';");
                    }

                    sb.AppendLine("INSERT INTO `creature` (`guid`, `id`, `map`, `spawnMask`, `modelid`, `equipment_id`, `position_x`, `position_y`, `position_z`, `orientation`, `spawntimesecsmin`, `spawntimesecsmax`, `spawndist`, `currentwaypoint`, `curhealth`, `curmana`, `DeathState`, `MovementType`) VALUES ");

                    foreach (var item in items)
                    {
                        var order = item.Waypoints.OrderBy(x => x.Index).ThenBy(x => x.Time);

                        var waypoint = order.FirstOrDefault(x => x.IsAverage);

                        if (waypoint == null)
                        {
                            waypoint = order.FirstOrDefault();
                        }

                        var o = waypoint.O;

                        sb.Append($"('{guid}', '{creature.id}', '{creature.map}', '{creature.spawnMask}', '{creature.modelid}', '{creature.equipment_id}', '{waypoint.X}', '{waypoint.Y}', '{waypoint.Z}', '{o}', '{creature.spawntimesecsmin}', '{creature.spawntimesecsmax}', '{Math.Max(Math.Round(waypoint.EstimatedSpawnDistance, 0), 5)}', '{creature.currentwaypoint}', '{creature.curhealth}', '{creature.curmana}', '{creature.DeathState}', '1')");

                        sb.AppendLine(items.Last() == item ? ";" : ",");
                        guid++;
                    }

                    sb.AppendLine();
                }

                var val = sb.ToString();

                if (string.IsNullOrWhiteSpace(val))
                    return null;

                return val;
            }

            return null;
        }

        private void exportSpawnOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var val = ExportSpawnOnly();

            if (string.IsNullOrWhiteSpace(val))
                return;

            saveFileDialog.Title = "Save File";
            saveFileDialog.Filter = "SQL (*.sql)|*.sql";
            saveFileDialog.FileName = "";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.CheckFileExists = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, val);
            }
        }

        private string ExportSpawnWithCreatureMovement()
        {
            if (WaypointContainerGridView.SelectedRows.Count > 0)
            {
                var items = WaypointContainerGridView.SelectedRows.Cast<DataGridViewRow>().Select(x => x.DataBoundItem as WaypointContainer).Where(x => x != null);

                string result = null;
                if (Functions.InputBox("Enter Starting Guid", "Starting Guid", ref result) != DialogResult.OK)
                    return null;

                var sb = new StringBuilder();

                var guid = result.ToLong();

                foreach (var group in items.GroupBy(x => x.Entry))
                {
                    var first = group.First();
                    var creature = DBHandler.GetFirstCreatureByid(first.Entry);

                    if (creature == null)
                        break;

                    sb.AppendLine();
                    sb.AppendLine($"-- Creature Spawn for Entry: \"{first.Entry}\" - \"{first.Name}\"");
                    if (group.Count() == 1)
                    {
                        sb.AppendLine("DELETE FROM `creature` WHERE `guid` = '{guid}';");
                    }
                    else
                    {
                        sb.AppendLine($"DELETE FROM `creature` WHERE `guid` BETWEEN '{guid}' AND '{guid + items.Count()}';");
                    }

                    foreach (var item in items)
                    {
                        var order = item.Waypoints.OrderBy(x => x.Index).ThenBy(x => x.Time);

                        var waypoint = order.First();
                        sb.AppendLine("INSERT INTO `creature` (`guid`, `id`, `map`, `spawnMask`, `modelid`, `equipment_id`, `position_x`, `position_y`, `position_z`, `orientation`, `spawntimesecsmin`, `spawntimesecsmax`, `spawndist`, `currentwaypoint`, `curhealth`, `curmana`, `DeathState`, `MovementType`) VALUES ");
                        sb.AppendLine($"('{guid}', '{creature.id}', '{creature.map}', '{creature.spawnMask}', '{creature.modelid}', '{creature.equipment_id}', '{waypoint.X}', '{waypoint.Y}', '{waypoint.Z}', '{waypoint.O}', '{creature.spawntimesecsmin}', '{creature.spawntimesecsmax}', '0', '{creature.currentwaypoint}', '{creature.curhealth}', '{creature.curmana}', '{creature.DeathState}', '2');");
                        sb.AppendLine("INSERT INTO `creature_movement` (`id`,`point`,`position_x`,`position_y`,`position_z`,`waittime`,`script_id`,`textid1`,`textid2`,`textid3`,`textid4`,`textid5`,`emote`,`spell`,`orientation`,`model1`,`model2`) VALUES");
                        item.Waypoints.ToList().ForEach(x => { sb.AppendLine($"({guid},{x.Index},{x.X},{x.Y},{x.Z},{x.WaitTime},0,0,0,0,0,0,0,0,{x.O},0,0),"); });
                        sb.Length = sb.Length - 3;
                        sb.Append(";");
                        sb.AppendLine();
                        sb.AppendLine($"-- .go xyz {waypoint.X} {waypoint.Y} {waypoint.Z}");
                        guid++;
                    }

                    sb.AppendLine();
                }

                var val = sb.ToString();

                if (string.IsNullOrWhiteSpace(val))
                    return null;

                return val;
            }

            return null;
        }

        private void exportSpawnWithCreatureMovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileToSql(ExportSpawnWithCreatureMovement());
        }

        private void exportToUDBSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileToSql(ExportSpawnOnly());
        }

        private void filterToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            UpdateWaypointContainerGridView();
        }

        private void generateAverageWaitTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WaypointContainerGridView.SelectedRows.Count > 0)
            {
                var items = WaypointContainerGridView.SelectedRows.Cast<DataGridViewRow>().Select(x => x.DataBoundItem as WaypointContainer).Where(x => x != null).ToList();
                items.ForEach(x => x.AssignAverageWaitTime());
            }
        }

        private void generateAverageWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WaypointContainerGridView.SelectedRows.Count > 0)
            {
                var items = WaypointContainerGridView.SelectedRows.Cast<DataGridViewRow>().Select(x => x.DataBoundItem as WaypointContainer).Where(x => x != null);

                foreach (var item in items)
                {
                    item.AssignAverageWaypoint();
                }
            }
        }

        private void generateExactWaitTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WaypointContainerGridView.SelectedRows.Count > 0)
            {
                var items = WaypointContainerGridView.SelectedRows.Cast<DataGridViewRow>().Select(x => x.DataBoundItem as WaypointContainer).Where(x => x != null).ToList();
                items.ForEach(x => x.AssignExactWaitTime());
            }
        }

        private Waypoint GetSelectedWaypoint()
        {
            if (WaypointGridView.SelectedRows.Count > 0)
            {
                return WaypointGridView.SelectedRows[0].DataBoundItem as Waypoint;
            }

            return null;
        }

        private WaypointContainer GetSelectedWaypointContainer()
        {
            if (WaypointContainerGridView.SelectedRows.Count > 0)
            {
                return WaypointContainerGridView.SelectedRows[0].DataBoundItem as WaypointContainer;
            }

            return null;
        }

        private void GridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            var gridView = (sender as DataGridView);

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    var clickedCell = gridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    gridView.CurrentCell = clickedCell;
                }
            }
        }

        private void HandleTrinityCoreSniff(string fileName, List<WaypointContainer> list)
        {
            using (var sr = File.OpenText(fileName))
            {
                if (InvokeRequired)
                {
                    Invoke
                    (new Action
                    (() =>
                    {
                        toolStripProgressBar1.ProgressBar.Maximum = (int)sr.BaseStream.Length;
                        toolStripProgressBar1.ProgressBar.Minimum = 0;
                    }));
                }

                uint index = 0;
                IDictionary<string, bool> guidInCombat = new Dictionary<string, bool>();

                while (!sr.EndOfStream)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => { toolStripProgressBar1.ProgressBar.Value = (int)sr.BaseStream.Position; }));
                    }

                    var line = sr.ReadLine();

                    if (line.Contains("SMSG_ON_MONSTER_MOVE") || line.Contains("SMSG_MONSTER_MOVE"))
                    {

                        WaypointContainer waypointContainer = null;
                        Waypoint lastPoint = null;

                        var values = line.Split(' ');
                        var time = values[9].Split('.');
                        var timeSpan = time[0].ToTimeSpan();
                        var count = 0u;

                        do
                        {
                            line = sr.ReadLine();

                            if (line.Contains("MoverGUID: Full:"))
                            {
                                if (line.Contains("Creature/0") || line.Contains("Vehicle/0"))
                                {
                                    index++;
                                      waypointContainer = new WaypointContainer
                                                            {
                                                                Index = index
                                                            };
                                    list.Add(waypointContainer);

                                    var packetline = line.Split(' ');
                                    waypointContainer.Entry = packetline[8].ToUint();
                                    waypointContainer.GUID = packetline[2];

                                    if (_creatureTemplateEntryAndNameList.ContainsKey(waypointContainer.Entry))
                                    {
                                        waypointContainer.Name = _creatureTemplateEntryAndNameList[waypointContainer.Entry];
                                    }
                                }
                      
                            }
                            else if (waypointContainer != null && line.Contains("PointsCount:"))
                            {
                                count = line.Split(' ')[3].ToUint();
                                lastPoint.Index = count;
                            }
                            else if (waypointContainer != null && line.StartsWith("Position: X:"))
                            {
                                var packetline = line.Split(' ');

                                lastPoint = new Waypoint
                                 {
                                     Index = 0,
                                     X = packetline[2].ToFloat(),
                                     Y = packetline[4].ToFloat(),
                                     Z = packetline[6].ToFloat(),
                                     O = 0f,
                                     Time = timeSpan
                                 };
                                waypointContainer.Waypoints.Add(lastPoint);
                            }
                            else if (waypointContainer != null && line.Contains("[0] Points: X:"))
                            {
                                var packetline = line.Split(' ');

                                waypointContainer.Waypoints.Add
                                (new Waypoint
                                 {
                                     Index = (uint)waypointContainer.Waypoints.Count + 1,
                                     X = packetline[5].ToFloat(),
                                     Y = packetline[7].ToFloat(),
                                     Z = packetline[9].ToFloat(),
                                     O = 0f,
                                     Time = timeSpan
                                 });
                            }
                            else if (waypointContainer != null && line.Contains("FaceDirection:"))
                            {
                                var packetline = line.Split(' ');
                                waypointContainer.Waypoints.Last().O = packetline[3].ToFloat();
                            }
                            else if (waypointContainer != null && line.Contains("WayPoints: X:"))
                            {
                                var packetline = line.Split(' ');

                                waypointContainer.Waypoints.Add
                                (new Waypoint
                                 {
                                     Index = (uint)waypointContainer.Waypoints.Count + 1,
                                     X = packetline[5].ToFloat(),
                                     Y = packetline[7].ToFloat(),
                                     Z = packetline[9].ToFloat(),
                                     O = 0f,
                                     Time = timeSpan
                                 });
                            }
                        }

                        while (line != string.Empty);
                    }
                    else if (Settings.Default.ObjectUpdate && (line.Contains("SMSG_UPDATE_OBJECT") || line.Contains("SMSG_COMPRESSED_UPDATE_OBJECT")))
                    {
                        var values = line.Split(' ');
                        var time = values[9].Split('.');
                        var timeSpan = time[0].ToTimeSpan();
                        WaypointContainer waypointContainer = null;

                        do
                        {
                            line = sr.ReadLine();

                            if (line.Contains(" Object Guid: Full: "))
                            {
                                if (line.Contains("Vehicle/0") || line.Contains("Creature/0"))
                                {
                                    var packetline = line.Split(' ');

                                    index++;
                                    waypointContainer = new WaypointContainer
                                                        {
                                                            Index = index
                                                        };
                                    list.Add(waypointContainer);

                                    waypointContainer.Entry = packetline[10].ToUint();
                                    waypointContainer.GUID = packetline[4];

                                    if (_creatureTemplateEntryAndNameList.ContainsKey(waypointContainer.Entry))
                                    {
                                        waypointContainer.Name = _creatureTemplateEntryAndNameList[waypointContainer.Entry];
                                    }
                                }
                            }
                            /*
                            if (lines[i].Contains("Transport/0"))
                            {
                                if (lines[i].Contains("Transport Position: X:"))
                                {
                                    string[] packetline = lines[i].Split(new char[] { ' ' });
                                    sniff.x = packetline[4];
                                    sniff.y = packetline[6];
                                    sniff.z = packetline[8];
                                    sniff.o = packetline[10];

                                    DataRow dr = dt.NewRow();
                                    dr[0] = sniff.entry;
                                    dr[1] = sniff.guid;
                                    dr[2] = sniff.x;
                                    dr[3] = sniff.y;
                                    dr[4] = sniff.z;
                                    dr[5] = sniff.o;
                                    dr[6] = sniff.time;
                                    dt.Rows.Add(dr);
                                }
                            }*/

                            else if (waypointContainer != null && line.Contains(" Points: X:"))
                            {
                                var packetline = line.Split(' ');

                                waypointContainer.Waypoints.Add
                                (new Waypoint
                                 {
                                     Index = (uint)waypointContainer.Waypoints.Count + 1,
                                     X = packetline[4].ToFloat(),
                                     Y = packetline[6].ToFloat(),
                                     Z = packetline[8].ToFloat(),
                                     O = 0f,
                                     Time = timeSpan
                                 });
                            }
                            else if (waypointContainer != null && waypointContainer.Waypoints.Any() && line.Contains(" Orientation:"))
                            {
                                waypointContainer.Waypoints.Last().O = line.Split(' ')[2].ToFloat();
                            }
                            else if (waypointContainer != null && line.Contains("UNIT_FIELD_FLAGS:"))
                            {
                                //[2](58) UNIT_FIELD_FLAGS: (32832) Unk6, OnlySwim
                                //20	524288	IN_COMBAT	

                                var flags = Regex.Replace(line, @"\[[0-9]+\] UNIT_FIELD_FLAGS: ([0-9]+).*", "$1").ToUint();
                                waypointContainer.InCombat = (flags & 524288) > 0;

                                if (guidInCombat.ContainsKey(waypointContainer.GUID))
                                {
                                    guidInCombat[waypointContainer.GUID] = waypointContainer.InCombat;
                                }
                                else
                                {
                                    guidInCombat.Add(waypointContainer.GUID, waypointContainer.InCombat);
                                }
                            }

                        }
                        while (line != string.Empty);
                    }
                }
            }
        }

        private void HandleUDBParserSniff(string fileName, List<WaypointContainer> list)
        {
            using (var sr = File.OpenText(fileName))
            {
                if (InvokeRequired)
                {
                    Invoke
                    (new Action
                    (() =>
                    {
                        toolStripProgressBar1.ProgressBar.Maximum = (int)sr.BaseStream.Length;
                        toolStripProgressBar1.ProgressBar.Minimum = 0;
                    }));
                }

                uint index = 0;
                IDictionary<string, bool> guidInCombat = new Dictionary<string, bool>();

                while (!sr.EndOfStream)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => { toolStripProgressBar1.ProgressBar.Value = (int)sr.BaseStream.Position; }));
                    }

                    var line = sr.ReadLine();
                    if (line.Contains("SMSG_CREATURE_QUERY_RESPONSE"))
                    {
                        uint entry = 0;
                        do
                        {
                            line = sr.ReadLine();

                            if (line.Contains("Entry:"))
                            {
                                entry = line.Split(' ')[1].ToUint();

                                if (!_creatureTemplateEntryAndNameList.ContainsKey(entry))
                                {
                                    _creatureTemplateEntryAndNameList.Add(entry, null);
                                }
                            }
                            else if (line.Contains("[0] Name: ") && entry > 0)
                            {
                                _creatureTemplateEntryAndNameList[entry] = line.Replace("[0] Name: ", string.Empty).Trim();
                            }
                        }

                        while (line != string.Empty);
                    }
                    else if (line.Contains("SMSG_MONSTER_MOVE"))
                    {
                        index++;
                        var waypointContainer = new WaypointContainer
                                                {
                                                    WaypointSouceType = WaypointSouceType.MonsterMove,
                                                    Index = index
                                                };

                        list.Add(waypointContainer);

                        var values = line.Split(' ');
                        var time = values[9].Split('.');
                        var timeSpan = time[0].ToTimeSpan();
                        var count = 0u;

                        do
                        {
                            line = sr.ReadLine();

                            if (line.Contains("GUID: Full: "))
                            {
                                var parts = Regex.Replace(line, ".*Entry: ([0-9]+) UInt64: ([0-9]+)", "$1 $2").Split(' ');

                                if (parts.Length == 2)
                                {
                                    var entry = parts[0].ToUint();
                                    var guid = parts[1];

                                    if (entry == 0)
                                    {
                                        entry = 0;
                                    }

                                    waypointContainer.Entry = entry;
                                    waypointContainer.GUID = guid;

                                    if (guidInCombat.ContainsKey(waypointContainer.GUID))
                                    {
                                        waypointContainer.InCombat = guidInCombat[waypointContainer.GUID];
                                    }

                                    if (_creatureTemplateEntryAndNameList.ContainsKey(waypointContainer.Entry))
                                    {
                                        waypointContainer.Name = _creatureTemplateEntryAndNameList[waypointContainer.Entry];
                                    }
                                }
                            }
                            else if (line.Contains("Waypoints:"))
                            {
                                count = line.Split(' ')[1].ToUint();
                            }
                            else if (line.Contains("Waypoint Endpoint: X:"))
                            {
                                var packetline = line.Split(' ');

                                var waypoint = new Waypoint
                                               {
                                                   Index = count,
                                                   X = packetline[3].ToFloat(),
                                                   Y = packetline[5].ToFloat(),
                                                   Z = packetline[7].ToFloat(),
                                                   O = 0f,
                                                   Time = timeSpan
                                               };
                                if (!waypoint.IsEmpty())
                                    waypointContainer.Waypoints.Add(waypoint);
                            }
                            else if (line.Contains("Position: X:"))
                            {
                                var packetline = line.Split(' ');
                                var waypoint = new Waypoint
                                               {
                                                   Index = (uint)waypointContainer.Waypoints.Count + 1,
                                                   X = packetline[2].ToFloat(),
                                                   Y = packetline[4].ToFloat(),
                                                   Z = packetline[6].ToFloat(),
                                                   O = 0f,
                                                   Time = timeSpan
                                               };

                                if (!waypoint.IsEmpty())
                                    waypointContainer.Waypoints.Add(waypoint);
                            }
                            else if (line.Contains("Waypoint: X:"))
                            {
                                var packetline = line.Split(' ');
                                var waypoint = new Waypoint
                                               {
                                                   Index = (uint)waypointContainer.Waypoints.Count + 1,
                                                   X = packetline[3].ToFloat(),
                                                   Y = packetline[5].ToFloat(),
                                                   Z = packetline[7].ToFloat(),
                                                   O = 0f,
                                                   Time = timeSpan
                                               };

                                if (!waypoint.IsEmpty())
                                    waypointContainer.Waypoints.Add(waypoint);
                            }
                        }

                        while (line != string.Empty);
                    }
                    else if (Settings.Default.ObjectUpdate && (line.Contains("SMSG_UPDATE_OBJECT") || line.Contains("SMSG_COMPRESSED_UPDATE_OBJECT")))
                    {
                        var values = line.Split(' ');
                        var time = values[9].Split('.');
                        var timeSpan = time[0].ToTimeSpan();
                        WaypointContainer waypointContainer = null;

                        uint? entry = null;
                        string guid = null;

                        do
                        {
                            line = sr.ReadLine();

                            if (line.Contains(" GUID: Full:"))
                            {
                                var parts = Regex.Replace(line, ".*Entry: ([0-9]+) UInt64: ([0-9]+)", "$1 $2").Split(' ');
                                waypointContainer = null;
                                entry = parts[0].ToUint();
                                guid = parts[1];

                                if (entry == 0)
                                {
                                    entry = 0;
                                }
                            }
                            else if (line.Contains(" Object Type: Unit") && entry != null && guid != null)
                            {
                                index++;
                                waypointContainer = new WaypointContainer
                                                    {
                                                        WaypointSouceType = WaypointSouceType.UpdateObject,
                                                        Index = index
                                                    };

                                list.Add(waypointContainer);

                                waypointContainer.Entry = entry.Value;
                                waypointContainer.GUID = guid;

                                if (guidInCombat.ContainsKey(waypointContainer.GUID))
                                {
                                    waypointContainer.InCombat = guidInCombat[waypointContainer.GUID];
                                }

                                if (_creatureTemplateEntryAndNameList.ContainsKey(waypointContainer.Entry))
                                {
                                    waypointContainer.Name = _creatureTemplateEntryAndNameList[waypointContainer.Entry];
                                }
                            }
                            else if (waypointContainer != null && line.Contains(" Position: X:"))
                            {
                                var packetline = line.Split(' ');
                                var waypoint = new Waypoint
                                               {
                                                   Index = (uint)waypointContainer.Waypoints.Count + 1,
                                                   X = packetline[3].ToFloat(),
                                                   Y = packetline[5].ToFloat(),
                                                   Z = packetline[7].ToFloat(),
                                                   O = 0f,
                                                   Time = timeSpan
                                               };
                                if (!waypoint.IsEmpty())
                                    waypointContainer.Waypoints.Add(waypoint);
                            }
                            else if (waypointContainer != null && waypointContainer.Waypoints.Any() && line.Contains(" Orientation:"))
                            {
                                waypointContainer.Waypoints.Last().O = line.Split(' ')[2].ToFloat();
                            }
                            else if (waypointContainer != null && line.Contains("UNIT_FIELD_FLAGS:"))
                            {
                                //[2](58) UNIT_FIELD_FLAGS: (32832) Unk6, OnlySwim
                                //20	524288	IN_COMBAT	

                                var flags = Regex.Replace(line, @"\[[0-9]+\] \([0-9]+\) UNIT_FIELD_FLAGS: \(([0-9]+)\).*", "$1").ToUint();
                                waypointContainer.InCombat = (flags & 524288) > 0;

                                if (guidInCombat.ContainsKey(waypointContainer.GUID))
                                {
                                    guidInCombat[waypointContainer.GUID] = waypointContainer.InCombat;
                                }
                                else
                                {
                                    guidInCombat.Add(waypointContainer.GUID, waypointContainer.InCombat);
                                }
                            }
                        }
                        while (line != string.Empty);
                    }
                }
            }
        }

        private void LoadSniff(string fileName)
        {
            exportToolStripMenuItem.Enabled = false;

            var type = string.Empty;
            using (var file = new StreamReader(fileName))
            {
                type = file.ReadLine();
                file.Close();
                if (!type.StartsWith("# TrinityCore - WowPacketParser") && !type.StartsWith("# UDBParser"))
                {
                    MessageBox.Show(openFileDialog.FileName + " this is not a valid TrinityCore or UDBParser parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            WaypointContainer.EnablePropertyChanged = false;
            _waypointContainers.Clear();
            _viewerForm.ClearWayPoints();
            WaypointContainerGridView.DataSource = null;
            WaypointContainerGridView.Invalidate();

            var list = new List<WaypointContainer>();

            if (type.StartsWith("# TrinityCore - WowPacketParser"))
            {
                Task.Factory.StartNew
                (() =>
                {
                    if (InvokeRequired)
                    {
                        Invoke
                        (new Action
                            (() => { toolStripStatusLabel.Text = "Start Parsing..."; }));
                    }

                    HandleTrinityCoreSniff(fileName, list);
                    ConsolidateWaypointContainers(list);
                    if (InvokeRequired)
                    {
                        Invoke
                        (new Action
                        (() =>
                        {
                            toolStripStatusLabel.Text = "Completed Parsing.";

                            _waypointContainers = new SortableBindingList<WaypointContainer>(list);

                            filterToolStripComboBox.Items.Clear();
                            filterToolStripComboBox.Items.AddRange(_waypointContainers.Where(x => !string.IsNullOrWhiteSpace(x.Name)).Select(x => x.Name).Distinct().OrderBy(x => x).Cast<object>().ToArray());

                            UpdateWaypointContainerGridView();
                        }));
                    }
                });
            }
            else if (type.StartsWith("# UDBParser"))
            {
                Task.Factory.StartNew
                (() =>
                {
                    if (InvokeRequired)
                    {
                        Invoke
                        (new Action
                            (() => { toolStripStatusLabel.Text = "Start Parsing..."; }));
                    }
                    HandleUDBParserSniff(fileName, list);
                    ConsolidateWaypointContainers(list);
                    if (InvokeRequired)
                    {
                        Invoke
                        (new Action
                        (() =>
                        {
                            toolStripStatusLabel.Text = "Completed Parsing.";

                            _waypointContainers = new SortableBindingList<WaypointContainer>(list);

                            filterToolStripComboBox.Items.Clear();
                            filterToolStripComboBox.Items.AddRange(_waypointContainers.Where(x => !string.IsNullOrWhiteSpace(x.Name)).Select(x => x.Name).Distinct().OrderBy(x => x).Cast<object>().ToArray());

                            UpdateWaypointContainerGridView();
                        }));
                    }
                });
            }
        }

        private void mergeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeSelection(false);
        }

        private void mergeUniqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeSelection(true);
        }

        private void SaveFileToSql(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
                return;

            saveFileDialog.Title = "Save File";
            saveFileDialog.Filter = "SQL (*.sql)|*.sql";
            saveFileDialog.FileName = "";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.CheckFileExists = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, val);
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = WaypointContainerGridView.DataSource as SortableBindingList<WaypointContainer>;
            WaypointContainer.EnablePropertyChanged = false;
            foreach (var waypointContainer in list)
            {
                waypointContainer.Selected = true;
            }
            WaypointContainer.EnablePropertyChanged = true;
            WaypointGridView.Invalidate();
        }

        private void selectWithSameGuidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WaypointContainerGridView.SelectedRows.Count > 0)
            {
                var items = WaypointContainerGridView.SelectedRows.Cast<DataGridViewRow>().Select(x => x.DataBoundItem as WaypointContainer).Where(x => x != null).Select(x => x.GUID).Distinct().ToList();
                foreach (var waypointContainer in _waypointContainers.Where(x => items.Contains(x.GUID)))
                {
                    waypointContainer.Selected = true;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Open File";
            openFileDialog.Filter = "Json File (*.json)|*.json";
            openFileDialog.FileName = "*.json";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var t = JsonConvert.DeserializeObject<List<WaypointContainer>>(File.ReadAllText(openFileDialog.FileName));
                t.ForEach(x => x.Selected = false);
                _waypointContainers = new SortableBindingList<WaypointContainer>(t);

                filterToolStripComboBox.Items.Clear();
                filterToolStripComboBox.Items.AddRange(_waypointContainers.Where(x => !string.IsNullOrWhiteSpace(x.Name)).Select(x => x.Name).Distinct().OrderBy(x => x).Cast<object>().ToArray());

                toolStripStatusLabel.Text = openFileDialog.FileName + " is selected for input.";
                UpdateWaypointContainerGridView();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Save File";
            saveFileDialog.Filter = "Json File (*.json)|*.json";
            saveFileDialog.FileName = "";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.CheckFileExists = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var t = JsonConvert.SerializeObject(_waypointContainers.ToList());

                File.WriteAllText(saveFileDialog.FileName, t);
            }
        }

        private void toolStripButtonLoadSniff_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Open File";
            openFileDialog.Filter = "Parsed Sniff File (*.txt)|*.txt";
            openFileDialog.FileName = "*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadSniff(openFileDialog.FileName);
                toolStripStatusLabel.Text = openFileDialog.FileName + " is selected for input.";
            }
        }

        private void ToolStripComboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateWaypointContainerGridView();
        }

        private void UpdateWaypointContainerGridView()
        {
            WaypointContainer.EnablePropertyChanged = false;
            var quary = _waypointContainers.AsQueryable();
            var filterText = filterToolStripComboBox.Text;
            if (!string.IsNullOrWhiteSpace(filterText))
            {
                var filterType = ToolStripComboBoxFilterType.SelectedText;
                switch (filterType)
                {
                    case "StartsWith":
                    {
                        if (filterText.IsNumeric())
                        {
                            quary = quary.Where(x => x.Entry != 0 && x.Entry.ToString().StartsWith(filterText));
                        }
                        else
                        {
                            quary = quary.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filterText, StringComparison.InvariantCultureIgnoreCase));
                        }
                        break;
                    }
                    case "EndsWith":
                    {
                        if (filterText.IsNumeric())
                        {
                            quary = quary.Where(x => x.Entry != 0 && x.Entry.ToString().EndsWith(filterText));
                        }
                        else
                        {
                            quary = quary.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.EndsWith(filterText, StringComparison.InvariantCultureIgnoreCase));
                        }
                        break;
                    }
                    default:
                    {
                        if (filterText.IsNumeric())
                        {
                            quary = quary.Where(x => x.Entry != 0 && x.Entry.ToString().Contains(filterText));
                        }
                        else
                        {
                            quary = quary.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(filterText, StringComparison.InvariantCultureIgnoreCase) != -1);
                        }
                        break;
                    }
                }
            }

            var list = quary.ToList();

            WaypointContainer.EnablePropertyChanged = true;
            _suspectUpdates = true;
            WaypointContainerGridView.DataSource = new SortableBindingList<WaypointContainer>(list.OrderBy(x => x.Index).ThenBy(x => x.GUID).ThenBy(x => x.InCombat));
            WaypointContainerGridView.Invalidate();
            _suspectUpdates = false;
        }

        private void UpdateWaypointsGridView()
        {
            var selection = GetSelectedWaypointContainer();

            if (selection == null)
                return;

            _suspectUpdates = true;
            WaypointGridView.DataSource = selection.Waypoints;
            WaypointGridView.Invalidate();
            _suspectUpdates = false;

            //if (selection.Selected)
            //{
            //    var waypoint = selection.Waypoints.FirstOrDefault();

            //    if (waypoint == null)
            //        return;

            //    _viewerForm.ViewerSpectator.Position = new Vector3(waypoint.Y, waypoint.X, waypoint.Z);
            //    _viewerForm.UpdateViewer();
            //}
        }

        private void UpdateWaypointsViewer()
        {
            if (_suspectUpdates)
                return;
            _viewerForm?.Waypoints?.Clear();

            _viewerForm?.AddWayPoints
            (_waypointContainers.Where(x => x.Selected).SelectMany
            (y => y.Waypoints.Select
            (x => new ViewerWaypoint(_viewerForm.VbWaypoint)
                  {
                      Position = new Vector3(x.Y, x.X, x.Z),
                      Guid = y.GUID,
                      WaypointContainerGuid = y.WaypointContainerGuid,
                      Orientation = x.O,
                      Index = x.Index,
                      Time = x.Time
                  })).ToList());
        }

        private void ViewerFormOnClosed(object sender, EventArgs eventArgs)
        {

            if (_viewerForm != null)
            {
                _viewerForm.Closed -= ViewerFormOnClosed;
            }

            _viewerForm = new ViewerForm();
            _viewerForm.Show();
            _viewerForm.Closed += ViewerFormOnClosed;

        }

        private void WaypointContainerGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var selection = GetSelectedWaypointContainer();

            if (selection == null)
                return;

            selection.Selected = !selection.Selected;

            UpdateWaypointsViewer();
        }

        private void WaypointContainerGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateWaypointsViewer();
        }

        private void WaypointContainerGridView_SelectionChanged(object sender, EventArgs e)
        {
            var selection = GetSelectedWaypointContainer();

            if (selection == null)
                return;

            UpdateWaypointsGridView();
        }

        private void WaypointForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_viewerForm != null)
            {
                _viewerForm.Closed -= ViewerFormOnClosed;
                _viewerForm.Close();
                _viewerForm = null;
            }
        }

        private void WaypointForm_Load(object sender, EventArgs e)
        {
            ViewerFormOnClosed(null, null);
        }

        private void WaypointGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateWaypointsViewer();
        }

        private void WaypointGridView_SelectionChanged(object sender, EventArgs e)
        {
            var selection = GetSelectedWaypoint();

            if (selection == null)
                return;

            _viewerForm.ViewerSpectator.Position = new Vector3(selection.Y, selection.X, selection.Z);
            _viewerForm.UpdateViewer();
        }

        private void WaypointGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            UpdateWaypointsViewer();
        }

        private void WaypointGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            UpdateWaypointsViewer();
        }

        #endregion
    }
}