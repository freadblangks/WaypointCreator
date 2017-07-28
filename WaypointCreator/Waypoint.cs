#region

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

using WaypointCreator.Properties;

#endregion

namespace WaypointCreator
{
    public class Waypoint : INotifyPropertyChanged
    {
        #region Fields

        public readonly Guid WaypointGuid = Guid.NewGuid();

        private int _index;

        private float _o;

        private TimeSpan _time;

        private int _waitTime;

        private float _x;

        private float _y;

        private float _z;

        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public int Index
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

        public TimeSpan Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

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

        public void FillSql(StringBuilder sb)
        {
            /*INSERT INTO `creature_movement` 
            * (`id`,`point`,`position_x`,`position_y`,`position_z`,`waittime`,`script_id`,`textid1`,`textid2`,`textid3`,`textid4`,`textid5`,`emote`,`spell`,`orientation`,`model1`,`model2`) 
            * VALUES*/
            sb.AppendLine($"(@GUID,{Index},{X},{Y},{Z},{WaitTime},0,0,0,0,0,0,0,0,{O},0,0),");
        }

        #endregion

        #region Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}