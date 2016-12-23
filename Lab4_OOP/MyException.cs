using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_OOP
{
    class MyException : Exception
    {
        private int healthValue;
        public int HealthValue { get { return healthValue; } }
        public MyException(int health, string message) : base(message)
        {
            this.healthValue = health;
        }
    }
}
