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

namespace Projekt_programowanie
{
    //menu
    public partial class MainWindow : Window
    {
        //konstruktor
        public MainWindow()
        {
            InitializeComponent();
        }
        //logiki guzików w menu
        private void startClick(object sender, RoutedEventArgs e)
        {
            GameWindow game = new GameWindow();
            game.Show();
            this.Close();
        }
        private void scoresClick(object sender, RoutedEventArgs e)
        {
            ScoreWindow scores = new ScoreWindow();
            scores.Show();
        }
        private void exitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
