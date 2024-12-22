namespace RunnerApp
{
    public class GameProcess
    {
        private readonly Window window2D = new Window();

        public GameProcess()
        {
            // пока пустой конструктор
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

                // gоказываем кадр всего что подготовлено
                window2D.Display();
            }
        }
    }
}
