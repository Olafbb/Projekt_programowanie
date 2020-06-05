﻿using System;
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
    class Star
    {
        private Position position;
        private Canvas canvas;
        ImageBrush starImage = new ImageBrush();
        private List<Rectangle> stars;
        public Star(Position position, Canvas canvas, List<Rectangle> stars)
        {
            this.position = position;
            this.canvas = canvas;
            this.stars = stars;
            starImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/star_model.png"));
        }
        public void create(int numberOfStars)
        {
            for (int i = 0; i < numberOfStars; i++)
            {
                Rectangle star = new Rectangle();
                star.Width = 20;
                star.Height = 20;
                star.Fill = starImage;
                position.setNewRandomPosition(star);
                canvas.Children.Add(star);
                stars.Add(star);
            }
        }
        public void move()
        {
            foreach (Rectangle star in stars)
            {
                Canvas.SetTop(star, Canvas.GetTop(star) + 5);
            }
        }
    }
}
