using System;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_OOP
{

    public abstract class Hero : IComparable, IDisposable
    {

        protected bool disposed = false;
        protected string name;
        protected int health;
        protected int shield;
        protected int damage;
        protected Hero() { }
        public Hero(string name, int health, int shield)
        {
            this.name = name;
            this.health = health;
            this.shield = shield;
        }

        public int Damage
        {
            get { return damage; }
            set
            {
                if (value >= 0)
                    damage = value;
                else
                {
                    damage = 0;
                    Console.WriteLine("Value damage can't be less then zero");
                }
            }
        }
        public int Health
        {
            get { return health; }
            set
            {
                if (value >= 0)
                    health = value;
                else
                {
                    throw new MyException(value, "Can't set the health to this value ");
                }
            }
        }

        public int Shield
        {
            get { return shield; }
            set
            {
                if (value >= 0)
                    shield = value;
                else
                {
                    shield = 0;
                    Console.WriteLine("Value shield can't be less then zero");
                }
            }
        }

 
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        public abstract void Attack(Hero enemy);
        public bool isAlive()
        {
            if (health > 0)
                return true;
            else
                return false;
        }

        int IComparable.CompareTo(object o)
        {
            if (!(o is Hero))
                throw new ArgumentException();

            Hero hero = (Hero)o;
            if (this.health < hero.health) return 1;
            if (this.health > hero.health) return -1;
            else return 0;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        protected void kill(Hero enemy)
        {
            enemy.Health = 0;
            Console.WriteLine(this.Name + " killed " + enemy.Name);
            return;
        }
    }

    public class Heroes : IEnumerable, IEnumerator, IDisposable
    {
    Hero[] heroes;
        int pos = -1;

        public Hero this[string name]
        {
            get
            {
                foreach (Hero h in heroes)
                {
                    if (h.Name == name) return h;
                }
                return null;
            }
            set { this[name] = value; }
        }

        public int getCount()
        {
            return heroes.Length;
        }
        public Hero this[int index]
        {
            get { return (Hero)heroes[index]; }
            set { if(value is Hero) 
                    heroes[index] = value;
                 else
                    throw new ArgumentException();
            }
        }

        public IEnumerator GetEnumerator() { return (IEnumerator) this ; }

        public bool MoveNext()
        {
            if (pos < heroes.Length - 1)
            {
                pos++;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            pos = -1;
        }

        public object Current
        {
            get { return heroes[pos]; }
        }

        public Heroes (int n)
            {
                heroes = new Hero[n];
            }
             
            public void Sort()
            {
                Array.Sort(this.heroes);
                this.Reset();
            }

            public void Dispose()
            {
                foreach (Hero h in heroes)
                    if(h != null)
                        h.Dispose();
            }

            ~Heroes()
            {
                Dispose();
            }
    }


    public static class HeroesExtension
    {
        public static Hero GetByHealth(this Heroes heroes, int health)
        {
            foreach (Hero h in heroes)
            {
                if(h != null)
                    if (h.Health == health) return h;
            }
            return null;
        }
    }
}
