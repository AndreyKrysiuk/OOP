using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_OOP
{
    class BinarySerialization
    {
        public static void SerializeSystemList(List<SSystem> systems)
        {
         BinaryFormatter formatter = new BinaryFormatter();

           using (FileStream fs = new FileStream("systems.dat", FileMode.OpenOrCreate))
           {
               formatter.Serialize(fs, systems);
           }
        }

        public static List<SSystem> DeserializeSystemList()
        {
            BinaryFormatter formatter = new BinaryFormatter();


            using (FileStream fs = new FileStream("systems.dat", FileMode.OpenOrCreate))
            {
                List<SSystem> deserilizeSystems = (List<SSystem>)formatter.Deserialize(fs);

                foreach (SSystem s in deserilizeSystems)
                {
                    Console.WriteLine("System: " + s.Name + "; Num of planets: " + s.Planets);
                }
                return deserilizeSystems;
            }
            
        }

    }
}
