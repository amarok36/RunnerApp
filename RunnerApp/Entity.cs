using SFML.Graphics;
using SFML.System;

namespace RunnerApp
{
    // базовый класс
    public class Entity
    {
        public float x, y, dx, dy, speed; // координаты, ускорение (по x и y), скорость
        public int width, height;
        public int health; // хранит жизни
        public bool life; // логическая жизнь
        public bool isMove, onGround; // на земле

        public Texture texture;
        public Sprite sprite;

        public Color backgroundColor = new Color(3, 0, 32);

        public Entity(Image image, float x, float y)
        {
            this.x = x;
            this.y = y;

            width = 32;
            height = 32;

            dx = 0;
            dy = 0;
            speed = 0;

            life = true;
            isMove = false;
            onGround = true;

            image.CreateMaskFromColor(backgroundColor); // делаем фон спрайта прозрачным
            texture = new Texture(image);
            sprite = new Sprite(texture);
            sprite.Origin = new Vector2f(width / 2, height / 2); // основная точка, с которой рисуем спрайт - в середине
        }
    }
}
