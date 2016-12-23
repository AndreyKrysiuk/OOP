using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_OOP
{
    public class RequireSupportEventArgs
    {
        private Hero enemy;
        public Hero Enemy { get { return enemy; } set { enemy = value; } }

        public string GetName()
        {
            return enemy.Name;
        }
        public RequireSupportEventArgs(Hero enemy)
        {
            this.enemy = enemy;
        }
    }

    public delegate void RequireSupportHandler(Soldier Sender, RequireSupportEventArgs enemy);


    public class Soldier : Hero, IHardAmmo, ISpeech
    {
        private static double accuracy; //parameter that affects the damage

        public event RequireSupportHandler RequireSupportEvent;

        static Soldier()
        {
            accuracy = 0.89;
        }

        public Soldier()
        {
            name = "Jhon Shepard";
            health = 100;
            damage = (int)(50 * accuracy);
            shield = 50;
        }

        public void RequireSupportOn(Hero enemy)
        {
            if (enemy != null && enemy.isAlive())
            {
                if (RequireSupportEvent != null)
                {
                    RequireSupportEvent(this, new RequireSupportEventArgs(enemy));
                }
            }

        }

        public void ReloadShields<T>(T Ally) where T : Adept 
        {
            if (Ally.isAlive() && Ally != null)
            {
                Ally.Shield += 50;
            }
            else
            {
                Console.WriteLine("Ally is dead ot not exist");
            }
        }
        public void SupportAction(RequireSupportEventArgs enemy)
        {
            Console.WriteLine(this.Name + " use granade on enemy " + enemy.GetName());
            ThrowGranade(enemy.Enemy);
        }
        public Soldier(string name, int health, int shield)
            : base(name, health, shield)
        {
            this.damage = (int)(50 * accuracy);
        }

        /* Soldier can't break enemies shield, if him damage is less then points of shield. 
        To break shield he need to use another skill */
        public override void Attack(Hero enemy)
        {
            if (!this.isAlive())
            {
                if (enemy.isAlive())
                {
                    if (enemy.Shield > this.damage)
                    {
                        Console.WriteLine(this.Name + " can't break through the shields of " + enemy.Name);
                        return;
                    }
                    else if (enemy.Shield <= this.damage && enemy.Shield > 0)
                    {
                        //if damage more then shield, some part of damage passes to health
                        int curr_damage = damage - enemy.Shield;
                        enemy.Shield = 0;
                        enemy.Health -= curr_damage;
                    }
                    else
                    {
                        if (enemy.Health > damage)
                            enemy.Health -= damage;
                        else
                        {
                            this.kill(enemy);
                            return;
                        }
                    }
                    Console.WriteLine(enemy.Name + " got " + damage + " damage from " + this.Name); //log damage
                }
                else
                {
                    Console.WriteLine(enemy.Name + " has been already dead");
                    return;
                }
            }
            else
            {
                Console.WriteLine(this.Name + " is dead. He can't attack");
            }
        }

        public void ThrowGranade(Hero enemy)
        {
            if (enemy.isAlive())
            {
                this.damage *= 2;
                Attack(enemy);
                this.damage /= 2;
                Console.WriteLine(this.Name + " used granade on " + enemy.Name);
            }
            else
            {
                Console.WriteLine(enemy.Name + "is already dead");
            }
        }

        public void ReloadCrioAmmo()
        {
            this.damage += 15;
            Console.WriteLine(this.Name + " used crio ammo");
        }

        /* This skill allow soldier kill every enemy, but i must limit it in next lab*/
        public void aimedShot(Hero enemy)
        {
            if (!this.isAlive())
            {
                if (enemy.isAlive())
                {
                    enemy.Health = 0;
                    Console.WriteLine(enemy.Name + " was killed by aimed shot from " + this.Name);
                }
                else
                {
                    Console.WriteLine(enemy.Name + " has been already dead");
                    return;
                }
            }
            else
            {
                Console.WriteLine(this.Name + " is dead. He can't attack");
                return;
            }
 
        }

        /* Inspiration - special skill of Soldier, that increase accuracy of all Soldiers, that will be create, 
         * and doubles damage of Soldier, whom used this skill*/
        public void inspiration()
        {
            accuracy = 1;
            this.damage *= 2;
        }

        public void Threaten()
        {
            Console.WriteLine(this.Name + ": I will kill every enemy of the Alliance!");
        }
        public void Inspire()
        {
            Console.WriteLine(this.Name + ": Destroy enemies of the Alliance!");
        }
        public void Talk()
        {
            Console.WriteLine(this.Name + ": Damn it! It was a great explosion!");
        }
    }
}
