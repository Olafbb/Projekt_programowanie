using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt_programowanie
{
    class Heart
    {
        private Position position;
        private Canvas canvas;
        ImageBrush heartImage = new ImageBrush();
        private List<Rectangle> hearts;
        //konstruktor
        public Heart(Position position, Canvas canvas, List<Rectangle> hearts)
        {
            //przypisywanie zmiennych do pól w klasie / tworzenie nowych instancji
            this.position = position;
            this.canvas = canvas;
            this.hearts = hearts;
            //przypisanie obrazka do tworzonych obiektów
            heartImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/heart_model.png"));
        }
        //tworzenie przeciwników w ilości numberOfHearts
        public void create(int numberOfHearts)
        {
            for (int i = 0; i < numberOfHearts; i++)
            {
                Rectangle heart = new Rectangle();
                heart.Width = 20;
                heart.Height = 20;
                heart.Fill = heartImage;
                position.setNewRandomPosition(heart);
                canvas.Children.Add(heart);
                hearts.Add(heart);
            }
        }
        //logika odpowiedzialna za ruch serc
        public void move()
        {
            foreach (Rectangle heart in hearts)
            {
                Canvas.SetTop(heart, Canvas.GetTop(heart) + 2);
            }
        }
    }
}
