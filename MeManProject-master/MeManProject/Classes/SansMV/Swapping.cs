using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class Swapping
    {
        private MemoireCentrale MC;
        private MemoireSecondaire MS;

        public Swapping(List<int> ListeTaillePartitions,int tailleMC , List<Processus> File)
        {
            MC = new MemoireCentrale(ListeTaillePartitions, tailleMC, File);
            Ordzonelib();
            MS = new MemoireSecondaire();
        }

        public void Rempmc(FileAtt file, int nbprocess)
        {
            Processus pr;
            int taille, adr1, grandt, i = 0;
            SortedList<int, Partition> mem = MC.Getmemoire();
            List<Partition> lpz = MC.Getzone();
            List<Processus> mems = new List<Processus>();
            bool b = true;
            Partition par = lpz.Last();
            grandt = par.GetTaille();
            int nblpz = lpz.Count;
            while (nbprocess != 0)
            {
                i = 0;
                b = true;
                nblpz = lpz.Count;
                pr = file.defiler();
                taille = pr.gettaille();
                while ((i < nblpz) && (b))
                {
                    if (taille <= lpz[i].GetTaille())
                    {
                        b = false;
                        adr1 = lpz[i].GetAdresse();
                    }
                    i++;
                }
                if (b == false)
                {
                   // k = 0;
                    /*adr1 = lpz[i - 1].GetAdresse(); //  cest exactement adr1 de 42
                    k = mem.IndexOfKey(adr1);
                    adr2 = mem.Values[k].GetAdresse();
                    while (adr2 != adr1)
                    {
                        k++;
                        adr2 = mem.Values[k].GetAdresse();
                    }*/
                    pr.setetat('A');
                    lpz[i - 1].SetPrecessus(pr);
                    lpz[i - 1].SetEtat(true);
                    lpz.RemoveAt(i - 1);
                }
                else
                {
                    if (taille <= grandt)
                    {
                        pr.setetat('T');
                        mems.Add(pr);
                    }
                    else
                    {
                        Console.WriteLine("le processus " + pr.getnom() + " ne peut pas etre execute car il est tres grand");
                    }
                }
                nbprocess--;
            }
            MS = new MemoireSecondaire(mems);
        }

        public void Rempmsmc(int indicemc, int indicems)
        {
            Processus p = MS.Recupprocess(indicems);
            SortedList<int, Partition> lp = MC.Getmemoire();
            p.setetat('A');
            lp[indicemc].SetPrecessus(p);
            lp[indicemc].SetEtat(true);
            //MC.Setmemoire(lp);
        }

        public void EntrSrt(Partition par)
        {
            SortedList<int, Partition> lpar = MC.Getmemoire();
            List<Processus> lpro = MS.Getmemsec();
            Processus pr;
            int indice = lpar.IndexOfValue(par);
            pr = lpar.Values[indice].GetPrecessus();
            lpar.Values[indice].SetEtat(false);
            pr.setetat('B');
            lpro.Add(pr);
            //MC.setmemoire(lpar); => Tu as changer directement les objets via les references
            //MS.Setmemsec(lpro);
        }

        public void FentSrt(Processus p)
        {
            List<Processus> lpro = MS.Getmemsec();
            int indice = lpro.IndexOf(p);
            if (lpro[indice].getetat() == 'B')
            {
                lpro[indice].setetat('T');
                //MS.Setmemsec(lpro);
            }
            else { Console.WriteLine("le processus " + lpro[indice].getnom() + " n'attend pas une E/S"); }
        }

        public void Ordzonelib()
        {

            List<Partition> lpz = MC.Getzone();
            lpz.Sort();/*
            List<Partition> l1 = new List<Partition>();
            List<Partition> l2 = MC.Getzone();
            Partition pr, prpetit;
            int petit = int.MaxValue;
            int cpt1, cpt2, taille, indice = 0, nbite2 = l2.Count;
            for (cpt1 = 0; cpt1 < l2.Count; cpt1++)
            {
                nbite2 = l2.Count;
                petit = int.MaxValue;
                for (cpt2 = 0; cpt2 < nbite2; cpt2++)
                {
                    pr = l2[cpt2];
                    taille = pr.GetTaille();
                    if (taille <= petit)
                    {
                        petit = taille;
                        indice = cpt2;
                    }
                }
                prpetit = l2[indice];
                l2.RemoveAt(indice);
                l1.Add(prpetit);
            }
            MC.Setzone(l1);*/
        }

        public int Rechzonlib(Processus p)
        {
            List<Partition> lz = MC.Getzone();
            SortedList<int, Partition> mem = MC.Getmemoire();
            int taille = p.gettaille();
            int taille1, taille2 = lz.Count;
            bool trouv = true;
            int position = 0;
            while (position < taille2 && trouv)
            {
                taille1 = lz[position].GetTaille();
                if (taille <= taille1)
                { trouv = false; }
                position++;
            }
            position -= 1;
            if (trouv == true)
            { position = -1; }
            return position;
        }

        public MemoireCentrale Getmc()
        {
            return MC;
        }

        public MemoireSecondaire Getms()
        {
            return MS;
        }
    }
}
