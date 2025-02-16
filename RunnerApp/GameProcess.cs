using RunnerApp.Enemies;
using RunnerApp.Properties;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace RunnerApp
{
    public class GameProcess
    {
        private Window window2D = new Window();

        private Player player = new Player(new Image(Resources.player), (650, 600));
        private Sound takeLamp = new Sound();
        private Sound throwChain = new Sound();

        GreenEnemy[] greenEnemy = new GreenEnemy[3];

        private Clock clock = new Clock();
        private double time;

        public GameProcess()
        {
            Map.RandomLampsGenerate();

            player.TakingLampEvent += Taking_LampEvent;
            player.ThrowingChainEvent += Throwing_ChainEvent;

            SoundBuffer lampBuffer = new SoundBuffer(Resources.take_lamp);
            takeLamp.SoundBuffer = lampBuffer;

            SoundBuffer chainBuffer = new SoundBuffer(Resources.throw_chain);
            throwChain.SoundBuffer = chainBuffer;

            GenerateGreenEnemies(new Image(Resources.greenEnemy));
        }

        private void Taking_LampEvent(object? sender, EventArgs e)
        {
            takeLamp.Play();
        }

        private void Throwing_ChainEvent(object? sender, EventArgs e)
        {
            throwChain.Play();
        }

        private void GenerateGreenEnemies(Image image)
        {
            for (int i = 0; i < greenEnemy.Length; i++)
            {
                greenEnemy[i] = new GreenEnemy(image, GetRandomCoordinates());
            }
        }

        private (double, double) GetRandomCoordinates()
        {
            int coordX = 0;
            int coordY = 0;

            bool done = false;

            while (done == false)
            {
                Random rnd = new Random();
                int rndValue = rnd.Next();

                coordX = 1 + rndValue % (Map.MapWidth - 1);
                coordY = 1 + rndValue % (Map.MapHeight - 1);

                if (Map.baseMap[coordY][coordX] == ' ' || Map.baseMap[coordY][coordX] == 's')
                {
                    coordX *= 32;
                    coordY *= 32;

                    done = true;
                }
            }
            return (coordX, coordY);
        }

        public void Run()
        {
            while (window2D.IsOpen)
            {
                time = clock.ElapsedTime.AsMilliseconds();
                clock.Restart();

                window2D.DispatchEvents();

                window2D.Clear();

                window2D.DrawMap();
                window2D.DrawText(player.score, player.health);

                player.Animate(time);
                window2D.Draw(player.sprite);
                player.Update(time);

                foreach (var enemy in greenEnemy)
                {
                    window2D.Draw(enemy.sprite);
                    enemy.Update(time);
                }

                window2D.Display();
            }
        }
    }
}
