using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame {
    class World {

        public int radius;

        public int MAX_MINERALPOINTS = 5;
        public WorldPoint[] MineralPoints = new WorldPoint[0];
        private int mineralPointCount = 0;

        public World(int radius, int MAX_MINERALPOINTS) {
            MineralPoints = new WorldPoint[this.MAX_MINERALPOINTS];
            this.radius = radius;
        }

        public void SetMineralPoint(int x, int y) {
            MineralPoints[mineralPointCount] = new WorldPoint(x, y);
            mineralPointCount++;
        }

    }
}
