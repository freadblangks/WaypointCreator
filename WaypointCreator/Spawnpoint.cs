#region

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using WaypointCreator.Properties;

#endregion

namespace WaypointCreator
{
    public class Spawnpoint : INotifyPropertyChanged
    {
        #region Fields

        public readonly Guid SpawnpointGuid = Guid.NewGuid();

        private double _estimatedSpawnDistance;

        private uint _index;

        private bool _isAverage;

        private float _o;

        private int _waitTime;

        private float _x;

        private float _y;

        private float _z;

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public double EstimatedSpawnDistance
        {
            get
            {
                return _estimatedSpawnDistance;
            }
            set
            {
                _estimatedSpawnDistance = value;
                OnPropertyChanged();
            }
        }

        public uint Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
                OnPropertyChanged();
            }
        }

        public bool IsAverage
        {
            get
            {
                return _isAverage;
            }
            set
            {
                _isAverage = value;
                OnPropertyChanged();
            }
        }

        public float O
        {
            get
            {
                return _o;
            }
            set
            {
                _o = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Time { get; set; }

        public int WaitTime
        {
            get
            {
                return _waitTime;
            }
            set
            {
                _waitTime = value;
                OnPropertyChanged();
            }
        }

        public SortableBindingList<Waypoint> Waypoints { get; set; }

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public float Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Public Methods and Operators

        public double Distance(float toX, float toY, float toZ)
        {
            var dX = X - toX;
            var dY = Y - toY;
            var dZ = Z - toZ;
            return Math.Sqrt(dX * dX + dY * dY + dZ * dZ);
        }

        public bool IsEmpty()
        {
            return X == 0 && Y == 0 && Z == 0;
        }

        #endregion

        #region Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (WaypointContainer.EnablePropertyChanged)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}