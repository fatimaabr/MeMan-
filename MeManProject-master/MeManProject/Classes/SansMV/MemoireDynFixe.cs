using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public abstract class MemoireDynFixe
    {
        public SortedList<int, Partition> memoire { get; set; } // Adresse + Partition
        //protected List<Partition> zonesLibres ; // psk lazem tkoon ordonnee des fois
        public FileAtt fileAtt;
        public int taille;


        public void AfficherMemoire()
        {
            Console.WriteLine("**********************************");
            for (int i = 0; i < memoire.Count; i++)
            {
                memoire.Values[i].afficher();
            }
        }

        /* public void AfficherListeZoneLibre()      // kayen zoneslibres wella nn ???????????????
         {
             for (int i = 0; i < zonesLibres.Count; i++)
             {
                 zonesLibres[i].afficher();
             }
         } */

        public void AfficherFileAttente()
        {
            fileAtt.afficherFile();
        }

        public abstract int charger(Processus pro);
        public abstract int arreter(Processus pro);
    }
}
