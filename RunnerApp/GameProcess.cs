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

        GreenEnemy[] greenEnemies = new GreenEnemy[5];

        private Clock clock = new Clock();
        private double time;

        private Sound takeLamp = new Sound();
        private Sound throwChain = new Sound();
        private Sound collisionEnemy = new Sound();

        TextInformation? gameStatus;

        public GameProcess()
        {
            Map.RandomLampsGenerate();

            player.TakingLampEvent += Taking_LampEvent;
            player.ThrowingChainEvent += Throwing_ChainEvent;
            player.СollideEnemyEvent += Сollide_EnemyEventEvent;

            SoundBuffer lampBuffer = new SoundBuffer(Resources.take_lamp);
            takeLamp.SoundBuffer = lampBuffer;

            SoundBuffer chainBuffer = new SoundBuffer(Resources.throw_chain);
            throwChain.SoundBuffer = chainBuffer;

            SoundBuffer collisonBuffer = new SoundBuffer(Resources.player_damage);
            collisionEnemy.SoundBuffer = collisonBuffer;

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

        private void Сollide_EnemyEventEvent(object? sender, EventArgs e)
        {
            collisionEnemy.Play();
        }

        private (double, double) GetRandomCoordinates()
        {
            int coordX = 0;
            int coordY = 0;
            int rndValue;

            bool done = false;

            while (done == false)
            {
                Random rnd = new Random();

                rndValue = rnd.Next();
                coordX = 1 + rndValue % (Map.MapWidth - 1);

                rndValue = rnd.Next();
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

        private void GenerateGreenEnemies(Image image)
        {
            for (int i = 0; i < greenEnemies.Length; i++)
            {
                greenEnemies[i] = new GreenEnemy(image, GetRandomCoordinates());
            }
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
                window2D.DrawText(player.GetScore(), player.GetHealth());

                player.Animate(time);
                window2D.Draw(player.sprite);
                player.Update(time);

                foreach (var enemy in greenEnemies)
                {
                    enemy.Animate(time);
                    window2D.Draw(enemy.sprite);
                    enemy.Update(time);
                }

                player.CollisionsWithGreenEnemies(greenEnemies);

                if (!player.life)
                    GameOver();

                window2D.Display();
            }
        }

        private void GameOver()
        {
            string gameOverText = "GAME OVER";
            Vector2f textPosition = new(620, 320);

            gameStatus = new TextInformation(gameOverText, textPosition);

            for (int i = 0; i < greenEnemies.Length; i++)
            {
                greenEnemies[i].StopMove();
            }

            window2D.Clear();
            window2D.Draw(gameStatus.text);
        }
    }
}
