using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class MemoirePhysiqueVirtuelle: Memoire
    {
        private int tailleContenu;
        private int nbContenu;
        private int nbContenuLibre;
        public bool MemoireVide { get; set; }

        public List<PageCase> ListMemoire { get; set; } // = new List<PageCase>();

        //Consturcteur 
        public MemoirePhysiqueVirtuelle(int tailleMemoire, int tailleContenu) : base(tailleMemoire)
        {
            this.tailleContenu = tailleContenu;
            nbContenu = tailleMemoire / tailleContenu;
            nbContenuLibre = nbContenu;
            //Créer les éléments de la liste
            ListMemoire = new List<PageCase>();
            for (int i = 0; i < nbContenu; i++)
            {
                ListMemoire.Add(new PageCase(-1));
            }
        }

        //Getters
        public int GetTailleContenu()
        {
            return tailleContenu;
        }

        public int GetNbContenu()
        {
            return nbContenu;
        }

        public int GetNbContenuLibre()
        {
            return nbContenuLibre;
        }

        public PageCase GetContenu(int i) //renvoie la page/case d'indice i (i de 0 à nbContenu)
        {
            return ListMemoire[i];
        }
        //Setters
        public void SetTailleContenu(int tailleContenu)
        {
            this.tailleContenu = tailleContenu;
        }

        public void SetNbContenu(int nbContenu)
        {
            this.nbContenu = nbContenu;
        }

        public void SetNbContenuLibre(int nbContenuLibre)
        {
            this.nbContenuLibre = nbContenuLibre;
        }

        public void Ajouter(PageCase p)
        {
            ListMemoire.Add(p);
        }

        public void AjouterAIndice(PageCase p, int indice)
        {
            ListMemoire[indice] = p;
            if (MemoireVide)
            {
                MemoireVide = false;
            }
        }
    }
}
