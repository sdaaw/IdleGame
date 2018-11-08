using System;
using System.Linq;
using System.Threading;


namespace IdleGame {
    class Program {
        public static int WorkerCount = 0;
        public static int Workers = 10; //arbitrary number

        public static Worker[] currentWorkers = new Worker[Workers];

        public static World world;

        static void Main(string[] args) {
            InitGame();
        }

        public static void InitGame() {

            GenerateWorld(15, 5); //arbitrary numbers

            for (int i = 0; i < Workers; i++) {
                CreateWorker();
            }
            GenerateMinerals(world.MineralPatchCount);
            Game();
        }

        public static void CreateWorker() {
            Random r = new Random();
            Worker w = new Worker(WorkerCount, r.Next(0, world.radius), r.Next(0, world.radius));
            Thread.Sleep(100); //sleep to reset RNG
            currentWorkers[WorkerCount] = w;
            WorkerCount++;
        }

        public static void GenerateWorld(int radius, int MAX_MINERALPOINTS) {
            world = new World(radius, MAX_MINERALPOINTS);
        }


        public static void PrintInfo() { //printing info :)
            Console.WriteLine("Worker count: " + currentWorkers.Count());
            Console.WriteLine("World radius: " + world.radius + "x" + world.radius);
            Console.WriteLine("Mineral patches: ");
            foreach (WorldPoint mp in world.MineralPoints) {
                Console.WriteLine(mp.ToString());
            }
            Console.WriteLine("");
            Console.WriteLine("Workers: ");
            foreach (Worker w in currentWorkers) {
                Console.WriteLine(w.ToString());
            }
            int totalMinerals = 0;
            foreach (WorldPoint mp in world.MineralPoints) {
                totalMinerals += mp.minerals;
            }
            Console.WriteLine("");
            Console.WriteLine("Total Minerals on the field: " + totalMinerals);
        }

        public static void GenerateMinerals(int amount) {
            int nx = 0;
            int ny = 0;
            Random r = new Random();
            for (int i = 0; i < amount; i++) {
                int rx = r.Next(1, world.radius - 1);
                int ry = r.Next(1, world.radius - 1);
                world.SetMineralPoint(rx, ry);
                //world.MineralPoints[i].minerals = 10; //for fast win
                // ========================
                // this segment makes sure multiple mineralpatches dont spawn on top of eachother(have same coordinates)
                // if theres no room for all the mineral points, it will run forever
                //
                for (int j = 0; j < i; j++) {
                    if (rx == world.MineralPoints[j].x && ry == world.MineralPoints[j].y) {
                        while (nx == world.MineralPoints[j].x && ny == world.MineralPoints[j].y) {
                            nx = r.Next(1, world.radius - 1);
                            ny = r.Next(1, world.radius - 1);
                            world.SetMineralPoint(nx, ny);
                        }
                    }
                }
            }
        }

        public static void Game() {
            int totalMinerals = 0;
            int totalTicks = 0;
            Random r = new Random();
            int mx = r.Next(-1, 2);
            int my = r.Next(-1, 2);

            while (true) {

                PrintInfo();
                foreach (WorldPoint mp in world.MineralPoints) {
                    totalMinerals += mp.minerals; 
                }

                foreach (Worker w in currentWorkers) {
                    mx = r.Next(-1, 2);
                    my = r.Next(-1, 2);
                    w.Move(mx, my);
                    Thread.Sleep(20); //small sleep to reset rng
                }
                if (totalMinerals == 0) { //win?
                    break;
                }
                totalTicks++;
                Console.Clear();
                totalMinerals = 0; //reset to see the new actual total in the following tick
            }
            Console.WriteLine("GAME FINISHED");
            Console.WriteLine("Game ticks elapsed: " + totalTicks);
        }
    }
}
