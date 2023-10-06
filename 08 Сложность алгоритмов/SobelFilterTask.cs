using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var widthSX = sx.GetLength(0);
            var heightSX = sx.GetLength(1);
            int posX = widthSX / 2;
            int posY = heightSX / 2;
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            
            for (int x = posX; x < width - posX; x++)
                for (int y = posY; y < height - posY; y++)
                {
                   result[x, y] = CalculateToGradient(g, sx, x, y);
                }

            return result;
        }

        public static double CalculateToGradient(double[,] g, double[,] sx, int x, int y)
        {
            double gradientX = 0;
            double gradientY = 0;
            var widthSX = sx.GetLength(0);
            var heightSX = sx.GetLength(1);

            for (int i = x - widthSX / 2, ii = 0; i <= x + widthSX / 2; i++, ii++)
            {
                for (int j = y - heightSX / 2, jj = 0; j <= y + heightSX / 2; j++, jj++)
                {
                    gradientX += g[i, j] * sx[ii, jj];
                    gradientY += g[i, j] * sx[jj, ii];
                }
            }
            return Math.Sqrt(gradientX * gradientX + gradientY * gradientY);
        }
    }
}
