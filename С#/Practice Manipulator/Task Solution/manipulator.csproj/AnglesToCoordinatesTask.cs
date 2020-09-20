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
            Func<double, float> cos = angle => (float)Math.Round(Math.Cos(angle), 6);
            Func<double, float> sin = angle => (float)Math.Round(Math.Sin(angle), 6);

            var elbowPos = new PointF(0, 0);
            var wristPos = new PointF(0, 0);
            var palmEndPos = new PointF(0, 0);

            elbowPos.X = Manipulator.UpperArm * cos(shoulder);
            elbowPos.Y = Manipulator.UpperArm * sin(shoulder);

            wristPos.X = elbowPos.X + Manipulator.Forearm * cos(shoulder + Math.PI + elbow);
            wristPos.Y = elbowPos.Y + Manipulator.Forearm * sin(shoulder + Math.PI + elbow);

            palmEndPos.X = wristPos.X + Manipulator.Palm * cos(shoulder + Math.PI + elbow + Math.PI + wrist);
            palmEndPos.Y = wristPos.Y + Manipulator.Palm * sin(shoulder + Math.PI + elbow + Math.PI + wrist);

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
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, (Manipulator.Forearm + Manipulator.Palm), Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI, Math.PI, 0, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm)]
        [TestCase(Math.PI / 2, 1.5 * Math.PI, Math.PI, -(Manipulator.Forearm + Manipulator.Palm), Manipulator.UpperArm)]
        [TestCase(0, Math.PI, Math.PI, Manipulator.Forearm + Manipulator.Palm + Manipulator.UpperArm, 0)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            if (Math.Abs(palmEndX - joints[2].X) > 1e-5 || Math.Abs(palmEndY - joints[2].Y) > 1e-5)
                Assert.Fail("TODO: проверить, что расстояния между суставами равны длинам сегментов манипулятора!");
        }
    }
}