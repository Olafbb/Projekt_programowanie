using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projekt_programowanie
{
    class Collision
    {
        //zmienne pomocniczne do określenia obszaru obiektów
        private static int PLAYER_RADIUS = 20;
        private static int METEOR_RADIUS = 20;
        private static int PROJECTILE_RADIUS = 9;
        //timer odpowiedzialny za nietykalność po otrzymaniu obrażeń
        private DispatcherTimer protectionTimer = new DispatcherTimer();
        //lista pocisków gracza
        private List<Rectangle> playerProjectilesList;
        //lista pocisków przeciwników
        private List<Rectangle> enemyProjectilesList;
        //lista przeciwników
        private List<Rectangle> enemiesList;
        //lista meteorów
        private List<Rectangle> meteorsList;
        //lista żyć (zbieralnych)
        private List<Rectangle> heartsList;
        //lista gwiazdek(dodatkowe punkty)
        private List<Rectangle> starsList;
        //gracz
        private Rectangle player;
        //Obszar okna
        private Canvas canvas;
        //klasa odpowiedzialna za ustalanie pozycji obiektów
        private Position position;
        //życia gracza
        private int lifes;
        //timer "gry"
        private DispatcherTimer gameTimer;
        //timer, który utrudnia grę co x sekund
        private DispatcherTimer impedimentTimer;
        //Okno gry
        private Window gameWindow;
        //punkty
        int scoreValue = 0;
        //konstruktor
        public Collision(Canvas canvas, List<Rectangle> playerProjectilesList, List<Rectangle> enemyProjectilesList,
            List<Rectangle> enemiesList, List<Rectangle> meteorsList, List<Rectangle> heartsList, List<Rectangle> starsList, Rectangle player,
            int lifes, DispatcherTimer gameTimer, DispatcherTimer impedimentTimer, Window gameWindow)
        {
            //przypisanie do zmiennych w klasie/ utworzenie nowych
            this.canvas = canvas;
            this.playerProjectilesList = playerProjectilesList;
            this.enemyProjectilesList = enemyProjectilesList;
            this.enemiesList = enemiesList;
            this.meteorsList = meteorsList;
            this.heartsList = heartsList;
            this.starsList = starsList;
            this.player = player;
            this.lifes = lifes;
            this.position = new Position(canvas);
            this.gameTimer = gameTimer;
            this.impedimentTimer = impedimentTimer;
            this.gameWindow = gameWindow;
        }
        public void check()
        {
            //Iteracja po liście meteorów i sprawdzenie czy nie kolidują z graczem (wpadanie gracza na meteory)
            foreach (Rectangle meteor in meteorsList)
            {
                if (METEOR_RADIUS + PLAYER_RADIUS > calculateDistance(Canvas.GetTop(player) + 50, Canvas.GetTop(meteor) + 20,
                    Canvas.GetLeft(player) + 37, Canvas.GetLeft(meteor) + 20))
                {
                    removeLife();
                    position.setNewRandomPosition(meteor);
                    scoreValue--;
                }
                //Zapętlona iteracja po liście pocisków dla każdego meteoru i sprawdzenie czy nie kolidują z meteorem (wpadanie pocisków na meteory)
                foreach (Rectangle projectile in playerProjectilesList)
                {
                    if (METEOR_RADIUS + PROJECTILE_RADIUS > calculateDistance(Canvas.GetTop(projectile) + 15, Canvas.GetTop(meteor) + 15,
                        Canvas.GetLeft(projectile) + 8, Canvas.GetLeft(meteor) + 8))
                    {
                        position.setNewRandomPosition(meteor);
                        position.rejectImage(meteor);
                        scoreValue++;
                    }
                }
            }
            //Iteracja po liście przeciwników i sprawdzenie czy nie kolidują z graczem (wpadanie gracza na przeciwników)
            foreach (Rectangle enemy in enemiesList)
            {
                if (2 * PLAYER_RADIUS > calculateDistance(Canvas.GetTop(player) + 49, Canvas.GetTop(enemy) + 49, Canvas.GetLeft(player) + 37, Canvas.GetLeft(enemy) + 49))
                {
                    removeLife();
                    position.setNewRandomPosition(enemy);
                    scoreValue--;

                }
                //Zapętlona iteracja po liście pocisków i sprawdzenie dla każdego przeciwnika czy go trafiliśmy
                foreach (Rectangle projectile in playerProjectilesList)
                {
                    if (METEOR_RADIUS + PROJECTILE_RADIUS > calculateDistance(Canvas.GetTop(projectile) + 15, Canvas.GetTop(enemy) + 49,
                        Canvas.GetLeft(projectile) + 15, Canvas.GetLeft(enemy) + 49))
                    {
                        position.setNewRandomPosition(enemy);
                        canvas.Children.Remove(projectile);
                        scoreValue++;
                    }
                }
            }
            //Iteracja po liście pocisków przeciwnika i sprawdzenie czy nie kolidują z graczem (odbieranie życia po trafieniu przez przeciwnika)
            foreach (Rectangle enemyProjectile in enemyProjectilesList)
            {
                if (PLAYER_RADIUS + PROJECTILE_RADIUS > calculateDistance(Canvas.GetTop(enemyProjectile) + 20, Canvas.GetTop(player) + 49,
                    Canvas.GetLeft(enemyProjectile) + 15, Canvas.GetLeft(player) + 49))
                {
                    position.rejectImage(enemyProjectile);
                    removeLife();
                }
            }
            //iteracja po liscie serc i sprawdzenie czy nie kolidują z graczem (zbieranie żyć)
            foreach (Rectangle heart in heartsList)
            {
                if (PLAYER_RADIUS + 49 > calculateDistance(Canvas.GetTop(heart) + 49, Canvas.GetTop(player) + 49 + 49,
                    Canvas.GetLeft(heart) + 49, Canvas.GetLeft(player) + 49))
                {
                    position.setNewRandomPosition(heart);
                    addLife();
                }
            }
            //iteracja po liscie gwiazdek i sprawdzenie czy nie kolidują z graczem (zbieranie gwiazdek)
            foreach (Rectangle star in starsList)
            {
                if (PLAYER_RADIUS + 49 > calculateDistance(Canvas.GetTop(star) + 49, Canvas.GetTop(player) + 49 + 49,
                    Canvas.GetLeft(star) + 49, Canvas.GetLeft(player) + 49))
                {
                    position.setNewRandomPosition(star);
                    scoreValue += 10;
                }
            }

        }
        //logika obliczająca czy obiekty się stykają (na podstawie ich współrzędnych)
        private double calculateDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
        //chyba nie potrzeba komentarza
        private void removeLife()
        {
            if (lifes > 0 && !protectionTimer.IsEnabled)
            {
                lifes--;
                //1.5 sekundy ochrony po otrzymaniu obrażeń
                protectionTimer.Tick += protection;
                protectionTimer.Interval = TimeSpan.FromMilliseconds(1500);
                protectionTimer.Start();
            }
            if (lifes == 0)
            {
                //koniec gry, zatrzymanie jej, stworzenie i zapisanie wyniku do pliku score.xml
                gameTimer.Stop();
                impedimentTimer.Stop();
                XmlScoreOperator scoreOperator = new XmlScoreOperator();
                scoreOperator.saveScore(new Score(scoreValue));
                MainWindow menu = new MainWindow();
                menu.Show();
                gameWindow.Close();
                MessageBox.Show("Nice! Your score:  " + scoreValue);
            }
        }
        private void protection(object sender, EventArgs e)
        {
            protectionTimer.Stop();
        }
        private void addLife()
        {
            if (lifes < 3)
            {
                lifes++;
            }
        }
        //gettery zmieniające wartości HUDa
        public int getScore()
        {
            return scoreValue;
        }
        public int getLifes()
        {
            return lifes;
        }
        public bool getProtection()
        {
            return protectionTimer.IsEnabled;
        }
    }
}
