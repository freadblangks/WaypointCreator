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
    public enum SpawnpointSouceType : uint
    {
        None = 0,
        ServerNPC = 1,
        ServerGObject = 2,
        SniffNPC = 3,
        SniffGObject = 4
    }

    public class SpawnpointContainer : INotifyPropertyChanged
    {
        #region Static Fields

        public static bool EnablePropertyChanged = false;

        #endregion

        #region Fields

        public readonly Guid SpawnpointContainerGuid = Guid.NewGuid();

        private bool _selected;

        #endregion

        #region Constructors and Destructors

        public SpawnpointContainer()
        {
            Spawnpoints = new SortableBindingList<Spawnpoint>();
        }

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public uint Entry { get; set; }

        public string GUID { get; set; }

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

        public SortableBindingList<Spawnpoint> Spawnpoints { get; set; }

        public SpawnpointSouceType SpawnpointSouceType { get; set; }

        public int ZoneID { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AssignAverageWaitTime()
        {
            if (Spawnpoints.Count < 2 || Spawnpoints.All(x => x.WaitTime != 0))
                return;

            var order = Spawnpoints.OrderBy(x => x.Index).ThenBy(x => x.Time).GroupBy(x => x.Time);

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

        public void AssignAverageSpawnpoint()
        {
            if (Spawnpoints.Count < 2 || Spawnpoints.Any(x => x.IsAverage))
                return;

            var averageX = Spawnpoints.Sum(x => x.X) / Spawnpoints.Count;
            var averageY = Spawnpoints.Sum(x => x.Y) / Spawnpoints.Count;
            var averageZ = Spawnpoints.Sum(x => x.Z) / Spawnpoints.Count;

            var order = Spawnpoints.OrderBy(x => x.Index).ThenBy(x => x.Time);

            var spawnpoint = order.FirstOrDefault(x => x.O != 0f);

            if (spawnpoint == null)
            {
                spawnpoint = order.FirstOrDefault();
            }

            var o = spawnpoint.O;

            var nearest = Spawnpoints.OrderBy(x => x.Distance(averageX, averageY, averageZ)).First();
            var farthestDistance = Spawnpoints.Select(x => x.Distance(averageX, averageY, averageZ)).Max();
            nearest.IsAverage = true;
            nearest.EstimatedSpawnDistance = farthestDistance;
        }

        public void AssignExactWaitTime()
        {
            if (Spawnpoints.Count < 2 || Spawnpoints.All(x => x.WaitTime != 0))
                return;

            var order = Spawnpoints.OrderBy(x => x.Index).ThenBy(x => x.Time).GroupBy(x => x.Time);

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