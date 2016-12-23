using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_OOP
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


        /*Метод heal нигде не используется в коде, потому был удален в соответствии с правилом о том,
         * что мертвые функции должны быть удалены. Но для наглядности я его просто закомментировал, 
         * коментированный код тоже следует удалять*/
        /*Medic can heal himself, but spend energy for this*/
       /* public void heal()
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
        }*/

        Action<Hero, int> up_health = (healed, health_points) =>
        {
            healed.Health += health_points;
            Console.WriteLine("Health level of " + healed.Name + " before = " + (healed.Health - health_points) + " and after = " + healed.Health);
        };

        public void SupportAction(RequireSupportEventArgs enemy)
        {
            Console.WriteLine(this.Name + " attacks enemy " + enemy.GetName());
            Attack(enemy.Enemy);
        }
        /*Also medic can heal another Hero or resurrect him, but heal less health*. Spend energy too*/
        /*Метод heal был переименован в соответствии с правилом, что методы должны описывать выполняемую ими функцию*/
        public void HealAlly(Hero ally)
        {
            int consuming = 40;
            if (this.isAlive() && energy > consuming)
            {
            /* Данная условная конструкция изменена, поскольку используется условие отрицания. 
             * По правилам предпочтительнее использовать положительные условия */
                if (ally.isAlive())
                    ally.Health += 75;
                else
                    ally.Health = 50;
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
            /*Переменная consuming была переименнована в PowerConsumption, 
             * поскольку не содержала достаточно информации о своем назначении*/
            int PowerConsumption = 70;
            if (this.isAlive() && this.Energy >= PowerConsumption)
            {
                this.energy -= PowerConsumption;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].isAlive())
                        this.kill(enemies[i]);
                }
            }
        }
    }
}
