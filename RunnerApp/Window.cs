using SFML.Graphics;
using SFML.Window;

namespace RunnerApp
{
    public class Window : RenderWindow
    {
        public Window() : base(new VideoMode(1366, 768, 24), "RunnerApp")
        {
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
                    if (Map.baseMap[i][j] == ' ') Map.mapSprite.TextureRect = new IntRect(0, 0, 32, 32); // если встретился пробел, рисуем пустой квадратик
                    if (Map.baseMap[i][j] == 'b') Map.mapSprite.TextureRect = new IntRect(32, 0, 32, 32); // если встретилось b, рисуем кирпичную стену
                    if (Map.baseMap[i][j] == 's') Map.mapSprite.TextureRect = new IntRect(64, 0, 32, 32); // если встретилось s, рисуем лестницу
                    if (Map.baseMap[i][j] == 'l') Map.mapSprite.TextureRect = new IntRect(96, 0, 32, 32);  // если встретиось l, рисуем лампу

                    Map.mapSprite.Position = new(j * 32, i * 32); // раскидываем тайлы, превращая в карту (задаем каждому из них позицию)

                    base.Draw(Map.mapSprite); // рисуем тайлы на экране
                }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
