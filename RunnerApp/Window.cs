using RunnerApp.Properties;
using SFML.Graphics;
using SFML.Window;

namespace RunnerApp
{
    public class Window : RenderWindow
    {

        private readonly Image mapImage; // объект изображения для карты
        private readonly Texture map; // текстура карты
        private readonly Sprite s_map; // спрайт

        public Window() : base(new VideoMode(1366, 768, 24), "RunnerApp")
        {
            mapImage = new Image(Resources.map); // подгружаем тайл карты из ресурсов
            map = new Texture(mapImage); // заряжаем текстуру картинкой
            s_map = new Sprite(map); // заливаем текстуру спрайтом

            // ограничение частоты фреймов для стабилизации скорости рендеринга сюжета игры
            base.SetFramerateLimit(80);

            Closed += Window_Closed;
        }

        // рисуем карту
        public void DrawMap()
        {
            for (int i = 0; i < Map.HEIGHT_MAP; i++)
                for (int j = 0; j < Map.WIDTH_MAP; j++)
                {
                    if (Map.TileMap[i][j] == ' ') s_map.TextureRect = new IntRect(0, 0, 32, 32); // если встретили пробел, то рисуем 1й тайл (квадратик) 
                    if (Map.TileMap[i][j] == 'b') s_map.TextureRect = new IntRect(32, 0, 32, 32); // если встретили s, то рисуем 2й тайл
                    if (Map.TileMap[i][j] == 's') s_map.TextureRect = new IntRect(64, 0, 32, 32); // если встретили 0, то рисуем 3й тайл

                    s_map.Position = new(j * 32, i * 32); // раскидываем тайлы, превращая в карту (задаем каждому из них позицию)

                    base.Draw(s_map); // рисуем тайлы на экран
                }
        }

        // зыкрываем окно
        private void Window_Closed(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
