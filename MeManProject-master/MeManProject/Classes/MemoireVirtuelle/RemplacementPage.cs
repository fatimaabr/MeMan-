using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public abstract class RemplacementPage
    {
        public MemoirePhysiqueVirtuelle MemoirePhysique { get; set; }
        private int defautPages = 0;
        public List<PageCase> ListeUtilisateur { get; set; }

        //Contructeur
        public RemplacementPage(int tailleMemoire, int tailleCase)
        {
            ListeUtilisateur = new List<PageCase>();

            MemoirePhysique = new MemoirePhysiqueVirtuelle(tailleMemoire, tailleCase);
        }


        public List<PageCase> copy()
        {
            List<PageCase> cp = new List<PageCase>();
            for (int i = 0; i < ListeUtilisateur.Count; i++)
            {
                cp.Add(this.ListeUtilisateur[i].copy());
            }
            return cp;
        }


        //Getters
        public int GetTailleListeUtilisateur()
        {
            return ListeUtilisateur.Count; //Retourner le nombre de page en attente dans la liste de l'utilisateur
        }
        public int GetDefautPage()
        {
            return defautPages;
        }
        //Récupérer l'élément d'indice i de la liste d'utilisateur
        public PageCase GetListei(int i)
        {
            return ListeUtilisateur[i];
        }
        //Setters
        public void SetDefautPages(int defautPages)
        {
            this.defautPages = defautPages;
        }
        public void AjouterAListe(PageCase p)
        {
            ListeUtilisateur.Add(p);
        }

        public void SuppDeListe(int i)
        {
            ListeUtilisateur.RemoveAt(i);
        }

       

        public int PremiereCaseLibre() //retourne l'indice de la première case libre
        {
            return (MemoirePhysique.GetNbContenu() - MemoirePhysique.GetNbContenuLibre());
        }
        //Autres
        public Boolean PageExiste(int ip)
        {
            int i = 0;
            //parcours de la mémoire physique
            while (i < (MemoirePhysique.GetNbContenu() - MemoirePhysique.GetNbContenuLibre()))
            {
                if ((MemoirePhysique.GetContenu(i)).GetNumeroPage() == ip)
                {
                    return true;
                }
                else i++;
            }
            return false;
        }

        public Boolean MemoirePleine()
        {
            if (MemoirePhysique.GetNbContenuLibre() == 0)
            {
                return true;
            }
            else
                return false;
        }

        public void DecCasesLibre() //Décrémenter le nombre de cases libres en mémoire physique
        {
            MemoirePhysique.SetNbContenuLibre(MemoirePhysique.GetNbContenuLibre() - 1);
        }

        public void RemplacerDansMemoire(PageCase p, int position)
        {
            MemoirePhysique.AjouterAIndice(p, position);
        }

        public bool ConditionContinuer()
        {
            return (GetTailleListeUtilisateur() != 0);
        }
        //Methode abstraite qui sera implémentée d'après la technique utilisée (FIFO, LRU, NFU, Aging)
        //Rôle: retourne le numéro de la case qui doit être remplacée dans la mémoire physique
        public abstract int PageAReplacer();

        //déroulement entier de l'algorithme
        public abstract string DeroulerAlgorithme();
    }
}
