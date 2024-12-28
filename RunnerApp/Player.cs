using SFML.Graphics;
using SFML.Window;
using System.Text;

namespace RunnerApp
{
    // класс игрока
    public class Player : Entity
    {
        enum state { left, right, up, down, jump, stay }; // состояние игрока
        state status;
        int playerScore;

        public Player(Image image, float x, float y) : base(image, x, y)
        {
            playerScore = 0;
            status = state.stay;
            sprite.TextureRect = new IntRect(0, 0, width, hight);
        }

        // управление персонажем
        public void Control()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                status = state.left;
                speed = 0.1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                status = state.right;
                speed = 0.1f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && onGround) // если нажата клавиша вверх и мы на земле, то можем прыгать
            {
                status = state.jump;
                dy = -0.4f;
                onGround = false; // состояние равно прыжок - прыгнули и сообщили, что мы не на земле
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                status = state.down;
            }

        }

        // проверка столкновений с картой
        public void CheckCollisionWithMap(double Dx, double Dy)
        {
            bool stairs = false;

            for (int i = (int)y / 32; i < (y + hight) / 32; i++) // проходимся по тайликам, контактирующим с игроком
                for (int j = (int)x / 32; j < (x + width) / 32; j++) // X делим на 32, тем самым получаем левый квадратик, с которым персонаж соприкасается
                {

                    if (Map.baseMap[i][j] == 'b') // если элемент - тайлик кирпича
                    {
                        if ((Dy > 0) && (stairs == false)) { y = i * 32 - hight; dy = 0; onGround = true; } // по Y вниз => идем в пол (стоим на месте) или падаем
                        if ((Dy < 0) && (stairs == false)) { y = i * 32 + 32; dy = 0; } // столкновение с верхними краями карты
                        if (Dx > 0) x = j * 32 - width; // с правым краем карты
                        if (Dx < 0) x = j * 32 + 32; // с левым краем карты
                    }
                    if (Map.baseMap[i][j] == 'l') // если элемент - тайлик лампы
                    {
                        StringBuilder sb = new StringBuilder(Map.baseMap[i]); // заменяем на пустой тайлик
                        sb[j] = ' ';
                        string str = sb.ToString();
                        Map.baseMap[i] = str;

                        playerScore += 20;
                    }
                    if ((Map.baseMap[i][j] == 's') && Keyboard.IsKeyPressed(Keyboard.Key.Up))
                    {
                        stairs = true;
                        dy = -0.1f;
                        status = state.up;
                        onGround = false;
                    }
                    if ((Map.baseMap[i][j] == 's') && Keyboard.IsKeyPressed(Keyboard.Key.Down))
                    {
                        stairs = true;
                        dy = 0.1f;
                        status = state.down;
                        onGround = false;
                    }
                }
        }

        // оживление объекта класса
        public void Update(float time)
        {
            Control();

            switch (status) // поведение в зависимости от направления 
            {
                case state.right: dx = speed; break; // состояние идти вправо
                case state.left: dx = -speed; break;  // состояние идти влево
                case state.up: dx = 0; dy = 0; break; // состояние подняться вверх (например по лестнице)
                case state.down: dx = 0; break; // состояние во время спуска персонажа
                case state.jump: break; 
                case state.stay: break;
            }
            x += dx * time; // ускорение * время - получаем смещение координат по Х и, как следствие, движение
            CheckCollisionWithMap(dx, 0); // обрабатываем столкновение по Х

            y += dy * time; // аналогично по Y
            CheckCollisionWithMap(0, dy); // обрабатываем столкновение по Y

            sprite.Position = new(x + width / 3, y + hight / 2); // задаем позицию спрайта в его центре, выводим бесконечно, иначе бы наш спрайт стоял на месте

            if (health <= 0) life = false; // если число жизней меньше или равно 0, то игрок умирает
            if (!isMove) speed = 0;

            dy = dy + 0.0015f * time; // постоянно притягиваемся к земле

            if (dx < 0) sprite.TextureRect = new IntRect(0, 0, 32, 32);
            if (dx > 0) sprite.TextureRect = new IntRect(0, 32, 32, 32);
        }
    }
}
