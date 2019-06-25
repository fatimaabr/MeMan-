using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class FileAtt
    {
        public List<Processus> File { get; set; }
        public int tai {get; set; }

         public FileAtt(List<Processus> LP) // constructeur
         {
             File = new List<Processus>();
             foreach (Processus ps in LP)
             {
                 File.Add(ps);
             }
         } 
        public FileAtt()
        {
            File = new List<Processus>();
        }

        public FileAtt copy()
        {
            FileAtt cp = new FileAtt();
            for (int i = 0; i < File.Count; i++)
            {
                cp.File.Add(this.File[i].copy());
            }
            return cp;
        }

        public Processus defiler()  // recuperer le 1er element de la file
        {
            if (File.Count == 0)
                return null;
            else
            {
                Processus P;
                P = File[0];      // le recuperer
                File.RemoveAt(0);  // le supprimer de la file
                return P;
            }
        }

        public Processus ElementFile(int index)  // retourne l'element de la file dont l'index est 'index' pour pouvoir acceder aux differents attribut de cet element
        {
            return File[index];
        }

        public int Enfiler(Processus p)   // Ajoute un element a la file ( a la fin )
        {
            if (p != null)
            {
                File.Add(p);
                return 0;    // enfilement executé avec succés 
            }
            else
                return -1;
        }

        public int ModiferEtat(Processus P, char etatP)  // modifie l'etat d'un processus
        {
            int indexP;
            indexP = File.IndexOf(P);   // recuperer l'index de P
            File[indexP].setetat(etatP); // changer l'etat de P ( le setter fais le travail ) 
            return 0;  // modification effectuée avec succés
        }

        public void afficherFile()
        {
            foreach (Processus ps in File)
            {
                ps.afficher();
            }
        }
    }
}
