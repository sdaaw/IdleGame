using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame {
    class WorldPoint {
        public int x = 0;
        public int y = 0;

        public int minerals = 500;

        public WorldPoint(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public override string ToString() {
            return "(" + x + ", " + y + ") Minerals: " + minerals;
        }

    }
}
