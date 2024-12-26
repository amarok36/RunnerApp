using SFML.Graphics;
using SFML.System;

namespace RunnerApp
{
    // базовый класс
    public class Entity
    {
        public float x, y, dx, dy, speed; // координаты, ускорение (по x и y), скорость
        public int width, hight; 
        public int health; // хранит жизни
        public bool life; // логическая жизнь
        public bool isMove, onGround; // на земле

        public Texture texture; 
        public Sprite sprite; 

        public Entity(Image image, float x, float y, int width, int hight)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.hight = hight;
           
            dx = 0;
            dy = 0;
            speed = 0;

            life = true; 
            isMove = false; 
            onGround = false;

            texture = new Texture(image); 
            sprite = new Sprite(texture); 
            sprite.Origin = new Vector2f(width / 2, hight / 2);// основная точка, с которой рисуем спрайт - в середине
        }
    }
}
