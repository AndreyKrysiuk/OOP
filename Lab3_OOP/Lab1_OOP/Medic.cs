using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_OOP
{
    public class Medic : Adept
    {
        public Medic()
        {
            name = "Doctor Chaves";
            health = 50;
            shield = 50;
            damage = 15;
            energy = 140;
        }

        public Medic(string name)
            : this()
        {
            this.name = name;
        }

        public Medic(string name, int health, int shield)
            : base(name, health, shield)
        {
            damage = 15;
        }

        /*Medic can heal himself, but spend energy for this*/
        public void heal()
        {
            int consuming = 40;
            if (energy > consuming)
            {
                if (this.isAlive())
                {
                    energy -= consuming;
                    this.Health += 100;
                }
            }
        }

        Action<Hero, int> up_health = (healed, health_points) =>
        {
            healed.Health += health_points;
            Console.WriteLine("Health level of " + healed.Name + " before = " + (healed.Health - health_points) + " and after = " + healed.Health);
        };

        public void SupportAction(RequireSupportEventArgs enemy)
        {
            Console.WriteLine(this.Name + " attacks enemy " + enemy.Enemy.Name);
            Attack(enemy.Enemy);
        }
        /*Also medic can heal another Hero or resurrect him, but heal less health*. Spend energy too*/
        public void heal(Hero ally)
        {
            int consuming = 40;
            if (this.isAlive() && energy > consuming)
            {
                if (!ally.isAlive())
                    ally.Health = 50;
                else
                    ally.Health += 75;
                energy -= consuming;
            }
            else if (this.isAlive() && energy != 0)
            {
                int health_points = (int)(energy * 75 / consuming);
                energy = 0;
                up_health(ally, health_points);
            }
        }

        public override void Singularity(params Hero[] enemies)
        {
            int consuming = 70;
            if (this.isAlive() && this.Energy >= consuming)
            {
                this.energy -= consuming;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].isAlive())
                        this.kill(enemies[i]);
                }
            }
        }
    }
}
