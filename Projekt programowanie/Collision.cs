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

        private static int PLAYER_RADIUS = 20;
        private static int METEOR_RADIUS = 20;
        private static int PROJECTILE_RADIUS = 9;

        private DispatcherTimer protectionTimer = new DispatcherTimer();
        private List<Rectangle> playerProjectilesList;
        private List<Rectangle> enemyProjectilesList;
        private List<Rectangle> enemiesList;
        private List<Rectangle> meteorsList;
        private List<Rectangle> heartsList;
        private List<Rectangle> starsList;
        private Rectangle player;
        private Canvas canvas;
        private Position position;
        private int lifes;
        private DispatcherTimer gameTimer;
        private DispatcherTimer impedimentTimer;
        private Window gameWindow;
        int scoreValue = 0;

        public Collision(Canvas canvas, List<Rectangle> playerProjectilesList, List<Rectangle> enemyProjectilesList,
            List<Rectangle> enemiesList, List<Rectangle> meteorsList, List<Rectangle> heartsList, List<Rectangle> starsList, Rectangle player,
            int lifes, DispatcherTimer gameTimer, DispatcherTimer impedimentTimer, Window gameWindow)
        {
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
            //Iteracja po liście meteorów i dla ich współrzędnych sprawdzenie czy nie kolidują z graczem (wpadanie gracza na meteory)
            foreach (Rectangle meteor in meteorsList)
            {
                if (METEOR_RADIUS + PLAYER_RADIUS > calculateDistance(Canvas.GetTop(player) + 50, Canvas.GetTop(meteor) + 20,
                    Canvas.GetLeft(player) + 37, Canvas.GetLeft(meteor) + 20))
                {
                    removeLife();
                    position.setNewRandomPosition(meteor);
                    scoreValue--;
                }
                //Zapętlona iteracja po liście pocisków dla każdego meteoru i dla ich współrzędnych sprawdzenie czy nie kolidują z meteorem (wpadanie pocisków na meteory)
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
            //to samo co z meteorami
            foreach (Rectangle enemy in enemiesList)
            {
                if (2 * PLAYER_RADIUS > calculateDistance(Canvas.GetTop(player) + 49, Canvas.GetTop(enemy) + 49, Canvas.GetLeft(player) + 37, Canvas.GetLeft(enemy) + 49))
                {
                    removeLife();
                    position.setNewRandomPosition(enemy);
                    scoreValue--;

                }
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
            foreach (Rectangle enemyProjectile in enemyProjectilesList)
            {
                if (PLAYER_RADIUS + PROJECTILE_RADIUS > calculateDistance(Canvas.GetTop(enemyProjectile) + 20, Canvas.GetTop(player) + 49,
                    Canvas.GetLeft(enemyProjectile) + 15, Canvas.GetLeft(player) + 49))
                {
                    position.rejectImage(enemyProjectile);
                    removeLife();
                }
            }
            foreach (Rectangle heart in heartsList)
            {
                if (PLAYER_RADIUS + 49 > calculateDistance(Canvas.GetTop(heart) + 49, Canvas.GetTop(player) + 49 + 49,
                    Canvas.GetLeft(heart) + 49, Canvas.GetLeft(player) + 49))
                {
                    position.setNewRandomPosition(heart);
                    addLife();
                }
            }
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
        //kalkulacja dystansu między dwoma obiektami za pomocą współrzędnych
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
                protectionTimer.Tick += protection;
                protectionTimer.Interval = TimeSpan.FromMilliseconds(1500);
                protectionTimer.Start();
            }
            if (lifes == 0)
            {
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
