using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class MemMultiFile
    {
        private MemoireFixe memoire;
        private List<FileAtt> ListeFile;
        // Constructeur 
        public MemMultiFile(MemoireFixe Mem, int Nbpartition)
        {
            memoire = Mem;
            ListeFile = new List<FileAtt>();
            int NbrPro = Mem.fileAtt.File.Count;
            Processus Pro;

            for (int i = 0; i < Nbpartition; i++)// creer "Nbpartition" de files vides
            {
                FileAtt Newfile = new FileAtt();
                Newfile.tai = Mem.GetMemoire().Values[i].taille;
                ListeFile.Add(Newfile);
            }

            for (int i = 0; i < NbrPro; i++) // repartir la liste de processus dans leurs files 
            {
                Pro = Mem.fileAtt.defiler();
                AjouterProces(Pro);
            }
        }
        // Methode pour ajouter un processus et le mettre dans la file qui lui covient 
        public int AjouterProces(Processus proc)
            //return -1 si le processus est trop grand : ne peut pas etre chargé en memoire
        {
            if (proc.taille > memoire.getTmax())
            {
                return -1;
            }
            else
            {
                int TailleMemoire = memoire.GetMemoire().Count;
                SortedList<int, Partition> LaMemoire = memoire.GetMemoire();
                int position = 0;
                int taillePartiton = LaMemoire.Values[0].GetTaille();

                for (int i = 0; i < TailleMemoire; i++)// chercher la meilleure partition depuis la taille 
                {
                    if ((taillePartiton > LaMemoire.Values[i].GetTaille()) && (proc.gettaille() <= LaMemoire.Values[i].GetTaille()))
                    {
                        taillePartiton = LaMemoire.Values[i].GetTaille();
                        position = i;
                    }
                }
                ListeFile[position].Enfiler(proc);

                /*if (position >= 0)
                {
                    if (proc.gettaille() <= LaMemoire.Values[position].GetTaille())
                        ListeFile[position].Enfiler(proc);
                    else
                        return -1;
                }
                else
                    return -1;*/
                return 0;
            }

        }
        // Methode pour Charger un processus dans une la partition qui lui covient 
        public void ChargerProc(FileAtt File, Partition part) // ?
        {
            Processus proc = File.defiler();
            memoire.charger(proc, part);
        }


        public List<FileAtt> GetListeFile()
        {
            return ListeFile;
        }

        public MemoireFixe Getmemoire()
        {
            return memoire;
        }

    }
}
