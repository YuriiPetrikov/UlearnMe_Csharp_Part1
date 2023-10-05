// Вставьте сюда финальное содержимое файла DrawingProgram.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RefactorMe
{
    class Drow
    {
        static float x, y;
        static Graphics graphic;

        public static void Initialize ( Graphics newGraphic )
        {
            graphic = newGraphic;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0; y = y0;
        }

        public static void MakeIt(Pen pen, double dlina, double angle)
        {
            //Делает шаг длиной dlina в направлении ugol и рисует пройденную траекторию
            var x1 = (float)(x + dlina * Math.Cos(angle));
            var y1 = (float)(y + dlina * Math.Sin(angle));
            graphic.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double dlina, double angle)
        {
           x = (float)(x + dlina * Math.Cos(angle)); 
           y = (float)(y + dlina * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void DrawSide(double minSide, double angle)
        {
            Drow.MakeIt(Pens.Yellow, minSide * 0.375f, angle);
            Drow.MakeIt(Pens.Yellow, minSide * 0.04f * Math.Sqrt(2), Math.PI / 4 + angle);
            Drow.MakeIt(Pens.Yellow, minSide * 0.375f, Math.PI + angle);
            Drow.MakeIt(Pens.Yellow, minSide * 0.375f - minSide * 0.04f, Math.PI / 2 + angle);

            Drow.Change(minSide * 0.04f, -Math.PI + angle);
            Drow.Change(minSide * 0.04f * Math.Sqrt(2), 3 * Math.PI / 4 + angle);
        }

        public static void Draw(int width, int height, double rotationAgle, Graphics graphic)
        {
            // rotationAgle пока не используется, но будет использоваться в будущем
            Drow.Initialize(graphic);

            var sz = Math.Min(width, height);

            var diagonalLength = Math.Sqrt(2) * (sz * 0.375f + sz * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Drow.SetPosition(x0, y0);

            //Рисуем 1-ую сторону
            DrawSide(sz, 0);

            //Рисуем 2-ую сторону
            DrawSide(sz, -Math.PI / 2);
            
            //Рисуем 3-ю сторону
            DrawSide(sz, Math.PI);
            
            //Рисуем 4-ую сторону
            DrawSide(sz, Math.PI / 2);
        }
    }
}
