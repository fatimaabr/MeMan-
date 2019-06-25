using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class MemUneFile
    {
        private MemoireFixe memoire;
        //Constructeur
        public MemUneFile(MemoireFixe memoire)
        {
            this.memoire = memoire;
        }

        public MemoireFixe Getmemoire()
        {
            return memoire;
        }
        // Methode FirstFit
      /*  public void FirstFit()
        {
            Processus proc;
            List<Partition> libre = memoire.GetPartLibre();

            //if (libre != null && memoire.GetFile() != null) // verifier si la file principale et la liste des zones libres ne sont pas vide
            {
                proc = memoire.GetFile().defiler();
                memoire.charger(proc, libre[0]); // charger la premiere partition vide par le processus 
            } 
        }*/

        public int FirstFit(Processus proc) // Correction
            // ROLE : Retourne 0 si le chargement du processus 'Proc' s'est bien effectué, 1 si la partition est occuppée. -1 SI c estimpossible de le charger en memoire (Taille > la plus grande partition ).
        {
            List<Partition> libre = memoire.GetPartLibre();
            if ( proc.taille > memoire.getTmax())
            {
                return -1;
            }
            else
            {
                if (libre.Count != 0)// verifier si la liste des zones libres n'est pas vide
                {
                    bool trouv = false;
                    int i = 0;
                    while ((trouv == false) & (i < libre.Count))
                    {
                        if (proc.gettaille() <= libre[i].GetTaille())
                            trouv = true;
                        else
                            i++;
                    }
                    if (trouv == false)
                        return 1;
                    else
                        memoire.charger(proc, libre[i]); // charger la premiere partition vide par le processus 
                }
                else
                    return 1;
                return 0;
            }
        }

        // Methode bestFit
        /*public void BestFit()
        {
            Processus proc;
            List<Partition> libre = memoire.GetPartLibre();
            if ((libre.Count != 0) && (memoire.GetFile().File.Count != 0)) // verifier si la file principale et la liste des zones libres ne sont pas vide
            {
                proc = memoire.GetFile().defiler();
                Partition best = libre[0];
                for (int i = 0; i < libre.Count; i++) // parcourir la liste des zones libres pour chercher la meilleure partition proche a la taille du processus 
                {
                    if (best.GetTaille() > libre[i].GetTaille() && proc.gettaille() <= libre[i].GetTaille())
                    {
                        best = libre[i];
                    }
                }
                if (best.GetTaille() >= proc.gettaille()) memoire.charger(proc, best); // verifier si la taille de la partiton "best" est superieure a la taille du processus (dans lecas ou il n'a pas trouvé de partition) 
            }

        }*/

        public int BestFit(Processus proc)// Corrigé
            // Proc est le processus issue de la file d'attente
        {
            if (proc.taille > memoire.getTmax())
            {
                return -1;
            }
            else
            {
                List<Partition> libre = memoire.GetPartLibre();
                if (libre.Count != 0) // verifier si la liste des zones libres n'est pas vide
                {
                    Partition best = libre[0];
                    for (int i = 0; i < libre.Count; i++) // parcourir la liste des zones libres pour chercher la meilleure partition proche a la taille du processus 
                    {
                        if ((best.GetTaille() > libre[i].GetTaille()) && (proc.gettaille() <= libre[i].GetTaille())) // tjs non verifié lors de la 1ere itération psk 1 = 1
                        {
                            best = libre[i];
                        }
                    }
                    if (best.GetTaille() >= proc.gettaille())
                        memoire.charger(proc, best); // verifier si la taille de la partiton "best" est superieure a la taille du processus (dans lecas ou il n'a pas trouvé de partition) 
                    else
                        return 1;
                }
                else
                    return 1;
                return 0;
            }
        }

        // Methode worstFit
        /*  public void WorstFit()
          {
              Processus proc;
              List<Partition> libre = memoire.GetPartLibre();
              if ((libre.Count != 0) && (memoire.GetFile().File.Count != 0))// verifier si la file principale et la liste des zones libres ne sont pas vide
              {
                  proc = memoire.GetFile().defiler();
                  Partition worst = libre[0];
                  for (int i = 0; i < libre.Count; i++)// parcourir la liste des zones libres pour chercher la meilleure partition proche a la taille du processus
                  {
                      if (worst.GetTaille() < libre[i].GetTaille() && proc.gettaille() <= libre[i].GetTaille())
                      {
                          worst = libre[i];
                      }
                  }
                  if (worst.GetTaille() >= proc.gettaille()) memoire.charger(proc, worst); // verifier si la taille de la partiton "worst" est superieure a la taille du processus (dans lecas ou il n'a pas trouvé de partition)
              }

          }*/

        public int WorstFit(Processus proc)
        {
            if (proc.taille > memoire.getTmax())
            {
                return -1;
            }
            else
            {
                List<Partition> libre = memoire.GetPartLibre();
                if (libre.Count != 0)// verifier si la liste des zones libres n'est pas vide
                {
                    Partition worst = libre[0];
                    for (int i = 0; i < libre.Count; i++)// parcourir la liste des zones libres pour chercher la meilleure partition proche a la taille du processus
                    {
                        if (worst.GetTaille() < libre[i].GetTaille() && proc.gettaille() <= libre[i].GetTaille())
                        {
                            worst = libre[i];
                        }
                    }
                    if (worst.GetTaille() >= proc.gettaille())
                        memoire.charger(proc, worst); // verifier si la taille de la partiton "worst" est superieure a la taille du processus (dans le cas ou il n'a pas trouvé de partition)
                    else
                        return 1;
                }
                else
                    return 1;
                return 0;
            }
        }
    }
}
