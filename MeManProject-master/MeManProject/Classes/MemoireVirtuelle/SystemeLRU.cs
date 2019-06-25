using System;
using System.Collections.Generic;

namespace MeManProject
{
    [Serializable]
    public class SystemeLRU : RemplacementPage
    {
        //List<PageCase> listeLRU = new List<PageCase>();
        public List<PageCase> ListeLRU { get; set; }

        public SystemeLRU(int tailleMemoire, int tailleCase) : base(tailleMemoire, tailleCase)
        {
            ListeLRU = new List<PageCase>();
        }

        public override int PageAReplacer()
        {
            //Récupérer l'emplacement en mémoire physique de la page en queue de liste -la moins récemment utilisée-
            int i = ListeLRU[ListeLRU.Count - 1].GetNumeroCase();
            return i;
            throw new NotImplementedException();
        }

        public override string DeroulerAlgorithme()
        {
            //Tant que la file entrée par l'utilisateur n'est pas terminée
            //while (base.GetTailleListeUtilisateur() != 0)

            //Récupérer la tête de la liste entrée par l'utilisateur
            PageCase pageCourante = GetListei(0);
            //Liberer la tete de la liste
            SuppDeListe(0);
            string[] arr = new string[4];
            if (PageExiste(pageCourante.GetNumeroPage()))
            {
                //*******************************************************************************************
                // p = "present" => la page existe deja 
                arr[0] = "p";
                //specifier le numero de page et le rajouter dans la chaine retourner en sortie 
                String pg = Convert.ToString(pageCourante.GetNumeroPage());
                arr[1] = pg;
                //********************************************************************************************
                /*Supprimer la page de la liste LRU*/
                //Récupérer son indice dans la liste
                ListeLRU.Remove(pageCourante);
                //Inserer la page courante en tête de listei
                ListeLRU.Insert(0, pageCourante);
            }
            else 
            {

                // a = "absent" => la page n'existe pas
                arr[0] = "a";
                //indiquer le numero de page existante deja et la rajouter dans la chaine 
                String pag = Convert.ToString(pageCourante.GetNumeroPage());
                arr[1] = pag;

                //Inserer la page courante en tête de liste
                ListeLRU.Insert(0, pageCourante);
                SetDefautPages(GetDefautPage() + 1);
                //Si la mémoire physique n'est pas entièrement remplie
                if (!MemoirePleine())
                {
                    //**********************************************************************************
                    // n = "not full" => la memoire n'est pas pleine
                    arr[2] = "n";
                    //*********************************************************************************
                    //Charger la page à la première case vide
                    pageCourante.SetNumeroCase(PremiereCaseLibre());
                    //ajouterAMemoire(pageCourante);
                    RemplacerDansMemoire(pageCourante, PremiereCaseLibre());
                    //Décrémenter le nombre de cases libres en mémoire physique
                    DecCasesLibre();
                }
                //Si la mémoire physique est entièrement remplie
                else
                {
                    //*******************************************************************************
                    // f = "full" => la memoire est pleine 
                    arr[2] = "f";
                    //******************************************************************************
                    //utiliser l'algorithme de remplacement FIFO
                    pageCourante.SetNumeroCase(PageAReplacer());
                    RemplacerDansMemoire(pageCourante, PageAReplacer());

                    String pr = Convert.ToString(PageAReplacer());
                    arr[3] = pr; // arr[3] contiennt le numero de page qu'elle va etre remplacer
              
                    //Supprimer la page de la liste
                    ListeLRU.RemoveAt(ListeLRU.Count - 1);

                }
            }
            string result = String.Concat(arr);
            return result;

        }
    }
}
                                              