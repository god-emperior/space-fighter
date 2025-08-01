using System;
using System.Numerics;
using Raylib_cs;
using Universe;
using Star;
using System.Collections.Generic;

namespace Stars
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(1500, 1300, "Window");


            Star.Star AlphaBeta2 = new Star.Star(2, new Vector2(1, 3), new Vector2(300, 1278));
            Star.Star AlphaRomeo3 = new Star.Star(5, new Vector2(-3, 3), new Vector2(600, 1135));
            Star.Star AlphaRomeo4 = new Star.Star(7, new Vector2(-3, 3), new Vector2(600, 1081));
            Universe.Universe.stars.Add(AlphaBeta2);
            Universe.Universe.stars.Add(AlphaRomeo3);
            Universe.Universe.stars.Add(AlphaRomeo4);


            int Count = 100;
            float timer = 1;
            Vector2[][] vector2 = new Vector2[100][];
            wdasd.casee.camera2D.zoom = 0.1F;

            float[] r = Universe.Universe.StarRan();

            Texture2D ship = Raylib.LoadTexture("drone_ico_t.png");
            //int[] r = new int[8];
            // Инициализация трассеров (один раз в начале)
            PathTracer[] tracers = new PathTracer[Universe.Universe.stars.Count + 1];
            for (int i = 0; i < tracers.Length; i++)
            {
                // Разные цвета для звезд и корабля
                Color trailColor = i < Universe.Universe.stars.Count
                    ? new Color(255, 100, 0, 150) // Оранжевый для звезд
                    : new Color(100, 200, 255, 150); // Голубой для корабля

                tracers[i] = new PathTracer(0.3f, trailColor);
            }

            Random random = new Random();
            Color[] c = new Color[8];
            Color[] b = new Color[8];

            bool IsOrbitHide = true;
            // Заполняем массивы значениями по умолчанию
            for (int i = 0; i < 8; i++)
            {
                c[i] = Color.GREEN;
                b[i] = Color.DARKGREEN;
            }
            
            // Генерация случайных цветов
            for (int i = 0; i < 8; i++)
            {
                int randomValue = random.Next(8); // Одно случайное число для всех проверок

                switch (randomValue)
                {
                    case 1:
                        c[i] = Color.SKYBLUE;
                        break;
                    case 2:
                        c[i] = Color.BEIGE;
                        b[i] = Color.BROWN;
                        break;
                    case 3:
                        c[i] = Color.GREEN;
                        b[i] = Color.VIOLET;
                        break;
                    case 4:
                        c[i] = Color.RED;
                        b[i] = Color.GRAY;
                        break;
                    case 5:
                        c[i] = Color.MAGENTA;
                        b[i] = Color.BLANK;
                        break;
                    case 6:
                        c[i] = Color.GRAY;
                        b[i] = Color.PINK;
                        break;
                    case 7:
                        c[i] = Color.ORANGE;
                        b[i] = Color.PINK;
                        break;
                        // case 0 оставляет цвета по умолчанию
                }
            }


            while (!Raylib.WindowShouldClose())
            {
                Raylib.GetFrameTime();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                float deltaTime = Raylib_cs.Raylib.GetFrameTime();

                Universe.Universe.S(deltaTime);

                //Universe.Universe.S1(deltaTime);
                SpaceShip.SpaceShip spaceShip = Universe.Universe.spaceShip;


                if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                {
                    spaceShip.Rotate(true, deltaTime);
                }
                else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                {
                    spaceShip.Rotate(false, deltaTime);
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_UP)) spaceShip.I(deltaTime);
                // switch (random.Next(8))
                // {
                //     case 0:  Color c = Color.SKYBLUE;
                //     case 1:  


                //    default: return
                //}
                Raylib.BeginMode2D(wdasd.casee.camera2D); RayCollision rayCollision = new RayCollision(); 
                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    wdasd.casee.camera2D.offset += wdasd.casee.speed * new Vector2(1, 0) * deltaTime;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                {
                    wdasd.casee.camera2D.offset += wdasd.casee.speed * new Vector2(0, 1) * deltaTime;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    wdasd.casee.camera2D.offset += wdasd.casee.speed * new Vector2(-1, 0) * deltaTime;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    wdasd.casee.camera2D.offset += wdasd.casee.speed * new Vector2(0, -1) * deltaTime;
                }
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    wdasd.casee.camera2D.zoom += wdasd.casee.zspeed * deltaTime;
                }
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
                {
                    wdasd.casee.camera2D.zoom += -wdasd.casee.zspeed * deltaTime;
                }
                for (int i = 0; i < Universe.Universe.stars.Count; i++)
                {
                    int colorIndex = i % 8;
                    int rdIndex = i % 7;
                    Raylib.DrawCircleGradient((int)Universe.Universe.stars[i].position.X, (int)Universe.Universe.stars[i].position.Y, r[rdIndex], c[colorIndex], b[colorIndex]);
                }
                // Обновление трассеров
                for (int i = 0; i < Universe.Universe.stars.Count; i++)
                {
                    tracers[i].Update(Universe.Universe.stars[i].position, deltaTime);
                }
                tracers[tracers.Length-1].Update(spaceShip.position, deltaTime); // Для корабля

                //Raylib.DrawCircleGradient((int)AlphaRomeo3.position.X, (int)AlphaRomeo3.position.Y, 5, Color.BLUE, Color.DARKBLUE);
                //Raylib.DrawCircleGradient((int)AlphaBeta2.position.X, (int)AlphaBeta2.position.Y, 3, Color.GREEN, Color.DARKGREEN);
                AlphaBeta2.Move(AlphaRomeo3.position, AlphaRomeo3.mass, deltaTime);
                AlphaRomeo3.Move(AlphaBeta2.position, AlphaBeta2.mass, deltaTime);

                Raylib.DrawTextureEx(ship, spaceShip.position - new Vector2(ship.height, ship.width) * 3 / 2, spaceShip.rotation, 3, Color.GRAY);
                Raylib.DrawCircleGradient((int)spaceShip.position.X, (int)spaceShip.position.Y, 3, Color.GREEN, Color.DARKGREEN);
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    IsOrbitHide = !IsOrbitHide;
                }
                if (!IsOrbitHide)
                {
                    for (int i = 0; i < tracers.Length; i++)
                    {
                        tracers[i].Draw();
                    }
                }
                Raylib.EndMode2D();
                Raylib.EndDrawing();
            }
        }
        class PathTracer
        {
            private const int MAX_POINTS = 100;
            private readonly Queue<Vector2> pathPoints = new Queue<Vector2>();
            private float timer;
            private readonly float recordInterval;
            private readonly Color color;
            private bool visible;

            public PathTracer(float interval = 0.5f, Color? trailColor = null)
            {
                recordInterval = interval;
                color = trailColor ?? new Color(255, 165, 0, 180); // Оранжевый с прозрачностью
            }

            public void Update(Vector2 currentPosition, float deltaTime)
            {
                timer += deltaTime;

                if (timer >= recordInterval)
                {
                    timer = 0;
                    pathPoints.Enqueue(currentPosition);

                    if (pathPoints.Count > MAX_POINTS)
                        pathPoints.Dequeue();
                }
            }

            public void Draw()
            {
                if (pathPoints.Count < 2) return;

                Vector2[] points = pathPoints.ToArray();
                Color currentColor = color;

                // Рисуем линии между точками
                for (int i = 1; i < points.Length; i++)
                {
                    // Плавное уменьшение прозрачности
                    float alpha = (float)i / points.Length;
                    currentColor.a = (byte)(color.a * alpha);

                    Raylib.DrawLineEx(
                        points[i - 1],
                        points[i],
                        2f,
                        currentColor
                    );

                    // Рисуем точку каждые 10 сегментов
                    if (i % 10 == 0)
                    {
                        Raylib.DrawCircleV(points[i], 3f, currentColor);
                    }
                }
            }

            public void Clear()
            {
                pathPoints.Clear();
            }
            public void SetVisible(bool visible)
            {
                this.visible = visible;
            }
        }
    }
}
