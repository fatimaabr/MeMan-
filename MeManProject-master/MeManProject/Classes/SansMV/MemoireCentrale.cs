using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class MemoireCentrale : MemoireDynFixe
    {
        protected List<Partition> zonesLibres;
        private int Tmax ;
        // taille de la plus grande partition

        public int getTmax() { return Tmax; }
        public void setTmax(int T) { Tmax = T; }

        public MemoireCentrale(List<int> TaillesPartitions,int tai, List<Processus> File)//creation d'une MC 'constructeur'
        {
            taille = tai;
            memoire = new SortedList<int, Partition>();
            zonesLibres = new List<Partition>();
            fileAtt = new FileAtt(File);
            Partition Par;
            int adr = 0;
            Tmax = TaillesPartitions.Max();

            foreach (int taille in TaillesPartitions)
            {
                Par = new Partition(adr, taille, false);
                memoire.Add(adr, Par);
                zonesLibres.Add(Par);
                adr = adr + taille;
            }
        }


        public override int charger(Processus pro)
            // ROLE : retourne 0 si c 'est bien effectuée en MC.
                  //  retourne 1 si elle peut etre charger en MS
                  // retourne -1 si c'est impossible de la charger ( grande taille )
        {
            if (pro.taille <= Tmax)
            {
                int taille, adr1, grandt, i = 0;
                bool b = true;
                grandt = Tmax;
                int nblpz = zonesLibres.Count;
                b = true;
                nblpz = zonesLibres.Count;
                taille = pro.gettaille();
                while ((i < nblpz) && (b))
                {
                    if (taille <= zonesLibres[i].GetTaille())
                    {
                        b = false;
                        adr1 = zonesLibres[i].GetAdresse();
                    }
                    i++;
                }
                if (b == false)
                {
                    pro.setetat('A');
                    zonesLibres[i - 1].SetPrecessus(pro);
                    zonesLibres[i - 1].SetEtat(true);
                    zonesLibres.RemoveAt(i - 1);
                }
                else
                {
                    pro.setetat('T');
                    return 1;
                    //mems.Add(pr); on a pas acces à la MS
                }

                return 0;
            }
            else
                return -1;
        }


        public override int arreter(Processus pro)
        {
            List<Partition> mem = memoire.Values.ToList<Partition>();
            Partition par = mem.Find(x => x.GetPrecessus() == pro); 
            par.SetEtat(false);
            par.SetPrecessus(null);
            zonesLibres.Add(par);
            return 0;
        }

        public FileAtt RempProcess(int nb)
        {
            int i;
            List<Processus> lpro = new List<Processus>();
            Random rand = new Random();
            for (i = 0; i < nb; i++)
            {
                char et = 'T';
                //Console.Out.WriteLine("donnez la taille de processus:");
                int taille = rand.Next(400)+1;
                //Console.Out.WriteLine("donnez le temp d'execution:");
                int temp = rand.Next(100)+1;
                //Console.Out.WriteLine("donnez le nom de processus:");
                Processus pro = new Processus("P"+(char)(i+65), taille, temp, et);
                lpro.Add(pro);
            }
            fileAtt = new FileAtt(lpro);
            return fileAtt;
        }

        public void Ajzonelibre(Partition pr)
        {
            zonesLibres.Add(pr);
        }

        public void Supzonelibre(Partition pr)
        {
            //int indice;
            zonesLibres.Remove(pr);
            //indice = zonesLibres.IndexOf(pr);
            //zonesLibres.RemoveAt(indice);
        }

        public SortedList<int, Partition> Getmemoire()
        {
            return memoire;
        }

        public List<Partition> Getzone()
        {
            return zonesLibres;
        }

        public FileAtt Getfile()
        {
            return fileAtt;
        }

        public void Setmemoire(SortedList<int, Partition> m)
        {
            memoire = m;
        } //=> C est une reference, inutile de la setter, les changements seront effectués directement sans avoir besoin d'un setter */

        public void Setzone(List<Partition> z)
        {
            zonesLibres = z;
        } 
    }
}
