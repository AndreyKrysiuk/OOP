using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_OOP
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




            WeakReference wr = new WeakReference(grunt);
            grunt = null;
            GC.Collect();
            grunt = (Soldier)wr.Target;
            if (grunt != null)
                Console.WriteLine("Object isn't collected");
            else
                Console.WriteLine("Object is already collected");

            Console.ReadKey();
        }
    }
}

﻿﻿