using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Manipulation
{
	public static class VisualizerTask
	{
		public static double X = 220;
		public static double Y = -100;
		public static double Alpha = 0.05;
		public static double Wrist = 2 * Math.PI / 3;
		public static double Elbow = 3 * Math.PI / 4;
		public static double Shoulder = Math.PI / 2;

		public static Brush UnreachableAreaBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 230));
		public static Brush ReachableAreaBrush = new SolidBrush(Color.FromArgb(255, 230, 255, 230));
		public static Pen ManipulatorPen = new Pen(Color.Black, 3);
		public static Brush JointBrush = Brushes.Gray;

		public static void KeyDown(Form form, KeyEventArgs key)
		{
			double k = 0.01;
			
			switch (key.KeyCode)
			{
				case Keys.Q:
					Shoulder += k;
					break;
				case Keys.A:
					Shoulder -= k;
					break;
				case Keys.W:
					Elbow += k;
					break;
				case Keys.S:
					Elbow -= k;
					break;
			}

			Wrist = -Alpha - Shoulder - Elbow;

			form.Invalidate();
		}


		public static void MouseMove(Form form, MouseEventArgs e)
		{
			// TODO: Измените X и Y пересчитав координаты (e.X, e.Y) в логические.
			var shoulderPos = GetShoulderPos(form);
			var mathPoint = ConvertWindowToMath(new PointF(e.X, e.Y), shoulderPos);
			X = mathPoint.X;
			Y = mathPoint.Y;
			UpdateManipulator();
			form.Invalidate();
		}

		public static void MouseWheel(Form form, MouseEventArgs e)
		{
			// TODO: Измените Alpha, используя e.Delta — размер прокрутки колеса мыши
			Alpha += e.Delta;
			UpdateManipulator();
			form.Invalidate();
		}

		public static void UpdateManipulator()
		{
			// Вызовите ManipulatorTask.MoveManipulatorTo и обновите значения полей Shoulder, Elbow и Wrist, 
			// если они не NaN. Это понадобится для последней задачи.
			var Updated = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);

			if (Updated[0] == double.NaN || Updated[1] == double.NaN || Updated[2] == double.NaN)
			{
				Shoulder = double.NaN;
				Elbow = double.NaN;
				Wrist = double.NaN;
			}
			else
			{
				Shoulder = Updated[0];
				Elbow = Updated[1];
				Wrist = Updated[2];
			}
		}

		public static void DrawManipulator(Graphics graphics, PointF shoulderPos)
		{
			var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

			graphics.DrawString(
                $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}", 
                new Font(SystemFonts.DefaultFont.FontFamily, 12), 
                Brushes.DarkRed, 
                10, 
                10);
			DrawReachableZone(graphics, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);

			// Нарисуйте сегменты манипулятора методом graphics.DrawLine используя ManipulatorPen.
			// Нарисуйте суставы манипулятора окружностями методом graphics.FillEllipse используя JointBrush.
			// Не забудьте сконвертировать координаты из логических в оконные

			var windowPoint = ConvertMathToWindow(new PointF((float)X, (float)Y), shoulderPos);

			X = windowPoint.X;
			Y = windowPoint.Y;

			graphics.DrawLine(ManipulatorPen, shoulderPos.X, shoulderPos.Y, shoulderPos.X + joints[0].X, shoulderPos.Y - joints[0].Y); //draw Shoulder
			graphics.DrawLine(ManipulatorPen, shoulderPos.X + joints[0].X, shoulderPos.Y - joints[0].Y, shoulderPos.X + joints[1].X, shoulderPos.Y - joints[1].Y); //draw Elbow
			graphics.DrawLine(ManipulatorPen, shoulderPos.X + joints[1].X, shoulderPos.Y - joints[1].Y, shoulderPos.X + joints[2].X, shoulderPos.Y - joints[2].Y); //draw Wrist

			foreach (var joint in joints)
				graphics.FillEllipse(JointBrush, shoulderPos.X + joint.X - 5, shoulderPos.Y - joint.Y - 5, 5, 5);

		}

		private static void DrawReachableZone(
            Graphics graphics, 
            Brush reachableBrush, 
            Brush unreachableBrush, 
            PointF shoulderPos, 
            PointF[] joints)
		{
			var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
			var rmax = Manipulator.UpperArm + Manipulator.Forearm;
			var mathCenter = new PointF(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
			var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
			graphics.FillEllipse(reachableBrush, windowCenter.X - rmax, windowCenter.Y - rmax, 2 * rmax, 2 * rmax);
			graphics.FillEllipse(unreachableBrush, windowCenter.X - rmin, windowCenter.Y - rmin, 2 * rmin, 2 * rmin);
		}

		public static PointF GetShoulderPos(Form form)
		{
			return new PointF(form.ClientSize.Width / 2f, form.ClientSize.Height / 2f);
		}

		public static PointF ConvertMathToWindow(PointF mathPoint, PointF shoulderPos)
		{
			return new PointF(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
		}

		public static PointF ConvertWindowToMath(PointF windowPoint, PointF shoulderPos)
		{
			return new PointF(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
		}
	}
}