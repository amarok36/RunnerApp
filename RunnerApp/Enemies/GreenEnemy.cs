using SFML.Graphics;

namespace RunnerApp.Enemies
{
    public class GreenEnemy : Entity
    {
        public GreenEnemy(Image image, (double x, double y) coordinates) : base(image, coordinates)
        {
            sprite.TextureRect = new IntRect(0, 0, width, height);
            health = 100;
            dx = 0.07;
        }

        public void Animate(double time)
        {
            if (dx > 0)
            {
                currentFrame += 0.01 * time;
                if (currentFrame > 3) currentFrame -= 3;
                sprite.TextureRect = new IntRect(32 * (int)currentFrame, 0, 32, 32);
            }
            if (dx < 0)
            {
                currentFrame += 0.01 * time;
                if (currentFrame > 3) currentFrame -= 3;
                sprite.TextureRect = new IntRect(32 * (int)currentFrame, 32, 32, 32);
            }
        }

        private void CheckCollisionWithMap(double dX, double dY)
        {
            if (dY < 0)
                for (int i = (int)y / 32; i < (y + height) / 32; i++)
                    for (int j = (int)(x + 5) / 32; j < (x + 5) / 32; j++)
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
                        { x = j * 32 + 32; dx = 0.07; }


            if (dX > 0)
                for (int i = (int)y / 32; i < (y + height) / 32; i++)
                    for (int j = (int)(x + 32) / 32; j < (x + 32) / 32; j++)
                        if (Map.baseMap[i][j] == 'b')
                        { x = j * 32 - 32; dx = -0.07; }
        }
        public void StopMove()
        {
            dx = 0;
            dy = 0;
        }

        public override void Update(double time)
        {
            x += dx * time;
            CheckCollisionWithMap(dx, 0);

            y += dy * time;
            CheckCollisionWithMap(0, dy);

            sprite.Position = new((float)x + width / 2, (float)y + height / 2);

            if (health <= 0) life = false;
            if (!isMove) speed = 0;

            dy = dy + 0.0015 * time; // gravity

            if (dx < 0) sprite.TextureRect = new IntRect(0, 0, 32, 32);
            if (dx > 0) sprite.TextureRect = new IntRect(0, 32, 32, 32);
        }
    }
}
