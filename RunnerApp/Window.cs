using SFML.Graphics;
using SFML.Window;

namespace RunnerApp
{
    public class Window : RenderWindow
    {
        public Window() : base(new VideoMode(1024, 768, 24), "RunnerApp")
        {
            // Ограничение частоты фреймов для стабилизации скорости 
            // рендеринга сюжета игры
            base.SetFramerateLimit(80);
        }

    }
}
