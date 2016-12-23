using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Soldier shepard = new Soldier();
            Adept alenko = new Adept();
            Engineer garus = new Engineer();
            Medic chaves = new Medic();
            Soldier grunt = new Soldier("Grunt", 500, 150);


            //anonymus method
            shepard.RequireSupportEvent +=  delegate (Soldier Sender, RequireSupportEventArgs enemy) 
            {
                Console.WriteLine("Soldier " + Sender.Name + " require support from " + garus.Name);
                garus.Overload(enemy.Enemy);
            };

            //lambda
            shepard.RequireSupportEvent += (Soldier Sender, RequireSupportEventArgs enemy) =>
            {
                Console.WriteLine("Soldier " + Sender.Name + " require support from " + alenko.Name);
                alenko.Singularity(enemy.Enemy);
            };


            Console.WriteLine("Name : " + grunt.Name + "; Health : " + grunt.Health + "; Shields : " + grunt.Shield);
            //call event
            shepard.RequireSupportOn(grunt);
            Console.WriteLine("Name : " + grunt.Name + "; Health : " + grunt.Health + "; Shields : " + grunt.Shield);

            //Func example
            Func<Hero, int, int> get_damage = (victim, damage) => {
                if(victim.Health >= damage){
                    victim.Health -= damage;
                    return victim.Health;
                } else {
                    victim.Health = 0;
                    return 0;
                }
            };

            Console.WriteLine("Grunt get some damage from me and now his health : " + get_damage(grunt, 120));

            //test method with Action inside
            for (int i = 0; i < 4; i++)
            {
                chaves.heal(grunt);
                Console.WriteLine("Name : " + grunt.Name + "; Health : " + grunt.Health + "; Shields : " + grunt.Shield);
            }


            Starship Normandy = new Starship("Normandy", 15);
            Normandy.AddMember(grunt);
            Normandy.AddMember(chaves);
            Normandy.AddMember(shepard);
            Normandy.AddMember(garus);

            //Normandy.GetCommand();
            
            Normandy.AddSystemToMap("Horse Head Nebula", 10);

            Normandy.ShowGalaxyMap();
            Normandy.JumpToSystem("Argos Rho");

            Console.WriteLine(Normandy.command["Jhon Shepard"].Name);


            Console.WriteLine("Command before sorting");
            Normandy.GetCommand();
            Normandy.command.Sort();

            Console.WriteLine("Command after sorting");
            Normandy.GetCommand();


            Normandy.HyperDriveStatus = HyperDriveEnum.online;
            Normandy.JumpToSystem("Horse Head Nebula");


           Console.WriteLine(Normandy.command.GetByHealth(392).Name);

           Console.WriteLine("Binary Serialization");
           BinarySerialization.SerializeSystemList(Normandy.GalaxyMap);
           BinarySerialization.DeserializeSystemList();


           Normandy.AddSystemToMap("Hawking Eta", 10);
           Console.WriteLine("JSON Serialization");
           JsonSerialization.SerializeSystemList(Normandy.GalaxyMap);
           JsonSerialization.DeserializeSystemList();
            Console.ReadKey();




   
        }
    }
}

﻿﻿