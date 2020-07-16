using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Cheque_Domicile_Express
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string NAN;
        public string MotDePasse;
        public string CESU;
        public int PremierCheque;
        public int DernierCheque;
        public int Millesime;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void cheque_Click(object sender, RoutedEventArgs e)
        {
            NAN = txt_nan.GetLineText(0);
            MotDePasse = txt_mdp.GetLineText(0);
            CESU = txt_cesu.GetLineText(0);
            PremierCheque = Int32.Parse(txt_premier_cheque.GetLineText(0));
            DernierCheque = Int32.Parse(txt_dernier_cheque.GetLineText(0));
            Millesime = Int32.Parse(txt_millesime.GetLineText(0));

          MessageBox.Show("NAN : " + NAN + "Mot de passe : " + MotDePasse + "CESU : " + CESU + "Premier cheque : " + PremierCheque + "Dernier cheque : " + DernierCheque + "Millesime : " + Millesime);

            IWebDriver navigateur = new ChromeDriver();
            navigateur.Navigate().GoToUrl("https://www.cheque-domicile-universel.com/espace-intervenant/startup.do");
            Thread.Sleep(3000);
            // Se  connecter 
            navigateur.FindElement(By.Name("identifiant")).SendKeys(NAN);
            navigateur.FindElement(By.Name("motpasse")).SendKeys(MotDePasse);
            navigateur.FindElement(By.Name("Valider")).Click();
            //------------------------------------------------//
            Thread.Sleep(3000);
            //------------------------------------------------//
            navigateur.FindElement(By.Name("codeCESU")).SendKeys(CESU);
            Thread.Sleep(5000);

            for (int i = PremierCheque; PremierCheque < DernierCheque+1; PremierCheque++)
            {
                navigateur.FindElement(By.Name("newCHQ")).SendKeys(PremierCheque.ToString());
                navigateur.FindElement(By.Name("millesime")).SendKeys("2020");
                navigateur.FindElement(By.Name("ajouter")).Click();
                Thread.Sleep(5000);

            }
            Actions actions = new Actions(navigateur);
            IWebElement checkbox = navigateur.FindElement(By.Name("acceptCondition"));
            actions.MoveToElement(checkbox).Perform();


            navigateur.Close();
            navigateur.Quit();

        }

    }
}
