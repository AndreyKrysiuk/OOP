using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace Lab4_OOP
{
    class JsonSerialization
    {

        /*В этом классе была заменена магическая строка "systems.dat" на именнованную константу serializationFileName
         */
        const String serializationFilename = "systems.json";
       
        public static void SerializeSystemList(List<SSystem> systems)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<SSystem>));
            using (FileStream fs = new FileStream(serializationFilename, FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, systems);
            }
        }

        public static List<SSystem> DeserializeSystemList()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<SSystem>));
            using (FileStream fs = new FileStream(serializationFilename, FileMode.OpenOrCreate))
            {
                List<SSystem> deserializedSystems = (List<SSystem>)jsonFormatter.ReadObject(fs);

                foreach (SSystem s in deserializedSystems)
                {
                    Console.WriteLine("System: " + s.Name + "; Num of planets: " + s.Planets);
                }
                return deserializedSystems;
            }
        }
    }
}
