using System;
using System.Collections;
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
using System.Windows.Threading;

namespace Projekt_programowanie
{
    public partial class GameWindow : Window
    {
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private DispatcherTimer impedimentTimer = new DispatcherTimer();
        private PlayerControls controls;
        private List<Rectangle> enemyProjectilesList = new List<Rectangle>();
        private List<Rectangle> enemiesList = new List<Rectangle>();
        private List<Rectangle> meteorsList = new List<Rectangle>();
        private List<Rectangle> playerProjectilesList = new List<Rectangle>();
        private List<Rectangle> heartList = new List<Rectangle>();
        private List<Rectangle> starsList = new List<Rectangle>();
        private PlayerProjectiles playerProjectiles;
        private Enemy enemy;
        private Meteors meteors;
        private Heart heart;
        private Star star;
        private Position position;
        private Collision collision;
        public GameWindow()
        {
            InitializeComponent();
            controls = new PlayerControls(myCanvas, player, playerProjectilesList);
            playerProjectiles = new PlayerProjectiles(playerProjectilesList);
            position = new Position(myCanvas);
            collision = new Collision(myCanvas, playerProjectilesList, enemyProjectilesList, enemiesList, meteorsList, heartList, starsList, player, 3, gameTimer, impedimentTimer, this);
            enemy = new Enemy(myCanvas, enemiesList, enemyProjectilesList);
            meteors = new Meteors(meteorsList, myCanvas);
            heart = new Heart(position, myCanvas, heartList);
            star = new Star(position, myCanvas, starsList);

            gameTimer.Tick += gameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(10);
            gameTimer.Start();

            impedimentTimer.Tick += impediment;
            impedimentTimer.Interval = TimeSpan.FromMilliseconds(5000);
            impedimentTimer.Start();

            enemy.create(2);
            meteors.create(5);
            heart.create(1);
            star.create(1);
        }
        private void gameEngine(object sender, EventArgs e)
        {
            controls.createKeyListeners();
            controls.playerManeuvering();
            playerProjectiles.move();
            enemy.shoot();
            enemy.moveProjectile();
            meteors.move();
            position.checkAndRelocateGameElements(550, playerProjectilesList, meteorsList, enemiesList, enemyProjectilesList, heartList);
            collision.check();
            enemy.move();
            heart.move();
            star.move();
            if (collision.getProtection())
            {
                HUD.Content = "Score: " + collision.getScore() + " Life: " + collision.getLifes() + " Enemies: " + enemiesList.Count + " PROTECTION! ";
            }
            else
            {
                HUD.Content = "Score: " + collision.getScore() + " Life: " + collision.getLifes() + " Enemies: " + enemiesList.Count;
            }
        }
        private void impediment(object sender, EventArgs e)
        {
            enemy.create(1);
            meteors.create(1);
        }
    }
}
