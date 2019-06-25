using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeManProject.Classes
{
    public class GestionTextBox
    {
        //Méthode permettant de valider que les chiffres lors du remplissage d'une TextBox
        public void ValiderNombresTB(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
