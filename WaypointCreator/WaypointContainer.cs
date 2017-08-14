#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using WaypointCreator.Properties;

#endregion

namespace WaypointCreator
{
    public class WaypointContainer : INotifyPropertyChanged
    {
        #region Fields

        private uint _entry;

        private string _guid;

        private string _name;

        private bool _selected;

 

        private uint _unitFlags;

        public readonly Guid WaypointContainerGuid = Guid.NewGuid();
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

        public uint Entry
        {
            get
            {
                return _entry;
            }
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        public string GUID
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public bool InCombat => (UnitFlags & 524288u) != 0;

        public uint UnitFlags
        {
            get
            {
                return _unitFlags;
            }
            set
            {
                _unitFlags = value;
                OnPropertyChanged();
            }
        }

        public SortableBindingList<Waypoint> Waypoints { get; set; }

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