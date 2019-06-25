using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class MemoireSecondaire
    {
        private List<Processus> MSS;
        public MemoireSecondaire()//'constructeur'
        {
            MSS = new List<Processus>();
        }

        public MemoireSecondaire(List<Processus> lp)// A supprimer ! 
        {
            MSS = new List<Processus>();
            foreach (Processus p in lp)
            {
                MSS.Add(p);
            }
        }

        public void Ajprocess(Processus p)
        {
            MSS.Add(p);
        }

        public Processus Recupprocess(int indice)
        {
            Processus p;
            p = MSS[indice];
            MSS.RemoveAt(indice);
            return p;
        }

        public Processus Affichepro(int indice)
        {
            return MSS[indice];
        }

        public void Etatprocess(Processus p, char etat)
        {
            int indice;
            indice = MSS.IndexOf(p);
            MSS[indice].setetat(etat);
        }

        /*public void Setmemsec(List<Processus> ms)
        {
            MSS = ms;
        }*/

        public List<Processus> Getmemsec()
        {
            return MSS;
        }

        public void Affichermems()
        {
            if (MSS.Count() > 0)
            {
                for (int i = 0; i < MSS.Count; i++)
                {
                    MSS[i].afficher();
                }
            }
            else { Console.WriteLine("vide!!!"); }
        }
    }
}
