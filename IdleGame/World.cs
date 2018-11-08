namespace IdleGame {
    class World {

        public int radius;

        public int MineralPatchCount = 5;
        public WorldPoint[] MineralPoints = new WorldPoint[0];
        private int mineralPointCount = 0;

        public World(int radius, int MineralPatchCount) {
            MineralPoints = new WorldPoint[this.MineralPatchCount];
            this.radius = radius;
        }

        public void SetMineralPoint(int x, int y) {
            MineralPoints[mineralPointCount] = new WorldPoint(x, y);
            mineralPointCount++;
        }

    }
}
