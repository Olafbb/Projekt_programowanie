using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Projekt_programowanie
{
    class PlayerControls
    {
        private Canvas canvas;
        private Rectangle player1;
        private bool isLeftKeyPressed;
        private bool isRightKeyPressed;
        private bool isUpKeyPressed;
        private bool isDownKeyPressed;
        private bool isSpaceKeyPressed;
        private List<Rectangle> projectiles;
        private DispatcherTimer reloadTimer = new DispatcherTimer();
        public PlayerControls(Canvas canvas, Rectangle player1, List<Rectangle> projectiles)
        {
            this.canvas = canvas;
            this.player1 = player1;
            this.projectiles = projectiles;
            reloadTimer.Tick += reload;
            reloadTimer.Interval = TimeSpan.FromMilliseconds(500);
        }
        public void createKeyListeners()
        {
            initKeysDown();
            initKeysUp();

        }
        public void playerManeuvering()
        {
            if (isLeftKeyPressed && !isRightKeyPressed && Canvas.GetLeft(player1) > 0)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) - 12);
            }
            else if (isRightKeyPressed && !isLeftKeyPressed && Canvas.GetLeft(player1) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) + 12);
            }
            else if (isUpKeyPressed && !isDownKeyPressed && Canvas.GetTop(player1) > 0)
            {
                Canvas.SetTop(player1, Canvas.GetTop(player1) - 12);
            }
            else if (isDownKeyPressed && !isUpKeyPressed && Canvas.GetTop(player1) + 110 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player1, Canvas.GetTop(player1) + 12);
            }
            if (isLeftKeyPressed && isUpKeyPressed && Canvas.GetLeft(player1) > 0 && Canvas.GetTop(player1) > 0)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) - 7.5);
                Canvas.SetTop(player1, Canvas.GetTop(player1) - 7.5);
            }
            else if (isRightKeyPressed && isUpKeyPressed && Canvas.GetLeft(player1) + 80 < Application.Current.MainWindow.Width && Canvas.GetTop(player1) > 0)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) + 7.5);
                Canvas.SetTop(player1, Canvas.GetTop(player1) - 7.5);
            }
            else if (isRightKeyPressed && isDownKeyPressed && Canvas.GetLeft(player1) + 80 < Application.Current.MainWindow.Width && Canvas.GetTop(player1) + 110 < Application.Current.MainWindow.Height)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) + 7.5);
                Canvas.SetTop(player1, Canvas.GetTop(player1) + 7.5);
            }
            else if (isLeftKeyPressed && isDownKeyPressed && Canvas.GetLeft(player1) > 0 && Canvas.GetTop(player1) + 110 < Application.Current.MainWindow.Height)
            {
                Canvas.SetLeft(player1, Canvas.GetLeft(player1) - 7.5);
                Canvas.SetTop(player1, Canvas.GetTop(player1) + 7.5);
            }
            if (isSpaceKeyPressed &&!reloadTimer.IsEnabled)
            {
                reloadTimer.Start();
                Rectangle playerProjectile = new Rectangle();
                playerProjectile.Tag = "projectile";
                playerProjectile.Height = 20;
                playerProjectile.Width = 5;
                playerProjectile.Fill = Brushes.Yellow;
                Canvas.SetTop(playerProjectile, Canvas.GetTop(player1) - playerProjectile.Height);
                Canvas.SetLeft(playerProjectile, Canvas.GetLeft(player1) + player1.Width / 2);
                canvas.Children.Add(playerProjectile);
                projectiles.Add(playerProjectile);
            }
        }
        private void initKeysDown()
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                isLeftKeyPressed = true;
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                isRightKeyPressed = true;
            }
            if (Keyboard.IsKeyDown(Key.Up))
            {
                isUpKeyPressed = true;
            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                isDownKeyPressed = true;
            }
            if (Keyboard.IsKeyDown(Key.Space))
            {
                isSpaceKeyPressed = true;
            }
        }
        private void initKeysUp()
        {
            if (Keyboard.IsKeyUp(Key.Left))
            {
                isLeftKeyPressed = false;
            }
            if (Keyboard.IsKeyUp(Key.Right))
            {
                isRightKeyPressed = false;
            }
            if (Keyboard.IsKeyUp(Key.Up))
            {
                isUpKeyPressed = false;
            }
            if (Keyboard.IsKeyUp(Key.Down))
            {
                isDownKeyPressed = false;
            }
            if (Keyboard.IsKeyUp(Key.Space))
            {
                isSpaceKeyPressed = false;
            }
        }
        private void reload(object sender, EventArgs e)
        {
            reloadTimer.Stop();
        }
    }
}
