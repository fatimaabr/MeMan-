using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeManProject
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new WindowViewModel(this);
        }
        #region Méthodes de navigation vers une page en particulier
        public void OuvrirMenu()
        {
            PageMenuSimulations PageMenu = new PageMenuSimulations();
            this.Content = PageMenu;
        }

        public void OuvrirFIFO()
        {
            SimulationFIFO PageMenu = new SimulationFIFO();
            this.Content = PageMenu;
        }

        public void OuvrirLRU()
        {
            SimulationLRU PageMenu = new SimulationLRU();
            this.Content = PageMenu;
        }

        public void OuvrirLFU()
        {
            SimulationLFU PageMenu = new SimulationLFU();
            this.Content = PageMenu;
        }

        public void OuvrirAging()
        {
            SimulationAging PageMenu = new SimulationAging();
            this.Content = PageMenu;
        }

        public void OuvrirUneFile()
        {
            SimulationUneFile PageMenu = new SimulationUneFile();
            this.Content = PageMenu;
        }

        public void OuvrirPlusFiles()
        {
            SimulationPlusFiles PageMenu = new SimulationPlusFiles();
            this.Content = PageMenu;
        }

        public void OuvrirSwap()
        {
            SimulationSwap PageMenu = new SimulationSwap();
            this.Content = PageMenu;
        }

        public void OuvrirFirstFit()
        {
            SimulationFirstFit PageMenu = new SimulationFirstFit();
            this.Content = PageMenu;
        }

        public void OuvrirNextFit()
        {
            SimulationNextFit PageMenu = new SimulationNextFit();
            this.Content = PageMenu;
        }

        public void OuvrirBestFit()
        {
            SimulationBestFit PageMenu = new SimulationBestFit();
            this.Content = PageMenu;
        }

        public void OuvrirWorstFit()
        {
            SimulationWorstFit PageMenu = new SimulationWorstFit();
            this.Content = PageMenu;
        }

        public void OuvrirBuddyFit()
        {
            SimulationBuddySystem PageMenu = new SimulationBuddySystem();
            this.Content = PageMenu;
        }
        #endregion
    }
}
