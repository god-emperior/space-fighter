using System;
using System.Collections.Generic;
using System.Numerics;

namespace Universe
{
    class Universe
    {
        static public List<Star.Star> stars = new List<Star.Star>();
        static public SpaceShip.SpaceShip spaceShip = new SpaceShip.SpaceShip(10, 100, 10, new Vector2(564, 135));
        static public void S(float deltaTime)
        {
            for (int j = 0; j < stars.Count; j++)
            {
                for (int i = 0; i < stars.Count; i++)
                {
                    if (j != i)
                    {
                        stars[j].Force(deltaTime, Move(stars[j].position, stars[j].mass, stars[i].position, stars[i].mass, deltaTime));
                    }
                    spaceShip.Force(deltaTime, Move(spaceShip.position, spaceShip.mass, stars[j].position, stars[j].mass, deltaTime));

                    stars[j].Force(deltaTime, Move(stars[j].position, stars[j].mass, spaceShip.position, spaceShip.mass, deltaTime));
                }
                stars[j].Move(deltaTime);
            }
            spaceShip.Move(deltaTime);
        }
        static public void S1(float deltaTime)
        {
            for (int j = 0; j < stars.Count; j++)
            {

            }
        }
        static public Vector2 Move(Vector2 position, float mass1, Vector2 position2, float mass2, float deltaTime)
        {
            Vector2 direction = position2 - position;

            const float G = 0.3f; // Гравитационная постоянная

            // 2. Нормализуем вектор (делаем длину = 1)
            if (direction != Vector2.Zero)
            {
                float distance = direction.Length();
                direction = Vector2.Normalize(direction);

                // Закон всемирного тяготения (F = G*m1*m2/r²)
                // Для простоты опускаем G и m1, включая их в mass2
                float force = G * mass2 * mass1 / direction.LengthSquared();

                // Применяем силу (a = F/m, но m=1 для нашего объекта)
                return direction * force;
            }
            return Vector2.Zero;
        }
        static public float[] StarRan()
        {
            Random random = new Random();
            float[] floats = new float[8];
            for (int i = 0; i < random.Next(8); i++)
            {
                stars.Add(new Star.Star(random.Next(49), new Vector2(random.Next(-4, 9), random.Next(-5, 8)), new Vector2(random.Next(400), random.Next(698))));
                floats[i] = random.Next(3, 47);
            }
            return floats;
        }
    }
}