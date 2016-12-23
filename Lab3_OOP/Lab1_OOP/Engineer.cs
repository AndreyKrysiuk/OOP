using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Lab3_OOP
{
    public class Engineer : Hero, IHardAmmo
    {
        public Engineer()
        {
            name = "Garus Vakarian";
            health = 75;
            damage = 35;
            shield = 150;
        }
        public Engineer(string name, int health, int shield)
            : base(name, health, shield)
        {
            this.damage = 35;
        }


        public void ReloadShields<T>(T Ally) where T : Adept
        {
            if (Ally.isAlive() && Ally != null)
            {
                Ally.Shield *= 2;
            }
            else
            {
                Console.WriteLine("Ally is dead ot not exist");
            }
        }
        public void ThrowGranade(Hero enemy)
        {
            if (enemy.isAlive())
            {
                this.damage *= 3;
                Attack(enemy);
                this.damage /= 3;
                Console.WriteLine(this.Name + " used granade");
            }
            else
            {
                Console.WriteLine(enemy.Name + " is already dead");
            }
        }

        public void ReloadCrioAmmo()
        {
            this.damage += 30;
            Console.WriteLine(this.Name + " used crio ammo");
        }
        /* engineer have double damage against shields, and standart damage against health*/
        public override void Attack(Hero enemy)
        {
            if (!this.isAlive())
            {
                Console.WriteLine(this.Name + " is dead. He can't attack");
                return;
            }
            if (enemy.isAlive())
            {
                if (enemy.Shield > 2 * this.damage)
                {
                    enemy.Shield -= 2 * this.damage;
                    Console.WriteLine(enemy.Name + " got " + 2 * this.damage + " damage from " + this.Name); //log double damage
                    return;
                }
                else if (enemy.Shield <= 2 * this.damage && enemy.Shield > 0)
                {
                    int curr_damage = (2 * damage - enemy.Shield) / 2; //but standart damage for health
                    if (enemy.Health < curr_damage)
                    {
                        this.kill(enemy);
                        return;
                    }
                    else
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
                Console.WriteLine(enemy.Name + " got " + damage + " damage from " + this.Name); //log standart damage
            }
            else
            {
                Console.WriteLine(enemy.Name + " has been already dead");
                return;
            }
        }

        public void SupportAction(RequireSupportEventArgs enemy)
        {
            Console.WriteLine(this.Name + " use Overload on enemy " + enemy.Enemy.Name);
            Overload(enemy.Enemy);
        }
        /*Engineers skill is overload. Overload breaks shields of enemies, if they quantity less or equal 3,
         * and reduces shields by 3/4 if quantity of enemies more then 3 */
        public void Overload(params Hero[] enemies)
        {
            if (this.isAlive())
            {
                if (enemies.Length > 3)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i].isAlive())
                            enemies[i].Shield /= 4;
                    }
                }
                else
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i].isAlive())
                            enemies[i].Shield = 0;
                    }
                }
            }
        }
    }
}

