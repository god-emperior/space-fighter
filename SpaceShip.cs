using System;
using System.Linq.Expressions;
using System.Numerics;

namespace SpaceShip
{
    class SpaceShip
    {
        public float mass;
        public int thrust;

        public Vector2 position;
        Vector2 speed;
        public Quaternion quaternion = new Quaternion();
        public float rotation;
        int rotationspeed;
        public SpaceShip(int mass, int thrust, int rotationspeed, Vector2 vector2)
        {
            this.mass = mass;
            this.thrust = thrust;
            this.rotationspeed = rotationspeed;
            this.position = vector2;
        }

        public void I(float deltaTime)
        {
            speed += DegreeToVector(rotation) * thrust * deltaTime;
        }
        public void Rotate(bool a, float deltaTime)
        {
            if (a)
            {
                rotation += -rotationspeed * deltaTime;
                quaternion.Z += -rotationspeed * deltaTime;
            }
            else
            {
                rotation += rotationspeed * deltaTime;
                quaternion.Z += rotationspeed * deltaTime;
            }
        }
        public static Vector2 DegreeToVector(float degree)
        {
            // Преобразуем градусы в радианы
            float radians = (degree - 90) * MathF.PI / 180f;
            int ofsetZ = 90;

            // Вычисляем компоненты вектора
            float x = MathF.Cos(radians);
            float y = MathF.Sin(radians);

            return new Vector2(x, y);
        }
        public void Move(float deltaTime)
        {
            position += speed * deltaTime;
        }

        public void Force(float deltaTime, Vector2 force)
        {
            speed += force / mass * deltaTime;
        }
    }
}