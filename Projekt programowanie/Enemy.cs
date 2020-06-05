using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Projekt_programowanie
{
    class Enemy
    {
        private Random random = new Random();

        private Canvas canvas;
        private Position position;

        private List<Rectangle> enemies;
        private List<Rectangle> enemyProjectiles;
        public Enemy(Canvas canvas, List<Rectangle> enemies, List<Rectangle> enemyProjectiles)
        {
            this.canvas = canvas;
            this.position = new Position(canvas);
            this.enemies = enemies;
            this.enemyProjectiles = enemyProjectiles;
        }
        public void create(int numberOfEnemies)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Rectangle enemy = new Rectangle();
                enemy.Fill = Brushes.Red;
                enemy.Width = 40;
                enemy.Height = 40;
                enemies.Add(enemy);
                position.setNewRandomPosition(enemy);
                canvas.Children.Add(enemy);
            }
        }
        public void move()
        {
            foreach (Rectangle enemy in enemies)
            {
                Canvas.SetLeft(enemy, Canvas.GetLeft(enemy) + random.Next(10) - random.Next(10));
                Canvas.SetTop(enemy, Canvas.GetTop(enemy) + 2);
            }
        }
        public void shoot()
        {
            foreach (Rectangle enemy in enemies)
            {
                if (random.Next(150) - (random.Next(150) + 130) > 0)
                {
                    Rectangle enemyProjectile = new Rectangle();
                    enemyProjectile.Fill = Brushes.BlueViolet;
                    enemyProjectile.Width = 10;
                    enemyProjectile.Height = 40;
                    Canvas.SetTop(enemyProjectile, Canvas.GetTop(enemy) + enemyProjectile.Height);
                    Canvas.SetLeft(enemyProjectile, Canvas.GetLeft(enemy) + enemy.Width / 2);
                    enemyProjectiles.Add(enemyProjectile);
                    canvas.Children.Add(enemyProjectile);
                }
            }
        }

        public void moveProjectile()
        {
            foreach (Rectangle enemyProjectile in enemyProjectiles)
            {
                Canvas.SetTop(enemyProjectile, Canvas.GetTop(enemyProjectile) + 5);
            }
        }
    }
}
