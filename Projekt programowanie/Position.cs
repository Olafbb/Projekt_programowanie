using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Projekt_programowanie
{
    class Position
    {
        private Random random = new Random();
        private Canvas canvas;
        public Position(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void setNewRandomPosition(Rectangle image)
        {
            Canvas.SetTop(image, random.Next(-100, 0));
            Canvas.SetLeft(image, random.Next(100, 700));
        }

        public void rejectImage(Rectangle image)
        {
            canvas.Children.Remove(image);
        }

        public void checkAndRelocateGameElements(int height, List<Rectangle> playerProjectilesList, List<Rectangle> meteors, List<Rectangle> enemies, List<Rectangle> enemiesProjectileList, List<Rectangle> heartsList)
        {
            foreach (Rectangle meteor in meteors)
            {
                if (Canvas.GetTop(meteor) > height)
                    setNewRandomPosition(meteor);
            }
            foreach (Rectangle enemy in enemies)
            {
                if (Canvas.GetTop(enemy) > height | Canvas.GetLeft(enemy) > Application.Current.MainWindow.Width | Canvas.GetLeft(enemy) < 0)
                    setNewRandomPosition(enemy);
            }
            foreach (Rectangle projectile in playerProjectilesList)
            {
                if (Canvas.GetTop(projectile) < 0)
                {
                    rejectImage(projectile);
                }
            }
            foreach (Rectangle enemyProjectile in enemiesProjectileList)
            {
                if (Canvas.GetTop(enemyProjectile) > height)
                {
                    rejectImage(enemyProjectile);
                }
            }
            foreach (Rectangle heart in heartsList)
            {
                if (Canvas.GetTop(heart) > height)
                {
                    setNewRandomPosition(heart);
                }
            }
        }
    }
}
