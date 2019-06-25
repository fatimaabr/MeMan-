using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class LigneTBLFUAging
    {
        public int IndicePage { get; set; }
        public int Ri { get; set; }
        public uint Compteur { get; set; }
        public int NumeroCase { get; set; }

        //constructeur
        public LigneTBLFUAging(int indicePage)
        {
            Compteur = 0b_00000000;
            IndicePage = indicePage;
            Ri = 0;
            NumeroCase = -1;
        }
    }
}
