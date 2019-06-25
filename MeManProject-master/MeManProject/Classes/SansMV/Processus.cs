using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class Processus
    {
        public string nom { get; set; }
        public int taille { get; set; }
        public int temps { get; set; }
        public char etat { get; set; } // T pour attente
                                       // A pour actif
                                       // B pour Bloqué                     
                                       // F pour fini

        //constructeur
        public Processus()
        {

        }

        public Processus(string n, int t, int tp, char e)
        {
            nom = n;
            taille = t;
            temps = tp;
            etat = e;
        }

        //getters
        public string getnom()
        {
            return nom;
        }

        public int gettaille()
        {
            return taille;
        }

        public int gettemps()
        {
            return temps;
        }

        public char getetat()
        {
            return etat;
        }

        //setters
        public void setnom(string nn)
        {
            nom = nn;
        }

        public void settaille(int tt)
        {
            taille = tt;
        }

        public void settemps(int ttp)
        {
            temps = ttp;
        }

        public void setetat(char ee)
        {
            etat = ee;
        }

        public Processus copy()
        {
            return (Processus)this.MemberwiseClone();
        }

        public void afficher()
        {
            Console.WriteLine("Processus " + nom + " de taille " + taille + " ko, demande " + temps + " ms pour terminer. Son etat est " + etat + " .");
        }
    }
}
