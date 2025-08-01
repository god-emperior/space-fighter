using System;
using System.Numerics;

namespace Star
{
    class Star
    {
        public float mass;
        Vector2 speed;
        public Vector2 position;
        public Star(float mass, Vector2 speed, Vector2 position)
        {
            this.mass = mass;
            this.speed = speed;
            this.position = position;
        }
        public void Move(Vector2 position2, float mass2, float deltaTime)
        {
            Vector2 direction = position2 - position;

            const float G = 1; // Гравитационная постоянная

            // 2. Нормализуем вектор (делаем длину = 1)
            if (direction != Vector2.Zero)
            {
                float distance = direction.Length();
                direction = Vector2.Normalize(direction);

                // Закон всемирного тяготения (F = G*m1*m2/r²)
                // Для простоты опускаем G и m1, включая их в mass2
                float force = G * mass2 / direction.LengthSquared();

                // Применяем силу (a = F/m, но m=1 для нашего объекта)
                speed += direction * force * Raylib_cs.Raylib.GetFrameTime();
            }
            // float += mass2 / Math.Pow((Vector2.Distance(position, position2)), 2);
            // speed = direction * (speed + *Raylib_cs.Raylib.GetFrameTime());
            position += speed * Raylib_cs.Raylib.GetFrameTime();
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