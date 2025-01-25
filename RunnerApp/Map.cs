using RunnerApp.Properties;
using SFML.Graphics;
using System.Text;

namespace RunnerApp
{
    public class Map : Sprite
    {
        public const int MapHeight = 22;
        public const int MapWidth = 43;

        public static Image mapImage = new Image(Resources.map);
        public static Texture mapTexture = new Texture(mapImage);
        public static Sprite mapSprite = new Sprite(mapTexture);

        public static string[] baseMap =
        [
            "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
            "b                                         b",
            "b                                         b",
            "bbbbbbbbbbbbbbsbbbbbbbbbb                 b",
            "b             s                           b",
            "b             s                           b",
            "b             s       bbs    bbbbbbbbbbsbbb",
            "b             s       bbs              s  b",
            "b             s       bbs              s  b",
            "b             s       bbs              s  b",
            "bbbbbbbsbbbbbbb       bbbbbbbbbbbbsbbbbbbbb",
            "b      s                          s       b",
            "b      s                          s       b",
            "b      s                          s       b",
            "bbbbbbbbbbbbbbbbbsbbbbbbbbbbbbbbbbs       b",
            "b                s                s       b",
            "b                s                s       b",
            "b                s                s       b",
            "b           sbbbbbb               bbbbbbbsb",
            "b           s                            sb",
            "b           s                            sb",
            "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
        ];

        public static void RandomLampsGenerate()
        {
            int countLamp = 5;

            while (countLamp > 0)
            {
                Random rnd = new Random();
                int rndValue = rnd.Next();

                int randomLampX = 1 + rndValue % (MapWidth - 1);
                int randomLampY = 1 + rndValue % (MapHeight - 1);

                if (baseMap[randomLampY][randomLampX] == ' ')
                {
                    // replacing the space character with the lamp character
                    StringBuilder sb = new StringBuilder(baseMap[randomLampY]);
                    sb[randomLampX] = 'l';

                    string str = sb.ToString();
                    baseMap[randomLampY] = str;

                    countLamp--;
                }
            }

        }
    }
}


