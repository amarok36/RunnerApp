namespace RunnerApp
{
    public class GameProcess
    {
        private readonly Window window2D = new Window();

        public GameProcess()
        {
            Map.RandomLampsGenerate();
        }

        // запуск игры
        public void Run()
        {
            while (window2D.IsOpen == true)
            {
                // обработка очереди событий
                window2D.DispatchEvents();

                // рисуем карту
                window2D.DrawMap();

                // показываем кадр всего, что подготовлено
                window2D.Display();
            }
        }
    }
}
