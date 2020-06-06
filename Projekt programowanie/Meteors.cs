using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Projekt_programowanie
{
    class Meteors
    {
        private Random random = new Random();
        private Canvas canvas;
        private Position position;

        private List<Rectangle> meteors;
        //konstruktor
        public Meteors(List<Rectangle> meteors, Canvas canvas)
        {
            //przypisywanie zmiennych do pól w klasie
            this.canvas = canvas;
            this.position = new Position(canvas);
            this.meteors = meteors;
        }
        //tworzenie meteorów w ilości numberOfMeteors
        public void create(int numberOfMeteors)
        {
            for (int i = 0; i < numberOfMeteors; i++)
            {
                Rectangle meteor = new Rectangle();
                meteor.Height = 20;
                meteor.Width = 20;
                meteor.RenderTransform = new RotateTransform(45);
                int a = random.Next(3);
                if (a == 0)
                    meteor.Fill = Brushes.Gray;
                else if (a == 1)
                    meteor.Fill = Brushes.Brown;
                else if (a == 2)
                {
                    meteor.Fill = Brushes.DarkGreen;
                }
                position.setNewRandomPosition(meteor);
                canvas.Children.Add(meteor);
                meteors.Add(meteor);
            }
        }

        //logika odpowiedzialna za ruch meteorów
        public void move()
        {
            foreach (Rectangle meteor in meteors)
            {
                if (meteor.Fill.Equals(Brushes.Gray))
                    Canvas.SetLeft(meteor, Canvas.GetLeft(meteor) + 1);
                if (meteor.Fill.Equals(Brushes.DarkGreen))
                    Canvas.SetLeft(meteor, Canvas.GetLeft(meteor) - 1);
                Canvas.SetTop(meteor, Canvas.GetTop(meteor) + 3);
            }
        }
    }
}
