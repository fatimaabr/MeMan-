using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class MemoireDynTable : MemoireDynFixe
    {
        public List<SortedList<int, Partition>> zonesLibres { get; set; }// adresse + partition
        protected List<Partition> zonesOccupees;
        int taille_lim;
        int puiss_max;
        int puiss_min;
        public MemoireDynTable(int Taille_m, int Taille_lim, List<Processus> File)
        {
            // if ( taille_lim < taille) => Controle de saisie de la taille !! la taille de la memoire doit etre une puissance de 2
            // sinon , je ne vois pas l interet d insister sur le fait que la taille lim le soit aussi ( puiss de 2 ? )
            taille = Taille_m;
            taille_lim = Taille_lim;
            memoire = new SortedList<int, Partition>();
            zonesOccupees = new List<Partition>();
            Partition P = new Partition(0, taille, false); // La taille d une memoire doit toujours etre une puissance de 2 !! controle de saisie
            memoire.Add(0, P);
            zonesLibres = new List<SortedList<int, Partition>>();
            puiss_min = (int)Math.Ceiling(Math.Log(taille_lim, 2));
            puiss_max = (int)Math.Log(taille, 2); // puiss de  2 !!! on ajoute pas de 1, deja un entier !
            for (int i = 0; i <= (puiss_max - puiss_min); i++)
            {
                zonesLibres.Add(new SortedList<int, Partition>());
            }
            zonesLibres[puiss_max - puiss_min].Add(0, P);  // Adresse 0
            fileAtt = new FileAtt(File);
        }

        public override int charger(Processus pro)
        // ROLE : Charger le processus pro en memoire en utilisant la methode buddy system.
        /* Retourne 0 si le processus a ete bien chargé en memoire. 
         *          -1 si temps/taille est nulle
         *          -2 si la taille du processus actuelle est superieure à celle de la memoire
         *           +1 si l'Espace memoire libre actuel est insuffisant pour charger ce processus            
         * */
        {
            if ((pro.gettaille() == 0)| (pro.temps == 0))
            {
                return -1; // impossible ! un processus de temps/taille nulle n est pas un processus 
            }
            else
            {
                if (pro.taille > taille)
                    return -2; // impossible de charger ce processus dans la memoire actuelle
                else
                {
                    int indTab;
                    Partition P;
                    indTab = rechBS(pro);
                    if (indTab == -1)
                    {
                        return 1; // Espace libre de la memoire insuffisant pour charger ce processus
                    }
                    else
                    {
                        P = zonesLibres[indTab].Values[0];
                        pro.setetat('A');
                        P.SetPrecessus(pro);
                        P.SetEtat(true);
                        zonesLibres[indTab].RemoveAt(0);
                        zonesOccupees.Add(P);
                        return 0; // chargement effectué avec succes.
                    }
                }
            }
        }

        public override int arreter(Processus pro)
        {
            int ind = (int)Math.Ceiling(Math.Log((double)pro.gettaille(), 2)) - puiss_min;
            Partition P = zonesOccupees.Find(x => x.GetPrecessus() == pro);
            if (P != null)
            {
                pro.setetat('F');
                P.SetEtat(false);
                P.SetPrecessus(null);
                zonesOccupees.Remove(P);
                zonesLibres[ind].Add(P.GetAdresse(), P);
                bool Fusion = fusionnerTout(ind, P.GetAdresse()); // Affichage ?? jcp, mais on  peut savoir si une fusion a été faite
            }
            else
                return -1; // introuvable => inexistant
            return 0;
        }

        private bool fusionnerTout(int ind, int adr)
        // ROLE : Retourne 'false' si au moins une fusion a été effectué, 'True' sinon
        {
            bool FUS = true;
            bool Chang = false;
            int adresse; // adresse de la partition apres fusion
            while (FUS == true)
            {
                FUS = fusionner(ind, adr, out adresse);
                if (FUS == true)
                {
                    adr = adresse;
                    ind++;
                    Chang = true;
                }
            }
            return Chang;
        }

        private bool fusionner(int ind, int adr, out int adrRes) // adrRes =W l'adresse de la partition finale en cas de fusion
        {
            if (zonesLibres[zonesLibres.Count - 1].Count != 0)
            {
                adrRes = adr;
                return false;
            }
            else
            {
                int adrC = adr ^ (int)Math.Pow(2.0, ind + puiss_min);
                if (zonesLibres[ind].ContainsKey(adrC))
                {
                    Partition P = null;
                    if (adr < adrC)
                    {
                        zonesLibres[ind].Remove(adrC);
                        memoire.Remove(adrC);
                        P = zonesLibres[ind].Values[zonesLibres[ind].IndexOfKey(adr)];
                    }
                    else
                    {
                        zonesLibres[ind].Remove(adr);
                        memoire.Remove(adr);
                        P = zonesLibres[ind].Values[zonesLibres[ind].IndexOfKey(adrC)];
                        adr = adrC;
                    }
                    adrRes = adr;
                    P.SetTaille(P.GetTaille() * 2);
                    zonesLibres[ind].Remove(adr);
                    zonesLibres[ind + 1].Add(adr, P);
                }
                else
                {
                    adrRes = adr;
                    return false;
                }
                return true;
            }
        }

        private int rechBS(Processus Pro)
        // ROLE: Retourne l indice de la liste de zones libre appropriée pour le processus 'Pro' si elle existe
        //      SINON, elle retourne -1
        {
            // on doit verifier que la taille dela partition est inf de celle de la memoire !!!!!!
            int ind = (int)Math.Ceiling(Math.Log((double)Pro.gettaille(), 2)) - puiss_min;
            if (ind < 0) // si la taille du processus, apres l avoir arrondi en une puissance de 2, reste inferieure à la taille de partition limite
                ind = 0; // on charge ce processus, dans la partition de taille limite
            bool trouv = false;
            int k = ind;
            while (!trouv)
            {
                if (k > puiss_max - puiss_min) // On a dépassé la limite du tableau 
                    return -1; // pas de partition disponible correspandante
                else
                {
                    if (zonesLibres[k].Count != 0)
                    {
                        trouv = true;
                    }
                    else
                    {
                        k++;
                    }
                }
            }
            Partition P;
            while (k > ind)
            {
                P = zonesLibres[k].Values[0];
                zonesLibres[k].RemoveAt(0);
                P.SetTaille(P.GetTaille() / 2);
                Partition P2 = new Partition(P.GetAdresse() + P.GetTaille(), P.GetTaille(), false);
                zonesLibres[k - 1].Add(P.GetAdresse(), P);
                zonesLibres[k - 1].Add(P2.GetAdresse(), P2);
                memoire.Add(P2.GetAdresse(), P2);
                k--;
            }
            return ind;
        }
    }
}
