using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class MemoireDynUnsorted : MemoireDynFixe
    {
        public List<Partition> zonesLibres { get; set; }
        protected List<Partition> zonesOccupees;
        Char type; // f for first fit
                   // n for next fit
        private int stop = 0;

        public MemoireDynUnsorted(char meth, int Taille_mem, List<Processus> File)
        {
            type = meth;
            taille = Taille_mem;
            memoire = new SortedList<int, Partition>();
            Partition P = new Partition(0, taille, false);
            memoire.Add(0, P);
            zonesLibres = new List<Partition>();
            zonesLibres.Add(P);
            zonesOccupees = new List<Partition>();
            fileAtt = new FileAtt(File);
        }

        public MemoireDynUnsorted(char meth, int Taille_mem) // NoOOOOOOOOOOOOOOOOOOOO
        {
            type = meth;
            taille = Taille_mem;
            memoire = new SortedList<int, Partition>();
            Partition P = new Partition(0, taille, false);
            memoire.Add(0, P);
            zonesLibres = new List<Partition>();
            zonesLibres.Add(P);
            zonesOccupees = new List<Partition>();
            fileAtt = new FileAtt();
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
            if ((pro.gettaille() == 0)| (pro.temps == 0))
                return -1; // impossible ! un processus de temps/taille nulle n est pas un processus 
            else
            {
                if (pro.taille > taille)
                    return -2; // impossible de charger ce processus dans la memoire actuelle
                else
                {
                    int ind;
                    if (type == 'f')
                        ind = rech_first(pro);
                    else
                        ind = rech_next(pro);
                    if (ind != -1)
                    {
                        pro.setetat('A');
                        Partition Par = zonesLibres[ind];
                        Par.SetPrecessus(pro);
                        Par.SetEtat(true);
                        int taille = Par.GetTaille();
                        if (taille != pro.gettaille())
                        {
                            Par.SetTaille(pro.gettaille());
                            Partition P = new Partition(pro.gettaille() + Par.GetAdresse(), taille - pro.gettaille(), false);
                            zonesOccupees.Add(Par);
                            zonesLibres.RemoveAt(ind);
                            zonesLibres.Add(P);
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
                zonesLibres.Add(P);
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
                zonesLibres.Remove(P);
                return true;
            }
        }

        private int rech_first(Processus Pro)
        // ROLE : Retour l'indice de la partition cherchee si elle existe. -1 sinon.
        {
            int i = 0;
            while (i != zonesLibres.Count)
            {
                if (zonesLibres[i].GetTaille() >= Pro.gettaille())
                {
                    return i;
                }
                else
                    i++;
            }
            return -1;
        }

        private int rech_next(Processus Pro)
        // ROLE : Retour l'indice de la partition cherchee si elle existe. -1 sinon.
        {
            int i = stop;
            int res = -1;
            bool trouv = false;
            while ((i != zonesLibres.Count) & (!trouv))
            {
                if (zonesLibres[i].GetTaille() >= Pro.gettaille())
                {
                    res = i;
                    trouv = true;
                    stop = i;
                }
                else
                    i++;
            }
            if ((!trouv) & (stop != 0))
            {
                i = 0;
                while ((i < stop) & (!trouv))
                {
                    if (zonesLibres[i].GetTaille() >= Pro.gettaille())
                    {
                        res = i;
                        trouv = true;
                        stop = i;
                    }
                    else
                        i++;
                }
            }
            return res;
        }
    }
}
