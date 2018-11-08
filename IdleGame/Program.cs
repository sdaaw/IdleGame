using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace IdleGame {
    class Program {
        public static int WorkerCount = 0;
        public static readonly int MAX_WORKERS = 10;

        public static Worker[] currentWorkers = new Worker[MAX_WORKERS];

        public static World world;

        static void Main(string[] args) {
            InitGame();
        }

        public static void InitGame() {
            GenerateWorld(15, 5);

            for (int i = 0; i < 10; i++) {
                CreateWorker();
            }
            GenerateMinerals(world.MAX_MINERALPOINTS);
            Game();
        }

        public static void CreateWorker() {
            Random r = new Random();
            Worker w = new Worker(WorkerCount, r.Next(0, world.radius), r.Next(0, world.radius));
            Thread.Sleep(100);
            currentWorkers[WorkerCount] = w;
            WorkerCount++;
        }

        public static void GenerateWorld(int radius, int MAX_MINERALPOINTS) {
            world = new World(radius, MAX_MINERALPOINTS);
        }


        public static void PrintInfo() {
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
        }

        public static void GenerateMinerals(int amount) {
            int nx = 0;
            int ny = 0;
            Random r = new Random();
            for (int i = 0; i < amount; i++) {
                int rx = r.Next(1, world.radius - 1);
                int ry = r.Next(1, world.radius - 1);
                world.SetMineralPoint(rx, ry);
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

                foreach (Worker w in currentWorkers) {
                    mx = r.Next(-1, 2);
                    my = r.Next(-1, 2);
                    w.Move(mx, my);
                    Thread.Sleep(20);
                }
                Thread.Sleep(10); //represents a game tick
                foreach (WorldPoint mp in world.MineralPoints) {
                    totalMinerals += mp.minerals;
                }
                if (totalMinerals < 0) {

                    break;

                }
                totalTicks++;
                Console.Clear();
            }
            Console.WriteLine("GAME FINISHED");
            Console.WriteLine("Game ticks elapsed: " + totalTicks);
        }
    }
}
