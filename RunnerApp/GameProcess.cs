namespace RunnerApp
{
    public class GameProcess
    {
        private readonly Window window2D = new Window();

        public GameProcess()
        {

        }

        public void Run()
        {
            while (window2D.IsOpen == true)
            {

                // Обработка очереди событий
                window2D.DispatchEvents();

                // Показываем кадр всего что подготовлено
                window2D.Display();
            }
        }
    }
}
