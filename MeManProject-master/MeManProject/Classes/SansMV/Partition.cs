using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeManProject
{
    [Serializable]
    public class Partition : IComparable<Partition>
    {
        public Processus process { get; set; }
        public int adresse { get; set; }
        public int taille { get; set; }
        public bool etat { get; set; }  // true -> occupée
                            // false -> vide

        //Constructeur
        public Partition(int adresse, int taille, bool etat)
        {
            this.taille = taille;
            this.adresse = adresse;
            this.etat = etat; //  si etat = vrai => alors la partition est occuppee. Sinon, elle est libre.
        }

        public int CompareTo(Partition Par)
        {
            if (Par == null)
                return 1;

            else
                return this.taille.CompareTo(Par.taille);
        }
        //Setters
        public void SetTaille(int taille)
        {
            this.taille = taille;
        }
        public void SetAdresse(int adresse)
        {
            this.adresse = adresse;
        }
        public void SetEtat(bool etat)
        {
            this.etat = etat;
        }

        public void SetPrecessus(Processus process)
        {
            this.process = process;
        }

        //Geteur
        public int GetTaille()
        {
            return taille;
        }
        public int GetAdresse()
        {
            return adresse;
        }
        public bool GetEtat()
        {
            return etat;
        }

        public Processus GetPrecessus()
        {
            return process;
        }

        public void afficher()
        {
            if (etat == false) // libre
                Console.WriteLine("Partition de taille : " + taille + " situé à l'adresse " + adresse + " . Elle est libre.");
            else // occuppee
            {
                Console.Write("Partition de taille : " + taille + " situé à l'adresse " + adresse + " . Elle est occuppee par : ");
                process.afficher();
            }
        }
    }
}
