#region

using System;
using System.Collections.Generic;
using System.ComponentModel;

#endregion

namespace WaypointCreator
{
    public class SortableBindingList<T> : BindingList<T>
    {
        #region Fields

        private readonly Dictionary<Type, PropertyComparer<T>> _comparers;

        private bool _isSorted;

        private ListSortDirection _listSortDirection;

        private PropertyDescriptor _propertyDescriptor;

        #endregion

        #region Constructors and Destructors

        public SortableBindingList()
            : base(new List<T>())
        {
            this._comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        public SortableBindingList(IEnumerable<T> enumeration)
            : base(new List<T>(enumeration))
        {
            this._comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        #endregion

        #region Properties

        protected override bool IsSortedCore => this._isSorted;

        protected override ListSortDirection SortDirectionCore => this._listSortDirection;

        protected override PropertyDescriptor SortPropertyCore => this._propertyDescriptor;

        protected override bool SupportsSearchingCore => true;

        protected override bool SupportsSortingCore => true;

        #endregion

        #region Methods

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            var itemsList = (List<T>)this.Items;

            var propertyType = property.PropertyType;
            PropertyComparer<T> comparer;
            if (!this._comparers.TryGetValue(propertyType, out comparer))
            {
                comparer = new PropertyComparer<T>(property, direction);
                this._comparers.Add(propertyType, comparer);
            }

            comparer.SetPropertyAndDirection(property, direction);
            itemsList.Sort(comparer);

            this._propertyDescriptor = property;
            this._listSortDirection = direction;
            this._isSorted = true;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override int FindCore(PropertyDescriptor property, object key)
        {
            var count = this.Count;
            for (var i = 0; i < count; ++i)
            {
                var element = this[i];
                if (property.GetValue(element).Equals(key))
                {
                    return i;
                }
            }

            return -1;
        }

        protected override void RemoveSortCore()
        {
            this._isSorted = false;
            this._propertyDescriptor = base.SortPropertyCore;
            this._listSortDirection = base.SortDirectionCore;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        #endregion
    }
}