using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Projekt_programowanie
{
    class PlayerProjectiles
    {
        private List<Rectangle> projectiles;
        public PlayerProjectiles(List<Rectangle> projectiles)
        {
            this.projectiles = projectiles;
        }
        public void move()
        {
            foreach (Rectangle projectile in projectiles)
            {
                Canvas.SetTop(projectile, Canvas.GetTop(projectile) - 20);
            }
        }
    }
}
