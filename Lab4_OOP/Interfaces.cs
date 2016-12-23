using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_OOP
{
    public interface IHardAmmo
    {
        void ReloadShields<T>(T Ally) where T : Adept;
        void ReloadCrioAmmo();
        void ThrowGranade(Hero enemy);
    }

    public interface ISpeech
    {
        void Threaten();
        void Inspire();
        void Talk();
    }

}
