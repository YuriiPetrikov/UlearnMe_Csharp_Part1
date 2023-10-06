// Вставьте сюда финальное содержимое файла ManipulatorTask.cs
using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному alpha (в радианах)
        /// См. чертеж manipulator.png!
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            // Используйте поля Forearm, UpperArm, Palm класса Manipulator

            var wristX = x - Manipulator.Palm * Math.Cos(alpha);
            var wristY = y + Manipulator.Palm * Math.Sin(alpha);

            var length = Math.Sqrt(wristX * wristX + wristY * wristY);

            var elbow = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, length);

            var shoulder = TriangleTask.GetABAngle(Manipulator.UpperArm, length, Manipulator.Forearm)
                + Math.Atan2(wristY, wristX);
            var wrist = -alpha - shoulder - elbow;
            if (length <= 0 || elbow == double.NaN || shoulder == double.NaN || wrist == double.NaN)
                return new[] { double.NaN, double.NaN, double.NaN };
            return new[] { shoulder, elbow, wrist };
            //return new[] { double.NaN, double.NaN, double.NaN };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            /*
            Random rnd = new Random(1);

            for(int i = 0; i < 10; i++)
            {
                var x = rnd.NextDouble() * 10;
                var y = rnd.NextDouble() * 10;
                var Angel = rnd.NextDouble() * 10;
                //TestMoveManipulatorTo(x, y, Angel);
            }
            */
            //Assert.Fail("Write randomized test here!");
        }
    }
}
