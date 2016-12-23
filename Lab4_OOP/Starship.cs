using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Lab4_OOP
{
    enum HyperDriveEnum : int { online = 1, damaged = -1, offline = 0};

    [Serializable]
    [DataContract]
    class SSystem
    {
        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public int Planets { get; set; }

        public SSystem(String name, int planetsNum)
        {
            Name = name;
            Planets = planetsNum;
        }
    }

    class Starship 
    {
        protected String name;
        public Heroes command;
        protected SSystem position;
        protected HyperDriveEnum hyperDriveStatus;
        public HyperDriveEnum HyperDriveStatus
        {
            get { return this.hyperDriveStatus; }
            set { 
                if((HyperDriveEnum)value == HyperDriveEnum.damaged
                || (HyperDriveEnum)value == HyperDriveEnum.offline 
                || (HyperDriveEnum)value == HyperDriveEnum.online)
            {
                 hyperDriveStatus = value;
            }
            }
        }
        public SSystem Position
        {
            get { return this.position; }
            set { if (value != null) this.position = value; }
        }

        private int count;

        public List<SSystem> GalaxyMap = new List<SSystem>{
            new SSystem("Local Cluster", 9),
            new SSystem("Argos Rho", 5),
            new SSystem("Armstrong Nebula", 6),
            new SSystem("Attican Beta", 8),
            new SSystem("Exodus Cluster ", 4),
            new SSystem("Gemini Sigma ", 3),
            new SSystem("Hades Gamma", 11)
    };

        public Starship(String name, int command_limit)
        {
            this.hyperDriveStatus = HyperDriveEnum.online;
            this.name = name;
            this.command = new Heroes(command_limit);
            this.position = GalaxyMap[0];
            this.count = 0;
        }

        public void AddMember(Hero member)
        {
            if (count < command.getCount())
            {
                command[count] = member;
                count++;
                Console.WriteLine("New member added");
            }
            else
            {
                Console.WriteLine("Can't add new member. " + name + " is full.");
            }
        }

        public void AddSystemToMap(String name, int planetsNum)
        {
            GalaxyMap.Add(new SSystem(name, planetsNum));
        }

        public void JumpToSystem(String SystemName)
        {
            if (hyperDriveStatus == HyperDriveEnum.online)
            {
                SSystem newSystem = GalaxyMap.Find(x => x.Name.Contains(SystemName));
                if (newSystem != null)
                {
                    this.position = newSystem;
                    Console.WriteLine("Faster then light jump successful. New System: " + newSystem.Name);
                }
                else
                {
                    Console.WriteLine("Can't jump to this system. Try another destination point");
                }
            }
            else if (hyperDriveStatus == HyperDriveEnum.offline)
            {
                Console.WriteLine("The Hyper Drive is offline. Set it to online to jump");
            }
            else
            {
                Console.WriteLine("The Hyper Drive is damaged. Repair it to jump");
            }
         }
        public void ShowGalaxyMap()
        {
            foreach (SSystem system in GalaxyMap)
            {
                Console.WriteLine("In " + system.Name + " " + system.Planets + " planets");
            }
        }

        /*метод GetCommand был переименован в ShowAllCommandMembers,
         * поскольку не корректно описывал действие метода*/
        public void ShowAllCommandMembers()
        {
            for (int i = 0; i < command.getCount(); i++)
            {
                if (command[i] != null) 
                    Console.WriteLine("- " + command[i].Name + " health :"+ command[i].Health) ;
            }
        }

        private Hero GetMemberByName(string Name)
        {
            for (int i = 0; i < count; i++)
            {
                if (command[i].Name == Name)
                {
                    return command[i];
                }
            }
            return null;
        }
    }
}
