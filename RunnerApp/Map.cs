using System.Text;

namespace RunnerApp
{
    public class Map
    {
        public const int MapHeight = 22; // высота карты
        public const int MapWidth = 42; // ширина карты

        public static string[] TileMap =
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
            int countLamp = 5; // количество ламп

            while (countLamp > 0)
            {
                Random rnd = new Random(); // объект для генерации случайных чисел
                int rndValue = rnd.Next();  // получаем очередное случайное число

                int randomLampX = 1 + rndValue % (MapWidth - 1); // рандомная коорд. по гориз. X от 1 до ширины карты-1, чтобы не получить коорд. границы карты
                int randomLampY = 1 + rndValue % (MapHeight - 1); // по вертикалим Y также

                if (TileMap[randomLampY][randomLampX] == ' ') // если по радномным коорд. встретили пробел
                {
                    // то заменяем пробел на символ лампы
                    StringBuilder sb = new StringBuilder(TileMap[randomLampY]);
                    sb[randomLampX] = 'l';

                    string str = sb.ToString();
                    TileMap[randomLampY] = str;

                    countLamp--; // декрементируем счётчик после создания лампы
                }
            }

        }
    }
}

