using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class SystemeLFU : SystLFUAging
    {
        /// <summary>
        /// Constucteur
        /// </summary>
        /// <param name="tailleMV">Taille de la mémoire virtuelle</param>
        /// <param name="tailleMP">Taille de la mémoire physique</param>
        /// <param name="taillePageCase">Taille de la Page/Case</param>
        public SystemeLFU(int tailleMV, int tailleMP, int taillePageCase) : base(tailleMV, tailleMP, taillePageCase) { }


        /// <summary>
        /// Mettre à jour la table de page: méthode appelée à chaque top d'horloge
        /// </summary>
        public override void MettreTableAjour()
        {
            //Incrémenter les compteurs avec Ri à 1
            if (DernierePage!=-1)
            {
                TablePage.ListeTB[DernierePage].Ri = 0;
                TablePage.ListeTB[DernierePage].Compteur++;
            }
        }
    }
}
