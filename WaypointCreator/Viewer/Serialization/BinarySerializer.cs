using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WaypointCreator.Viewer.Serialization
{
    public class BinarySerializer
    {
        public static void SerializeObject<T>(MemoryStream ms, T obj)
        {
            var fomatter = new BinaryFormatter();
            fomatter.Serialize(ms, obj);
        }

        public static T DeSerializeObject<T>(MemoryStream ms)
        {
            var fomatter = new BinaryFormatter();
            return (T) fomatter.Deserialize(ms);
        }
    }
}