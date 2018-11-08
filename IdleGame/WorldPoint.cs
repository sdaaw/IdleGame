namespace IdleGame {
    class WorldPoint {
        public int x = 0;
        public int y = 0;

        public int minerals = 500; //500 by default if not set 

        public bool isMineralPatch;

        public WorldPoint(int x, int y) {
            this.x = x;
            this.y = y;
            if (minerals > 0) isMineralPatch = true;
        }

        public override string ToString() {
            string s = "(" + x + ", " + y + ")";
            if(isMineralPatch) s += " Minerals: " + minerals;
            return s;
        }

    }
}
