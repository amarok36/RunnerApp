using RunnerApp.Properties;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace RunnerApp
{
    public class GameProcess
    {
        private Window window2D = new Window();
        private Player player = new Player(new Image(Resources.player), 650, 600);

        private Clock clock = new Clock();
        private double time;

        public GameProcess()
        {
            Map.RandomLampsGenerate();
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

                window2D.Display();
            }
        }
    }
}
