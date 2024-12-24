using RunnerApp.Properties;
using SFML.Graphics;
using SFML.Window;

namespace RunnerApp
{
    public class Window : RenderWindow
    {

        private readonly Image mapImage; // объект изображения для карты
        private readonly Texture mapTexture; // текстура карты
        private readonly Sprite mapSprite; // спрайт

        public Window() : base(new VideoMode(1366, 768, 24), "RunnerApp")
        {
            mapImage = new Image(Resources.map); // подгружаем тайл карты из ресурсов
            mapTexture = new Texture(mapImage); // заряжаем текстуру картинкой
            mapSprite = new Sprite(mapTexture); // заливаем спрайт текстурой

            // ограничение частоты фреймов для стабилизации скорости рендеринга
            base.SetFramerateLimit(80);

            Closed += Window_Closed;
        }

        // рисуем карту
        public void DrawMap()
        {
            for (int i = 0; i < Map.MapHeight; i++)
                for (int j = 0; j < Map.MapWidth; j++)
                {
                    if (Map.TileMap[i][j] == ' ') mapSprite.TextureRect = new IntRect(0, 0, 32, 32); // если встретился пробел, рисуем пустой квадратик
                    if (Map.TileMap[i][j] == 'b') mapSprite.TextureRect = new IntRect(32, 0, 32, 32); // если встретилось b, рисуем кирпичную стену
                    if (Map.TileMap[i][j] == 's') mapSprite.TextureRect = new IntRect(64, 0, 32, 32); // если встретилось s, рисуем лестницу
                    if (Map.TileMap[i][j] == 'l') mapSprite.TextureRect = new IntRect(96, 0, 32, 32);  // если встретиось l, рисуем лампу

                    mapSprite.Position = new(j * 32, i * 32); // раскидываем тайлы, превращая в карту (задаем каждому из них позицию)

                    base.Draw(mapSprite); // рисуем тайлы на экране
                }
        }

        // зыкрываем окно
        private void Window_Closed(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
