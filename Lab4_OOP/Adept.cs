using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_OOP
{
    public class Adept : Hero, ISpeech
    {
        protected int energy = 100; //energy allows Adept and Medic to use their skills
        public int Energy
        {
            get { return energy; }
            set { energy = value; }
        }
        public Adept()
        {
            name = "Kaiden Alenko";
            health = 75;
            damage = 30;
            shield = 75;
        }
        public Adept(string name, int health, int shield)
            : base(name, health, shield)
        {
            this.damage = 30;
        }

        /*Adept attacks directly health, avoiding the shields*/

        public override void Attack(Hero enemy)
        {
            /*Условные конструкции перестроены в соответствии с правилом,
             * что положительные условия предпочтительнее отрицательных*/
            if (this.isAlive())
            {
                if (enemy.isAlive())
                {
                    if (enemy.Health > damage)
                        enemy.Health -= damage;
                    else
                    {
                        this.kill(enemy);
                        return;
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

        public void Threaten()
        {
            Console.WriteLine(this.Name + ": You are enemies of the Alliance! Prepare to die!");
        }

        public void Inspire()
        {
            Console.WriteLine(this.Name + ": Forward! To battle! For the Alliance!");
        }

        public void Talk()
        {
            Console.WriteLine(this.Name + ": I can use my biotic powers to pull that bottle of whiskey");
        }

        public void SupportAction(RequireSupportEventArgs enemy)
        {
            Console.WriteLine(this.Name + " use Singularity on emeny " + enemy.GetName());
            Singularity(enemy.Enemy);
        }
        

        /*Здесь был устаревший комментарий, который гласил о том, что если у противника,
         * на которого применяеться сингулярность меньше 20 хп, он погибает.
         * В коде прописано, что противник погибает, если у него меньше 40 хп. 
         Комментарий обновлен в соответствии с правилом, что устаревшие комментарии должны обновляться или убираться вовсе.*/
        /* singularity attacks every enemy, that specified in params and dicrease health of each enemy by half.
         If health less than 40, killed enemy. Consuming energy */
        public virtual void Singularity(params Hero[] enemies)
        {
            int consuming = 30;
            if (this.isAlive() && this.Energy > consuming)
            {
                this.energy -= consuming;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].isAlive())
                    {

                        if (enemies[i].Health <= 40)
                            this.kill(enemies[i]);
                        else
                            enemies[i].Health /= 2;

                    }
                }
            }
        }
    }
}
