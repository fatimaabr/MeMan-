using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class PageCase
    {
        private static int taille;                  //Taille de la page/case en Ko
        public int numeroPage { get; set; }         //Numero de la page 
        private int numeroCase = 0;                 //Numero de la case (si la page est en mémoire centrale) sinon à 0

        //Constructeur
        public PageCase(int numeroPage)
        {
            this.numeroPage = numeroPage;

        }

        //Getters
        public int GetTaillePC()
        {
            return taille;
        }

        public int GetNumeroPage()
        {
            return numeroPage;
        }

        public int GetNumeroCase()
        {
            return numeroCase;
        }

        //Setters
        static public void SetTaille(int tailleEntree)
        {
            taille = tailleEntree;
        }

        public void SetNumeroPage(int numeroPage)
        {
            this.numeroPage = numeroPage;
        }

        public void SetNumeroCase(int numeroCase)
        {
            this.numeroCase = numeroCase;
        }

        public PageCase copy()
        {
            return (PageCase)this.MemberwiseClone();
        }
    }
}
