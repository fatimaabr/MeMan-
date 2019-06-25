using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class TableDePageLFUAging
    {
        //Liste des lignes de la table de pages
        public List<LigneTBLFUAging> ListeTB { get; set; }
        public int nbEntrees { get; set; }
        public int PagePetitCompteur { get; set; }

        //Constructeur
        public TableDePageLFUAging(int nbEntrees)
        {
            this.nbEntrees = nbEntrees;
            PagePetitCompteur = -1;
            ListeTB = new List<LigneTBLFUAging>();
            LigneTBLFUAging ligne;
            for (int i = 0; i < nbEntrees; i++)
            {
                ligne = new LigneTBLFUAging(i);
                ListeTB.Add(ligne);
            }
        }

        public int CalculPagePetitCompteur()
        {
            int plusPetit = -1;
            int pageCorrespondante=-1;
            int i = 0;
            while (i < nbEntrees)
            {
                if (ListeTB[i].NumeroCase != -1)
                {
                    if (plusPetit == -1)
                    {
                        plusPetit = Convert.ToInt32(ListeTB[i].Compteur);
                        pageCorrespondante = ListeTB[i].IndicePage;
                    }
                    else if (plusPetit > Convert.ToInt32(ListeTB[i].Compteur))
                    {
                        plusPetit = Convert.ToInt32(ListeTB[i].Compteur);
                        pageCorrespondante = ListeTB[i].IndicePage;
                    }

                }
                i++;
            }
            if (pageCorrespondante != -1)
            {
                this.PagePetitCompteur = pageCorrespondante;
                return pageCorrespondante;
            }
            else
            {
                return PagePetitCompteur;
            }
        }

        


    }
}
