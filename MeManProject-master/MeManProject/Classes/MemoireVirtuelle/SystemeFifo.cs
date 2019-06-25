using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace MeManProject
{
    [Serializable]
    public class SystemeFifo : RemplacementPage
    {
        private int time;

        public Queue<PageCase> FileFIFO { get; set; }
        public SystemeFifo(int tailleMemoire, int tailleCase) : base(tailleMemoire, tailleCase)
        {
            FileFIFO = new Queue<PageCase>();
        }

        //Implémentation des méthodes abstraites
        public override int PageAReplacer()
        {
            //Defiler la file (Récupérer la page qui a été entrée en premier) et renvoyer son emplacement en mémoire physique en sortie de la méthode
            return FileFIFO.Peek().GetNumeroCase();
            throw new NotImplementedException();
        }

        public override string DeroulerAlgorithme()
        {
            //temps d'attente entre les instructions de l'algorithme

            //Tant que la file entrée par l'utilisateur n'est pas terminée
            //while (base.GetTailleListeUtilisateur() != 0)
            string[] arr = new string[4];
            Console.WriteLine(time++);
            //Récupérer la tête de la liste entrée par l'utilisateur
            PageCase pageCourante = GetListei(0);
            //Liberer la tete de la liste
            SuppDeListe(0);
            //Vérifier que la page est déjà en mémoire physique
            if (!PageExiste(pageCourante.GetNumeroPage()))
            {
                //*******************************************************************************************
                // a = "absent" => la page n'existe pas 
                arr[0] = "a";
                //specifier le numero de page et le rajouter dans la chaine retourner en sortie 
                String pg = Convert.ToString(pageCourante.GetNumeroPage());
                arr[1] = pg;
                //********************************************************************************************
                SetDefautPages(GetDefautPage() + 1);
                //Enfiler la pageCourante
                FileFIFO.Enqueue(pageCourante);
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

                    //**************************************************************************************************
                    //specifier le numero de page a remplacer et le rajouter dans la chaine retourner en sortie 
                    String pr = Convert.ToString(PageAReplacer());
                    arr[3] = pr; // arr[3] contiennt le numero de page qu'elle va etre remplacer
                    //**************************************************************************************************

                    //Defiler la page remplacer
                    FileFIFO.Dequeue();
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
            return result;
        }
    }
}
