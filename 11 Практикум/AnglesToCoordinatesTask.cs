using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var xElbowPos = Manipulator.UpperArm * Math.Cos(shoulder);
            var yElbowPos = Manipulator.UpperArm * Math.Sin(shoulder);
            var xWristPos = Manipulator.Forearm * Math.Cos(shoulder + Math.PI + elbow) + xElbowPos;
            var yWristPos = Manipulator.Forearm * Math.Sin(shoulder + Math.PI + elbow) + yElbowPos;
            var xPalmEndPos = Manipulator.Palm * Math.Cos(shoulder + Math.PI + elbow + Math.PI + wrist) + xWristPos;
            var yPalmEndPos = Manipulator.Palm * Math.Sin(shoulder + Math.PI + elbow + Math.PI + wrist) + yWristPos;

            var elbowPos = new PointF((float)xElbowPos, (float)yElbowPos);
            var wristPos = new PointF((float)xWristPos, (float)yWristPos);
            var palmEndPos = new PointF((float)xPalmEndPos, (float)yPalmEndPos);
            
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        // Доработайте эти тесты!
        // С помощью строчки TestCase можно добавлять новые тестовые данные.
        // Аргументы TestCase превратятся в аргументы метода.
		[TestCase(Math.PI / 2, Math.PI / 2, Math.PI, 
				Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
		[TestCase(Math.PI / 2, Math.PI / 2, Math.PI, 
				Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
		[TestCase(0, Math.PI, Math.PI, 
				Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm , 0)]
		[TestCase(Math.PI / 2, Math.PI, Math.PI, 	
				0, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm)]
		[TestCase(0, -Math.PI / 2, Math.PI / 2, 
				Manipulator.UpperArm + Manipulator.Palm, +Manipulator.Forearm)]

        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
			Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
			Assert.AreEqual(Manipulator.UpperArm * Math.Cos(shoulder), joints[0].X, 1e-5, "shoulder endX");
            //Assert.Fail("TODO: проверить, что расстояния между суставами равны длинам сегментов манипулятора!");
        }
    }
}
