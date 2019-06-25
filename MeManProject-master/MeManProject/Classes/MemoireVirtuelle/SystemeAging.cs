using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class SystemeAging: SystLFUAging
    {
        const uint v = 0b_10000000;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="tailleMV">Taille de la mémoire virtuelle</param>
        /// <param name="tailleMP">Taille de la mémoire physique</param>
        /// <param name="taillePageCase">Taille de la page/case</param>
        public SystemeAging(int tailleMV, int tailleMP, int taillePageCase) : base(tailleMV, tailleMP, taillePageCase) { }

        /// <summary>
        /// Mettre à jour la table de page: méthode appelée à chaque top d'horloge
        /// </summary>
        public override void MettreTableAjour()
        {
            int j = 0;
            if (DernierePage != -1)
            {
                while (j < TablePage.nbEntrees)
                {
                    //décalage du compteur d'une position vers la droite
                    TablePage.ListeTB[j].Compteur = TablePage.ListeTB[j].Compteur / 2;
                    j++;
                }
                //Pour la dernière page utilisée, mettre à jour le compteur en ajoutant Ri au bit de poids fort
                TablePage.ListeTB[DernierePage].Compteur = TablePage.ListeTB[DernierePage].Compteur | v; //ou logique avec "10000000"
                //Remettre Ri à 0
                TablePage.ListeTB[DernierePage].Ri = 0;
            }
        }
    }
}
