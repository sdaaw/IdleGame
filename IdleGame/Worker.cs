using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame {
    class Worker {
        private int id;
        private int exp;
        private int speed = 5; //takes 5 gameticks to move 1 square
        public int x;
        public int y;
        private int mineralsCarrying = 0;

        public Worker(int id, int x, int y) {
            this.x = x;
            this.y = y;
            this.id = id;
        }

        public override string ToString() {
            return "(" + x + ", " + y + ") | Minerals: " + mineralsCarrying;
        }

        public void Move(int distancex, int distancey) {

            for (int i = 0; i < Program.world.MineralPoints.Count(); i++) {
                if (x == Program.world.MineralPoints[i].x && y == Program.world.MineralPoints[i].y) {
                    if (Program.world.MineralPoints[i].minerals > 0) {
                        mineralsCarrying += 10;
                        Program.world.MineralPoints[i].minerals -= 10;
                    }
                }
            }

            if (x < Program.world.radius && x > 0) {
                x += distancex;
            }
            if (y < Program.world.radius && y > 0) {
                y += distancey;
            }

            if (y == 0) {
                y += 1;
            }
            if (x == 0) {
                x += 1;
            }
            if (x == Program.world.radius) {
                x -= 1;
            }
            if (y == Program.world.radius) {
                y -= 1;
            }

        }

    }
}
