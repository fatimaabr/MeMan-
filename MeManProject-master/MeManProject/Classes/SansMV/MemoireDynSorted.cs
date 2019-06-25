using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class MemoireDynSorted : MemoireDynFixe
    {
        public SortedList<int, Partition> zonesLibres { get; set; } // taille et Partition
        protected List<Partition> zonesOccupees;
        char type;      // w for worse fit
                        // b for best fit

        public MemoireDynSorted(char meth, int Taille_mem,List<Processus> File)
        {
            type = meth;
            taille = Taille_mem;
            memoire = new SortedList<int, Partition>();
            Partition P = new Partition(0, taille, false);
            memoire.Add(0, P);
            zonesLibres = new SortedList<int, Partition>();
            zonesOccupees = new List<Partition>();
            zonesLibres.Add(taille, P);
            fileAtt = new FileAtt(File);
        }

        public override int charger(Processus pro)
        // ROLE : Charger le processus pro en memoire en utilisant les methodes worse ou best fit.
        /* Retourne 0 si le processus a ete bien chargé en memoire. 
         *          -1 si temps/taille est nulle
         *          -2 si la taille du processus actuelle est superieure à celle de la memoire
         *           +1 si l'Espace memoire libre actuel est insuffisant pour charger ce processus
         * 
         * */

        {
            if ((pro.gettaille() == 0)|(pro.temps==0))
                return -1; // impossible ! un processus de temps/taille nulle n est pas un processus 
            else
            {
                if (pro.taille > taille)
                    return -2; // impossible de charger ce processus dans la memoire actuelle
                else
                {
                    int ind;
                    if (type == 'w')
                        ind = rech_worse(pro);
                    else
                        ind = rech_best(pro);
                    if (ind != -1)
                    {
                        pro.setetat('A');
                        Partition Par = zonesLibres.Values[ind];
                        Par.SetPrecessus(pro);
                        Par.SetEtat(true);
                        int taille = Par.GetTaille();
                        if (taille != pro.gettaille())
                        {
                            Par.SetTaille(pro.gettaille());
                            Partition P = new Partition(pro.gettaille() + Par.GetAdresse(), taille - pro.gettaille(), false);
                            zonesOccupees.Add(Par);
                            zonesLibres.RemoveAt(ind);
                            zonesLibres.Add(P.GetTaille(), P);
                            memoire.Add(P.GetAdresse(), P);
                        }
                        else
                            zonesLibres.RemoveAt(ind);
                    }
                    else
                        return 1; // Espace libre de la memoire insuffisant pour charger ce processus
                }
            }
            return 0;
        }

        public override int arreter(Processus pro)
        // ROLE : Libere le processus pro de la memoire
        // Retourne 0 si la liberation a été effectuée avec succés. -1 sinon
        {
            Partition P = zonesOccupees.Find(x => x.GetPrecessus() == pro);
            if (P != null)
            {
                pro.setetat('F');
                P.SetEtat(false);
                P.SetPrecessus(null);
                zonesOccupees.Remove(P);
                zonesLibres.Add(P.GetTaille(), P);
                int ind = memoire.IndexOfKey(P.GetAdresse()); // l indice de a partition en memoire
                bool FUS = fusionnerTout(ind);// AFFIIICHE
            }
            else
                return -1; // introuvable => inexistant
            return 0;
        }

        private bool fusionnerTout(int ind)
        // ROLE : Retourne 'false' si au moins une fusion a été effectué, 'True' sinon
        {
            bool FUS = true;
            bool Chang = false;
            int NewInd = ind; // soit ind soit ind - 1
            int indInit = ind; // On sauvegarde l'indice initial, pour savoir apres si une fusion a été effectué ou non
            while (FUS == true)
            {
                FUS = fusionner(ind, out NewInd);
                if (FUS == true)
                {
                    ind = NewInd;
                    Chang = true;
                }
            }
            return Chang;
        }

        private bool fusionner(int ind, out int indRes)
        {
            Partition P = memoire.Values[ind];
            if (memoire.Count == 1)
            {
                indRes = ind;
                return false;
            }
            else
            {
                if (ind == 0) // On ne vérifie kue la prochaine partition
                {
                    if ((P.GetAdresse() + P.GetTaille() == memoire.Values[ind + 1].GetAdresse()) & (memoire.Values[ind + 1].GetEtat() == false))
                    {
                        indRes = ind;
                        P.SetTaille(memoire.Values[ind + 1].GetTaille() + P.GetTaille());
                        P = memoire.Values[ind + 1]; // on doit le supprimer de la liste de zones libre
                        memoire.Remove(memoire.Values[ind + 1].GetAdresse());
                    }
                    else
                    {
                        indRes = ind;
                        return false;
                    }
                }
                else
                {
                    if (ind == memoire.Count - 1) // Derniere partition <=> On ne vérifie ue la partition précédente
                    {
                        if ((memoire.Values[ind - 1].GetAdresse() + memoire.Values[ind - 1].GetTaille() == P.GetAdresse()) & (memoire.Values[ind - 1].GetEtat() == false))
                        {
                            indRes = ind - 1;
                            memoire.Values[ind - 1].SetTaille(memoire.Values[ind - 1].GetTaille() + P.GetTaille());
                            memoire.Remove(P.GetAdresse());
                        }
                        else
                        {
                            indRes = ind;
                            return false;
                        }
                    }
                    else
                    {
                        if ((memoire.Values[ind - 1].GetAdresse() + memoire.Values[ind - 1].GetTaille() == P.GetAdresse()) & (memoire.Values[ind - 1].GetEtat() == false))
                        {
                            indRes = ind - 1;
                            memoire.Values[ind - 1].SetTaille(memoire.Values[ind - 1].GetTaille() + P.GetTaille());
                            memoire.Remove(P.GetAdresse());
                        }
                        else
                        {
                            if ((P.GetAdresse() + P.GetTaille() == memoire.Values[ind + 1].GetAdresse()) & (memoire.Values[ind + 1].GetEtat() == false))
                            {
                                indRes = ind;
                                P.SetTaille(memoire.Values[ind + 1].GetTaille() + P.GetTaille());
                                P = memoire.Values[ind + 1]; // on doit le supprimer de la liste de zones libre
                                memoire.Remove(memoire.Values[ind + 1].GetAdresse());
                            }
                            else
                            {
                                indRes = ind;
                                return false;
                            }
                        }
                    }
                }
                // Supprimer P de la liste de zones libres
                bool trouv = false;
                int i = 0;
                while ((trouv == false) & (i < zonesLibres.Count))
                {
                    if (zonesLibres.Values[i] == P)
                        trouv = true;
                    else
                        i++;
                }
                if (trouv == true)
                {
                    zonesLibres.RemoveAt(i);
                }
                else
                {
                    Console.WriteLine("Erreur lors de la fusion");
                }
                return true;
            }
        }

        private int rech_best(Processus pro)
        // ROLE : Retour l'indice de la partition cherchee si elle existe. -1 sinon.
        {
            int i = 0;
            while (i != zonesLibres.Count)
            {
                if (zonesLibres.Values[i].GetTaille() >= pro.gettaille())
                {
                    return i;
                }
                else
                    i++;
            }
            return -1;
        }

        private int rech_worse(Processus pro)
        // ROLE : Retour l'indice de la partition cherchee si elle existe. -1 sinon.
        {
            int res = -1;
            if (zonesLibres.Values[zonesLibres.Count - 1].GetTaille() >= pro.gettaille())
                res = zonesLibres.Count - 1;
            return res;
        }
    }
}
