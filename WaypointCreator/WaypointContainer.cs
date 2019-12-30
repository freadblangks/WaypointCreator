#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using WaypointCreator.Properties;

#endregion

namespace WaypointCreator
{
    public enum WaypointSouceType : uint
    {
        None = 0,

        UpdateObject = 1,

        MonsterMove = 2
    }

    public class WaypointContainer : INotifyPropertyChanged
    {
        #region Static Fields

        public static bool EnablePropertyChanged = false;

        #endregion

        #region Fields

        public readonly Guid WaypointContainerGuid = Guid.NewGuid();

        private bool _selected;

        #endregion

        #region Constructors and Destructors

        public WaypointContainer()
        {
            Waypoints = new SortableBindingList<Waypoint>();
        }

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public uint Entry { get; set; }

        public string GUID { get; set; }

        public bool InCombat { get; set; }

        public uint Index { get; set; }

        public int MapID { get; set; }

        public string Name { get; set; }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected == value)
                    return;
                _selected = value;
                OnPropertyChanged();
            }
        }

        public SortableBindingList<Waypoint> Waypoints { get; set; }

        public WaypointSouceType WaypointSouceType { get; set; }

        public SpawnpointSouceType SpawnpointSouceType { get; set; }

        public int ZoneID { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AssignAverageWaitTime()
        {
            if (Waypoints.Count < 2 || Waypoints.All(x => x.WaitTime != 0))
                return;

            var order = Waypoints.OrderBy(x => x.Index).ThenBy(x => x.Time).GroupBy(x => x.Time);

            var waittimes = new List<int>();

            var array = order.Select(x => x.First()).ToArray();

            for (var i = 0; i < array.Length; i++)
            {
                if (i + 1 < array.Length)
                {
                    var ms = (int)(array[i + 1].Time - array[i].Time).TotalMilliseconds;
                    waittimes.Add(ms);
                    array[i + 1].WaitTime = ms;
                }
            }

            var average = 0;

            if (waittimes.Count > 0)
                average = waittimes.Sum() / waittimes.Count;

            for (var i = 0; i < array.Length; i++)
            {
                if (i + 1 < array.Length)
                {
                    array[i + 1].WaitTime = average;
                }
            }
        }

        public void AssignAverageWaypoint()
        {
            if (Waypoints.Count < 2 || Waypoints.Any(x => x.IsAverage))
                return;

            var averageX = Waypoints.Sum(x => x.X) / Waypoints.Count;
            var averageY = Waypoints.Sum(x => x.Y) / Waypoints.Count;
            var averageZ = Waypoints.Sum(x => x.Z) / Waypoints.Count;

            var order = Waypoints.OrderBy(x => x.Index).ThenBy(x => x.Time);

            var waypoint = order.FirstOrDefault(x => x.O != 0f);

            if (waypoint == null)
            {
                waypoint = order.FirstOrDefault();
            }

            var o = waypoint.O;

            var nearest = Waypoints.OrderBy(x => x.Distance(averageX, averageY, averageZ)).First();
            var farthestDistance = Waypoints.Select(x => x.Distance(averageX, averageY, averageZ)).Max();
            nearest.IsAverage = true;
            nearest.EstimatedSpawnDistance = farthestDistance;
        }

        public void AssignExactWaitTime()
        {
            if (Waypoints.Count < 2 || Waypoints.All(x => x.WaitTime != 0))
                return;

            var order = Waypoints.OrderBy(x => x.Index).ThenBy(x => x.Time).GroupBy(x => x.Time);

            var array = order.Select(x => x.First()).ToArray();

            var waittimes = new List<int>();

            for (var i = 0; i < array.Length; i++)
            {
                if (i + 1 < array.Length)
                {
                    var ms = (int)(array[i + 1].Time - array[i].Time).TotalMilliseconds;
                    waittimes.Add(ms);
                    array[i + 1].WaitTime = ms;
                }
            }
        }

        #endregion

        #region Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (EnablePropertyChanged)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}