using RunnerApp.Properties;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using System.Text;

namespace RunnerApp
{
    public class Player : Entity
    {
        enum State { left, right, up, down, stay };
        State state;  
        double currentFrame;
        public int score;
        bool stairs;

        Sound sound = new Sound();
        
        public Player(Image image, double x, double y) : base(image, x, y)
        {
            sprite.TextureRect = new IntRect(0, 0, width, height);
            state = State.stay;
            currentFrame = 0;
            health = 100;
            score = 0;
            stairs = false;

            var soundBuffer = new SoundBuffer(Resources.take_lamp);
            sound.SoundBuffer = soundBuffer;
        }

        private void Control()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                state = State.left;
                speed = 0.13;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                state = State.right;
                speed = 0.13;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !onGround)
            {
                state = State.up;
                speed = 0.1;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                state = State.down;
                speed = 0.1;
            }
        }

        public void Animate(double time)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                currentFrame += 0.01 * time;
                if (currentFrame > 3) currentFrame -= 3;
                sprite.TextureRect = new IntRect(32 * (int)currentFrame, 0, 32, 32);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                currentFrame += 0.01 * time;
                if (currentFrame > 3) currentFrame -= 3;
                sprite.TextureRect = new IntRect(32 * (int)currentFrame, 32, 32, 32);
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Up) || Keyboard.IsKeyPressed(Keyboard.Key.Down)) && !onGround)
            {
                currentFrame += 0.01 * time;
                if (currentFrame > 2) currentFrame -= 2;
                sprite.TextureRect = new IntRect(32 * (int)currentFrame, 64, 32, 32);
            }
        }

        private void CheckCollisionWithMap(double dX, double dY)
        {
            if (dY < 0)
                for (int i = (int)y / 32; i < (y + height) / 32; i++)
                    for (int j = (int)(x + 10) / 32; j < (x + 5) / 32; j++)
                        if (Map.baseMap[i][j] == 'b')
                        { y = i * 32 + 32; dy = 0; }


            if (dY > 0)
                for (int i = (int)y / 32; i < (y + height) / 32; i++)
                    for (int j = (int)(x + 10) / 32; j < (x + 20) / 32; j++)
                        if (Map.baseMap[i][j] == 'b')
                        { y = i * 32 - 32; dy = 0; onGround = true; }


            if (dX < 0)
                for (int i = (int)y / 32; i < (y + height) / 32; i++)
                    for (int j = (int)x / 32; j < x / 32; j++)
                        if (Map.baseMap[i][j] == 'b')
                        { x = j * 32 + 32; }


            if (dX > 0)
                for (int i = (int)y / 32; i < (y + height) / 32; i++)
                    for (int j = (int)(x + 32) / 32; j < (x + 32) / 32; j++)
                        if (Map.baseMap[i][j] == 'b')
                        { x = j * 32 - 32; }


            for (int i = (int)y / 32; i < (y + height) / 32; i++)
                for (int j = (int)x / 32; j < (x + 17) / 32; j++)
                {
                    if (Map.baseMap[i][j] == 's')
                    {
                        stairs = true;
                        onGround = false;
                    }
                    else
                    {
                        stairs = false;
                        onGround = true;
                    }

                    if (Map.baseMap[i][j] == ' ' && Map.baseMap[i + 1][j] == 's'
                        && (state == State.left || state == State.right))
                        dy = 0;
                }
        }

        private void CheckCollisionWithLamps(double dX, double dY)
        {
            for (int i = (int)y / 32; i < (y + height) / 32; i++)
                for (int j = (int)(x + 20) / 32; j < (x + 10) / 32; j++)
                    if (Map.baseMap[i][j] == 'l')
                    {
                        StringBuilder sb = new StringBuilder(Map.baseMap[i]);
                        sb[j] = ' ';
                        string str = sb.ToString();
                        Map.baseMap[i] = str;
                       
                        score += 20;
                        sound.Play();
                    }
        }

        public void Update(double time)
        {
            Control();

            switch (state)
            {
                case State.stay: dx = 0; break;
                case State.right: dx = speed; break;
                case State.left: dx = -speed; break;
                case State.up: dy = -speed; dx = 0; break;
                case State.down: dy = speed; dx = 0; break;
            }

            x += dx * time;
            CheckCollisionWithMap(dx, 0);
            CheckCollisionWithLamps(dx, 0);

            y += dy * time;
            CheckCollisionWithMap(0, dy);
            CheckCollisionWithLamps(0, dy);

            sprite.Position = new((float)x + width / 2, (float)y + height / 2);

            if (health <= 0) life = false;
            if (!isMove) speed = 0;

            if (onGround) dy = dy + 0.0015 * time; // gravity

            if (stairs) sprite.TextureRect = new IntRect(0, 64, 32, 32);

            if (dx < 0) sprite.TextureRect = new IntRect(0, 0, 32, 32);
            if (dx > 0) sprite.TextureRect = new IntRect(0, 32, 32, 32);

            DisplayСoordinates();
        }

        private void DisplayСoordinates()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Player coordinates \nX: {x:f2}, Y: {y:f2}");
            Console.CursorVisible = false;
        }
    }
}
