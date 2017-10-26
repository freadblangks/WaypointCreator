#region

using System;
using System.Collections.Generic;

#endregion

namespace WaypointCreator
{
    public abstract class ParserBase
    {
        #region Public Methods and Operators

        public List<WaypointContainer> Parse(string fileName, Action<string> updateStatus, Action<int, int> updateProgress)
        {
            var list = new List<WaypointContainer>();

            updateStatus?.Invoke("Parsing.....");

            ParseInternal(fileName, list, updateStatus, updateProgress);

            updateStatus?.Invoke("Complete.");

            return list;
        }

        public abstract void ParseInternal(string fileName, List<WaypointContainer> list, Action<string> updateStatus, Action<int, int> updateProgress);

        #endregion
    }
}