namespace WaypointCreator.Viewer.Serialization
{
    public static class Extensions
    {
        public static T FromXml<T>(this string xml)
        {
            return (T) XMLSerializer.FromXml(xml, typeof (T));
        }

        public static string ToXml<T>(this T obj)
        {
            if (obj == null) return null;
            return XMLSerializer.ToXml(obj);
        }
    }
}