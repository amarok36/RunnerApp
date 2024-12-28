using RunnerApp.Properties;
using SFML.Graphics;
using SFML.System;

namespace RunnerApp
{
    public class GameProcess
    {
        private Window window2D = new Window();

        private Player player = new Player(new Image(Resources.player), 650, 600);

        private Clock clock = new Clock(); // привязка ко времени, а не к загруженности процессора
        private float time;
       
        public GameProcess()
        {
            Map.RandomLampsGenerate();
        }

        // запуск игры
        public void Run()
        {
            while (window2D.IsOpen)
            {
               // время в микросекундах
                time = clock.ElapsedTime.AsMilliseconds();
                clock.Restart();

                // обработка очереди событий
                window2D.DispatchEvents();

                // рисуем карту
                window2D.DrawMap();

                // рисуем спрайт игрока
                window2D.Draw(player.sprite);
                player.Update(time);

                // показываем кадр всего, что готово
                window2D.Display();
            }
        }
    }
}
