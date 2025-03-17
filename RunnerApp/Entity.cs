using SFML.Graphics;
using SFML.System;

namespace RunnerApp
{
    // base class
    public class Entity
    {
        public double currentFrame;

        public double x, y;
        public double dx, dy;
        public double speed;

        public int width, height;
        public int health;

        public bool life;
        public bool isMove, onGround;

        public Texture texture;
        public Sprite sprite;

        public Color backgroundColor = new Color(3, 0, 32);

        public Entity(Image image, (double x, double y) coordinates)
        {
            currentFrame = 0;

            x = coordinates.x;
            y = coordinates.y;

            width = 32;
            height = 32;

            dx = 0;
            dy = 0;
            speed = 0;

            life = true;
            isMove = false;
            onGround = true;

            image.CreateMaskFromColor(backgroundColor); // transparent sprite background
            texture = new Texture(image);
            sprite = new Sprite(texture);
            sprite.Origin = new Vector2f(width / 2, height / 2);
        }

        public virtual void Update(double time)
        {
            x += dx * time;
            y += dy * time;
            dy = dy + 0.0015 * time;

            sprite.Position = new((float)x + width / 2, (float)y + height / 2);
        }
    }
}
