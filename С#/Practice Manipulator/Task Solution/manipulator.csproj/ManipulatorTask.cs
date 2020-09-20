using System;
using System.Drawing;
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
            var joints = AnglesToCoordinatesTask.GetJointPositions(VisualizerTask.Shoulder, VisualizerTask.Elbow, VisualizerTask.Wrist);

            Func<double, double> cos = angle => Math.Round(Math.Cos(angle), 10);
            Func<double, double> sin = angle => Math.Round(Math.Sin(angle), 10);

            double wrist = - alpha - VisualizerTask.Shoulder - VisualizerTask.Elbow;
            var wristPos = new Vector(x + cos(Math.PI - alpha) * Manipulator.Palm, y + sin(Math.PI - alpha) * Manipulator.Palm);
            double elbow = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, Math.Sqrt(wristPos.X * wristPos.X + wristPos.Y * wristPos.Y));
            double shoulder = TriangleTask.GetABAngle(Math.Sqrt(wristPos.X * wristPos.X + wristPos.Y * wristPos.Y), Manipulator.UpperArm, Manipulator.Forearm);
            shoulder += Math.Atan2(wristPos.Y, wristPos.X);

            if (wrist == double.NaN || elbow == double.NaN || shoulder == double.NaN)
            {
                wrist = double.NaN;
                elbow = double.NaN;
                shoulder = double.NaN;
			}

            return new double[] { shoulder, elbow, wrist };
        }

        public static double DistTo(this Vector p1, Vector p2)
        {
            return Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
		}
    }

    public class Vector
    {
        public double X;
        public double Y;
        public Vector(double x, double y) { X = x; Y = y; }
	}

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            
        }
    }
}