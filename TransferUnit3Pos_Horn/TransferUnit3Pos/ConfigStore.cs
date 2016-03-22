using System.IO;
using System.Runtime.Serialization.Json;

namespace TransferUnit3Pos
{
    public class ConfigStore
    {
        public void WriteConfig<T>(T data, string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(stream, data);
                stream.Flush();
            }
        }
        public T ReadConfig<T>(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                stream.Position = 0;
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(stream);
            }
        }
    }
}
