using SFML.Graphics;
using SFML.Window;

namespace RunnerApp
{
    public class Window : RenderWindow
    {
        public Window() : base(new VideoMode(1366, 768, 24), "RunnerApp", Styles.Close)
        {
            // frame rate limit to stabilize rendering speed
            base.SetFramerateLimit(80);

            Closed += Window_Closed;
        }

        public void DrawMap()
        {
            for (int i = 0; i < Map.MapHeight; i++)
                for (int j = 0; j < Map.MapWidth; j++)
                {
                    if (Map.baseMap[i][j] == ' ') Map.mapSprite.TextureRect = new IntRect(0, 0, 32, 32); // empty square
                    if (Map.baseMap[i][j] == 'b') Map.mapSprite.TextureRect = new IntRect(32, 0, 32, 32); // brick
                    if (Map.baseMap[i][j] == 's') Map.mapSprite.TextureRect = new IntRect(64, 0, 32, 32); // stairs
                    if (Map.baseMap[i][j] == 'l') Map.mapSprite.TextureRect = new IntRect(96, 0, 32, 32);  // lamp

                    Map.mapSprite.Position = new(j * 32, i * 32);

                    base.Draw(Map.mapSprite);
                }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
