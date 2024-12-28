using RunnerApp.Properties;
using SFML.Graphics;
using System.Text;

namespace RunnerApp
{
    public class Map
    {
        public const int MapHeight = 22; // высота карты
        public const int MapWidth = 42; // ширина карты

        public static Image mapImage = new Image(Resources.map); // подгружаем тайл карты из ресурсов
        public static Texture mapTexture = new Texture(mapImage); // заряжаем текстуру картинкой
        public static Sprite mapSprite = new Sprite(mapTexture); // заливаем спрайт текстурой

        public static string[] baseMap =
        [
            "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
            "b                                        b",
            "b                                        b",
            "bbbbbbbbbbbbbsbbbbbbbbbb                 b",
            "b            s                           b",
            "b            s                           b",
            "b            s       bbs    bbbbbbbbbbsbbb",
            "b            s       bbs              s  b",
            "b            s       bbs              s  b",
            "b            s       bbs              s  b",
            "bbbbbbsbbbbbbb       bbbbbbbbbbbbsbbbbbbbb",
            "b     s                          s       b",
            "b     s                          s       b",
            "b     s                          s       b",
            "bbbbbbbbbbbbbbbbsbbbbbbbbbbbbbbbbs       b",
            "b               s                s       b",
            "b               s                s       b",
            "b               s                s       b",
            "b          sbbbbbb               bbbbbbbsb",
            "b          s                            sb",
            "b          s                            sb",
            "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
        ];

        // рандомно расставляем лампы по карте
        public static void RandomLampsGenerate()
        {
            int countLamp = 5;

            while (countLamp > 0)
            {
                Random rnd = new Random();
                int rndValue = rnd.Next();

                int randomLampX = 1 + rndValue % (MapWidth - 1); // рандомная коорд. по гориз. X от 1 до ширины карты-1, чтобы не получить коорд. границы карты
                int randomLampY = 1 + rndValue % (MapHeight - 1); // по вертикалим Y также

                if (baseMap[randomLampY][randomLampX] == ' ') // если по радномным коорд. встретили пробел
                {
                    // то заменяем пробел на символ лампы
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

