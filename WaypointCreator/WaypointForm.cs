#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;

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

        private readonly SortableBindingList<WaypointContainer> _waypointContainers = new SortableBindingList<WaypointContainer>();

        private bool _suspectUpdates;

        private ViewerForm _viewerForm;

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

        public static WaypointContainer ParseMonsterMove(string[] lines, int i)
        {
            var waypointContainer = new WaypointContainer();

            var values = lines[i].Split(' ');
            var time = values[9].Split('.');
            var timeSpan = time[0].ToTimeSpan();

            do
            {
                i++;

                if (lines[i].Contains("MoverGUID: Full:"))
                {
                    if (lines[i].Contains("Creature/0") || lines[i].Contains("Vehicle/0"))
                    {
                        var packetline = lines[i].Split(' ');
                        waypointContainer.Entry = packetline[8].ToUint();
                        waypointContainer.GUID = packetline[2];
                    }
                }
                var waypoint = new Waypoint
                               {
                                   Index = waypointContainer.Waypoints.Count,
                                   Time = timeSpan
                               };

                waypointContainer.Waypoints.Add(waypoint);
                if (lines[i].Contains("Position: X:"))
                {
                    var packetline = lines[i].Split(' ');
                    waypoint.X = packetline[2].ToFloat();
                    waypoint.Y = packetline[4].ToFloat();
                    waypoint.Z = packetline[6].ToFloat();
                    waypoint.O = 0f;
                }

                if (lines[i].Contains("[0] Points: X:"))
                {
                    var packetline = lines[i].Split(' ');
                    waypoint.X = packetline[5].ToFloat();
                    waypoint.Y = packetline[7].ToFloat();
                    waypoint.Z = packetline[9].ToFloat();
                    waypoint.O = 0f;
                }

                if (lines[i].Contains("FaceDirection:"))
                {
                    var packetline = lines[i].Split(' ');
                    waypoint.O = packetline[3].ToFloat();
                }

                /*
                    if (lines[i].Contains("WayPoints: X:"))
                    {
                        string[] packetline = lines[i].Split(new char[] { ' ' });
                        sniff.x = packetline[5];
                        sniff.y = packetline[7];
                        sniff.z = packetline[9];
                        sniff.o = "0";

                        if (lines[i].Contains("[0]") || lines[i].Contains("[1]")) { }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = sniff.entry;
                            dr[1] = sniff.guid;
                            dr[2] = sniff.x;
                            dr[3] = sniff.y;
                            dr[4] = sniff.z;
                            dr[5] = sniff.o;
                            dr[6] = sniff.time;
                            dt.Rows.Add(dr);
                            sniff.entry = "";
                        }
                    }
                    */
            }
            while (lines[i] != string.Empty);

            return waypointContainer;
        }

        public static WaypointContainer ParseUpdateObject(string[] lines, int i)
        {
            WaypointContainer waypointContainer = null;

            var values = lines[i].Split(' ');
            var time = values[9].Split('.');
            var timeSpan = time[0].ToTimeSpan();

            do
            {
                i++;

                if (lines[i].Contains("MoverGUID: Full:"))
                {
                    if (lines[i].Contains("Vehicle/0") || lines[i].Contains("Creature/0"))
                    {
                        var packetline = lines[i].Split(' ');

                        waypointContainer = new WaypointContainer
                                            {
                                                Entry = packetline[9].ToUint(),
                                                GUID = packetline[3]
                                            };
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

                if (lines[i].Contains("Points: X:"))
                {
                    if (waypointContainer == null)
                        continue;

                    var packetline = lines[i].Split(' ');

                    waypointContainer.Waypoints.Add(new Waypoint
                                                    {
                                                        Index = waypointContainer.Waypoints.Count,
                                                        X = packetline[4].ToFloat(),
                                                        Y = packetline[6].ToFloat(),
                                                        Z = packetline[8].ToFloat(),
                                                        O = 0f,
                                                        Time = timeSpan
                                                    });
                }
            }
            while (lines[i] != string.Empty);

            return waypointContainer;
        }

        #endregion

        #region Methods

        private void copySqlToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var val = ExportToSql();

            if (string.IsNullOrWhiteSpace(val))
                return;

            Clipboard.SetText(val);
        }

        private string ExportToSql()
        {
            var selection = GetSelectedWaypointContainer();

            if (selection == null || !selection.Waypoints.Any())
                return null;
            var firstWaypoint = selection.Waypoints.First();

            var sb = new StringBuilder();
            sb.AppendLine($"-- Pathing for {selection.Name} Entry: {selection.Entry} 'UDB FORMAT'");
            sb.AppendLine("@GUID := XXXXXX;");

            sb.AppendLine($"UPDATE `creature` SET `position_x`={firstWaypoint.X},`position_y`={firstWaypoint.Y},`position_z`={firstWaypoint.Z} WHERE `guid`=@GUID;");
            sb.AppendLine("DELETE FROM `creature_movement` WHERE `id`=@GUID;");
            sb.AppendLine("INSERT INTO `creature_movement` (`id`,`point`,`position_x`,`position_y`,`position_z`,`waittime`,`script_id`,`textid1`,`textid2`,`textid3`,`textid4`,`textid5`,`emote`,`spell`,`orientation`,`model1`,`model2`) VALUES");

            selection.Waypoints.ToList().ForEach(x => x.FillSql(sb));
            sb.Length = sb.Length - 3;
            sb.Append(";");
            sb.AppendLine();
            sb.AppendLine($"-- .go xyz {firstWaypoint.X} {firstWaypoint.Y} {firstWaypoint.Z}");

            return sb.ToString();
        }

        private void exportToUDBSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var val = ExportToSql();

            if (string.IsNullOrWhiteSpace(val))
                return;

            saveFileDialog.Title = "Save File";
            saveFileDialog.Filter = "Path Insert SQL (*.sql)|*.sql";
            saveFileDialog.FileName = "";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.CheckFileExists = false;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, val);
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

        private void LoadSniff(string fileName)
        {
            using (var file = new StreamReader(fileName))
            {
                var line = file.ReadLine();
                file.Close();
                if (!line.StartsWith("# TrinityCore - WowPacketParser"))
                {
                    MessageBox.Show(openFileDialog.FileName + " this is not a valid TrinityCore parsed sniff file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            _waypointContainers.Clear();

            using (var sr = File.OpenText(fileName))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    if (line.Contains("SMSG_ON_MONSTER_MOVE"))
                    {
                        var waypointContainer = new WaypointContainer();
                        _waypointContainers.Add(waypointContainer);

                        var values = line.Split(' ');
                        var time = values[9].Split('.');
                        var timeSpan = time[0].ToTimeSpan();

                        do
                        {
                            line = sr.ReadLine();

                            if (line.Contains("MoverGUID: Full:"))
                            {
                                if (line.Contains("Creature/0") || line.Contains("Vehicle/0"))
                                {
                                    var packetline = line.Split(' ');
                                    waypointContainer.Entry = packetline[8].ToUint();
                                    waypointContainer.GUID = packetline[2];

                                    var last = _waypointContainers.LastOrDefault(x => x.GUID == waypointContainer.GUID);

                                    if (last != null)
                                    {
                                        waypointContainer.UnitFlags = last.UnitFlags;
                                    }

                                    if (_creatureTemplateEntryAndNameList.ContainsKey(waypointContainer.Entry))
                                    {
                                        waypointContainer.Name = _creatureTemplateEntryAndNameList[waypointContainer.Entry];
                                    }
                                }
                            }
                            else if (line.Contains("Position: X:"))
                            {
                                var packetline = line.Split(' ');

                                waypointContainer.Waypoints.Add(new Waypoint
                                                                {
                                                                    Index = waypointContainer.Waypoints.Count + 1,
                                                                    X = packetline[2].ToFloat(),
                                                                    Y = packetline[4].ToFloat(),
                                                                    Z = packetline[6].ToFloat(),
                                                                    O = 0f,
                                                                    Time = timeSpan
                                                                });
                            }
                            else if (line.Contains("[0] Points: X:"))
                            {
                                var packetline = line.Split(' ');

                                waypointContainer.Waypoints.Add(new Waypoint
                                                                {
                                                                    Index = waypointContainer.Waypoints.Count + 1,
                                                                    X = packetline[5].ToFloat(),
                                                                    Y = packetline[7].ToFloat(),
                                                                    Z = packetline[9].ToFloat(),
                                                                    O = 0f,
                                                                    Time = timeSpan
                                                                });
                            }
                            else if (line.Contains("FaceDirection:"))
                            {
                                var packetline = line.Split(' ');
                                waypointContainer.Waypoints.Last().O = packetline[3].ToFloat();
                            }
                            else if (line.Contains("WayPoints: X:"))
                            {
                                var packetline = line.Split(' ');

                                waypointContainer.Waypoints.Add(new Waypoint
                                                                {
                                                                    Index = waypointContainer.Waypoints.Count + 1,
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
                    else if (Settings.Default.ObjectUpdate && line.Contains("SMSG_UPDATE_OBJECT"))
                    {
                        var values = line.Split(' ');
                        var time = values[9].Split('.');
                        var timeSpan = time[0].ToTimeSpan();
                        WaypointContainer waypointContainer = null;

                        do
                        {
                            line = sr.ReadLine();

                            if (line.Contains("MoverGUID: Full:"))
                            {
                                if (line.Contains("Vehicle/0") || line.Contains("Creature/0"))
                                {
                                    var packetline = line.Split(' ');

                                    waypointContainer = new WaypointContainer();
                                    _waypointContainers.Add(waypointContainer);

                                    waypointContainer.Entry = packetline[9].ToUint();
                                    waypointContainer.GUID = packetline[3];

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

                            else if (line.Contains("Points: X:"))
                            {
                                var packetline = line.Split(' ');

                                waypointContainer.Waypoints.Add(new Waypoint
                                                                {
                                                                    Index = waypointContainer.Waypoints.Count + 1,
                                                                    X = packetline[4].ToFloat(),
                                                                    Y = packetline[6].ToFloat(),
                                                                    Z = packetline[8].ToFloat(),
                                                                    O = 0f,
                                                                    Time = timeSpan
                                                                });
                            }
                            else if (line.Contains("UNIT_FIELD_FLAGS:") && waypointContainer != null)
                            {
                                var packetline = line.Split(' ');

                                var unitFlags = packetline[2].Split('/')[0].ToUint();

                                waypointContainer.UnitFlags = unitFlags;
                            }
                        }
                        while (line != string.Empty);
                    }
                }
            }

            UpdateWaypointContainerGridView();
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

        private void ToolStripTextBoxFilterText_TextChanged(object sender, EventArgs e)
        {
            UpdateWaypointContainerGridView();
        }

        private void UpdateWaypointContainerGridView()
        {
            var quary = _waypointContainers.AsQueryable();
 
            var filterText = ToolStripTextBoxFilterText.Text;
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
                            quary = quary.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.EndsWith(filterText, StringComparison.InvariantCultureIgnoreCase))
                                ;
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
                            quary = quary.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(filterText, StringComparison.InvariantCultureIgnoreCase) != -1)
                                ;
                        }
                        break;
                    }
                }
            }

            _suspectUpdates = true;
            WaypointContainerGridView.DataSource = new SortableBindingList<WaypointContainer>(quary.ToList());
            WaypointContainerGridView.Invalidate();
            _suspectUpdates = false;
        }

        private void UpdateWaypointsViewer()
        {
            if (_suspectUpdates)
                return;
            _viewerForm?.Waypoints?.Clear();

            _viewerForm?.AddWayPoints(_waypointContainers.Where(x => x.Selected).SelectMany(y => y.Waypoints.Select(x => new ViewerWaypoint(_viewerForm.VbWaypoint)
                                                                                                                         {
                                                                                                                             Position = new Vector3(x.Y, x.X, x.Z),
                                                                                                                             Guid = y.GUID,
                                                                                                                             WaypointContainerGuid = y.WaypointContainerGuid,
                                                                                                                             Orientation = x.O,
                                                                                                                             Index = x.Index
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

        private void WaypointContainerGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateWaypointsViewer();
        }

        private void WaypointContainerGridView_SelectionChanged(object sender, EventArgs e)
        {
            var selection = GetSelectedWaypointContainer();

            if (selection == null)
                return;
            _suspectUpdates = true;
            WaypointGridView.DataSource = selection.Waypoints;
            WaypointGridView.Invalidate();
            _suspectUpdates = false;

            if (selection.Selected)
            {
                var waypoint = selection.Waypoints.FirstOrDefault();

                if (waypoint == null)
                    return;

                _viewerForm.ViewerSpectator.Position = new Vector3(waypoint.Y, waypoint.X, waypoint.Z);
                _viewerForm.UpdateViewer();
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