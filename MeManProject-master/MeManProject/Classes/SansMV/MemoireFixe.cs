using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class MemoireFixe : MemoireDynFixe
    {

        protected List<Partition> zonesLibres;
        //private int NbPartition;
        private int Tmax;
        // taille de la plus grande partition

        public int getTmax() { return Tmax; }

        //Constructeur 
        public MemoireFixe(int tailleMem, List<int> partitions, List<Processus> File)
        {
            taille = tailleMem;
            memoire = new SortedList<int, Partition>();
            zonesLibres = new List<Partition>();
            fileAtt = new FileAtt(File);
            int[] tab = partitions.ToArray();
            int adresse = 0;
            Tmax = tab.Max();
            for (int i = 0; i < tab.Length; i++)
            {
                Partition part = new Partition(adresse, tab[i], false);
                memoire.Add(part.GetAdresse(),part);
                zonesLibres.Add(part);
                adresse = adresse + tab[i];
            }
        }

        public MemoireFixe(int tailleMem, int NbPartition)
        {
            taille = tailleMem;
            memoire = new SortedList<int, Partition>();
            zonesLibres = new List<Partition>();
            int[] tab = GetUniformPartition(tailleMem, NbPartition);
            int adresse = 0;
            Tmax = tab.Max();
            for (int i = 0; i < tab.Length; i++)
            {
                Partition part = new Partition(adresse, tab[i], false);
                memoire.Add(part.GetAdresse(), part);
                zonesLibres.Add(part);
                adresse = adresse + tab[i];
            }

        }

        public int getTaille() { return taille; }

        // Methode Pour Charger un processus dans une partition 
        public override int charger(Processus pro) // charge par default ? Laquelle ?
        {
           /* if (pro.gettaille() <= par.GetTaille())
            {
                par.SetPrecessus(pro);
                par.SetEtat(true);
                zonesLibres.Remove(par);
                return 0;
            }
            else*/
                return -1;

        }

        public void charger(Processus pro, Partition par)// Void ?
        {
            if (pro.gettaille() <= par.GetTaille())
            {
                par.SetPrecessus(pro);
                par.SetEtat(true);
                zonesLibres.Remove(par);
            }
        }

        // Methode Pour arreter un processus dansune Partiton 
        public override int arreter(Processus pro)
        {
            List<Partition> mem = memoire.Values.ToList<Partition>();
            Partition par = mem.Find(x => x.GetPrecessus() == pro);
            par.SetEtat(false);
            par.SetPrecessus(null);
            zonesLibres.Add(par);
            return 0;
        }

        public int arreter (Partition par)
        {
            par.SetEtat(false);
            par.SetPrecessus(null);
            zonesLibres.Add(par);
            return 0;
        }
        // Methode Pour ajouter une partition 
   /*     public void AjoutPart(Partition part) //LA taille de la memoire n est pas constante ??
        {
            NbPartition = NbPartition + 1;
            memoire.Add(part.GetAdresse(), part);
            zonesLibres.Add(part);
            taille = taille + part.GetTaille(); 

        }*/
        // Les Getters
        public SortedList<int, Partition> GetMemoire()
        {
            return memoire;
        }
        public List<Partition> GetPartLibre()
        {
            return zonesLibres;
        }
        public FileAtt GetFile()
        {
            return fileAtt;
        }

        // Seteur 
        public void SetFile(FileAtt newfile)
        {
            fileAtt = newfile;

        }

        //Partie pour la genration aleatoire de la memoire 
        private Random random = new Random();
        private Boolean contientDoublons(int[] arr)
        {
            var dict = new Dictionary<int, int>(); // realisable aussi avec une liste
            foreach (int count in arr)
            {
                if (dict.ContainsKey(count))
                    return true;
                else
                    dict[count] = 1;  // The indexer can be used to change the value associated with a key. -L
            }
            return false;
        }

        private int[] GetUniformPartition(int input, int parts)
            // input = taille de la memoire, parts = Nbr des partition -L
        {
            if (input <= 0 || parts <= 0)
                throw new ArgumentException("invalid input or parts");
            if (input < MinUniformPartition(parts))
                throw new ArgumentException("input is too small");

            int[] partition = new int[parts];
            int sum = 0;
            for (int i = 0; i < parts - 1; i++)
            {
                int max = input - MinUniformPartition(parts - i - 1) - sum;
                partition[i] = random.Next(parts - i, max);
                sum += partition[i];
            }
            partition[parts - 1] = input - sum; // last
            return partition;
        }

        // sum of 1,2,3,4,..,n
        private int MinUniformPartition(int n)
        {
            return n * n - 1;
        }
    }
}
