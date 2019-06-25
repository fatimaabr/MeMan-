using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public abstract class SystLFUAging: RemplacementPage
    {
        /*ATRIBUTS*/
        public TableDePageLFUAging TablePage { get; set; }
        public int DernierePage { get; set; }

        public SystLFUAging(int tailleMV, int tailleMP, int taillePageCase) : base(tailleMP, taillePageCase)
        {
            DernierePage = -1;
            //création de la table de pages en mettant à jour le nombre d'entrées
            TablePage = new TableDePageLFUAging(tailleMV / taillePageCase);
        }

        public abstract void MettreTableAjour();

        /// <summary>
        /// La méthode retourne le numéro de page ayant le compteur le plus petit
        /// </summary>
        /// <returns></returns>
        public override int PageAReplacer()
        {
            return TablePage.CalculPagePetitCompteur();
        }

        
        /// <summary>
        /// 
        /// </summary>
        public override string DeroulerAlgorithme()
        {
            string[] arr = new string[4] ;
            int pageAR;
            //parcourir la liste de page 
            //while (ConditionContinuer())
            //Récupérer la tête de la liste entrée par l'utilisateur
            PageCase pageCourante = GetListei(0);
            //Liberer la tete de la liste
            SuppDeListe(0);
            //Sauvegarder le numéro de la page
            DernierePage = pageCourante.numeroPage;

            if (MemoirePhysique.MemoireVide)
            {
              TablePage.PagePetitCompteur = pageCourante.numeroPage;
            }
            //Mettre le Ri de la page courante à 1
            TablePage.ListeTB[pageCourante.numeroPage].Ri = 1;
            //Si la page n'est pas présente en mémoire
            if (!PageExiste(pageCourante.GetNumeroPage()))
            {   
                //*******************************************************************************************
                // a = "absent" => la page n'existe pas 
                arr[0] = "a";
                //specifier le numero de page et le rajouter dans la chaine retourner en sortie 
                String pg = Convert.ToString(pageCourante.GetNumeroPage());          
                arr[1] = pg;
                //********************************************************************************************
                //incrémenter le nombre de défauts de page
                SetDefautPages(GetDefautPage() + 1);
                //Si la mémoire physique n'est pas entièrement pleine
                if (!MemoirePleine())
                {
                    //**********************************************************************************
                    // n = "not full" => la memoire n'est pas pleine
                    arr[2] = "n";
                    //*********************************************************************************
                    //Charger la page à la première case vide
                    pageCourante.SetNumeroCase(PremiereCaseLibre());
                    //Mettre à jour le numéro de case de la page -dans la table de page-
                    TablePage.ListeTB[pageCourante.numeroPage].NumeroCase = PremiereCaseLibre();
                    RemplacerDansMemoire(pageCourante, PremiereCaseLibre());
                    //Décrémenter le nombre de cases libres en mémoire physique
                    DecCasesLibre();
                }
                else
                {
                    //*******************************************************************************
                    // f = "full" => la memoire est pleine 
                    arr[2] = "f";
                    //******************************************************************************
                    pageCourante.SetNumeroCase(PageAReplacer());
                    //Mettre à jour le numéro de case de la page -dans la table de page-
                    TablePage.ListeTB[pageCourante.numeroPage].NumeroCase = TablePage.ListeTB[PageAReplacer()].NumeroCase;
                    pageAR = PageAReplacer();
                   
                    //specifier le numero de page a remplacer et le rajouter dans la chaine retourner en sortie 
                    String pr = Convert.ToString(PageAReplacer());
                    arr[3] = pr; // arr[3] contiennt le numero de page qu'elle va etre remplacer
                    TablePage.ListeTB[pageAR].NumeroCase = -1;
                    //TablePage.ListeTB[pageAR].Compteur = 0;
                    RemplacerDansMemoire(pageCourante, TablePage.ListeTB[PageAReplacer()].NumeroCase);
                }
            }
            else
            {
                //**************************************************************************************
                // p = "present" => la page est presente
                arr[0] = "p";
                //indiquer le numero de page existante deja et la rajouter dans la chaine 
                String pag = Convert.ToString(pageCourante.GetNumeroPage());
                arr[1] = pag;
                //**************************************************************************************
            }
            string result = String.Concat(arr);
            return result ;
        }
    }
}
