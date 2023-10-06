// Вставьте сюда финальное содержимое файла VisualizerTask.cs
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
			// TODO: Добавьте реакцию на QAWS и пересчитывать Wrist
			if (key.KeyValue == 'Q') { Shoulder += Math.PI / 360; }
			if (key.KeyValue == 'A') { Shoulder -= Math.PI / 360; }
			if (key.KeyValue == 'W') { Elbow    += Math.PI / 360; }
			if (key.KeyValue == 'S') { Elbow    -= Math.PI / 360; }
			Wrist = -Alpha - Shoulder - Elbow;
			form.Invalidate(); 
		}


		public static void MouseMove(Form form, MouseEventArgs e)
		{
			// TODO: Измените X и Y пересчитав координаты (e.X, e.Y) в логические.
			var mousePos = ConvertWindowToMath(
				new PointF((float)e.X, (float)e.Y), new PointF(form.ClientSize.Width / 2, form.ClientSize.Height / 2));
			X = mousePos.X;
			Y = mousePos.Y;
			
			UpdateManipulator();
			form.Invalidate();
		}

		public static void MouseWheel(Form form, MouseEventArgs e)
		{
			// TODO: Измените Alpha, используя e.Delta — размер прокрутки колеса мыши
			//В методе MouseWheel добавить обработку прокрутки колеса мыши. Оно должно менять Alpha.
			Alpha += e.Delta;
			UpdateManipulator();
			form.Invalidate();
		}

		public static void UpdateManipulator()
		{
			// Вызовите ManipulatorTask.MoveManipulatorTo и обновите значения полей Shoulder, Elbow и Wrist, 
			// если они не NaN. Это понадобится для последней задачи.
			//В методе UpdateManipulator вызвать ManipulatorTask.MoveManipulatorTo
			//и обновить значения Shoulder, Elbow и Wrist(это понадобится в последней задаче).
			//UpdateManipulator нужно вызывать после каждого изменения X, Y или Alpha,
			//то есть в методах MouseMove и MouseWheel.
			if(Shoulder != double.NaN && Elbow != double.NaN && Wrist != double.NaN)
				ManipulatorTask.MoveManipulatorTo(Shoulder, Elbow, Wrist);
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

			DrawManipulatorBody(graphics, shoulderPos, joints);
		}

		public static void DrawManipulatorBody(Graphics graphics, PointF shoulderPos, PointF[] joints)
		{
            var shoulderPosWindows = ConvertMathToWindow(joints[0], shoulderPos);
            var elbowPosWindows = ConvertMathToWindow(joints[1], shoulderPos);
            var wristPosWindows = ConvertMathToWindow(joints[2], shoulderPos);

            graphics.FillEllipse(JointBrush, shoulderPos.X - 15, shoulderPos.Y - 15, 30, 30);
            graphics.FillEllipse(JointBrush, shoulderPosWindows.X - 15, shoulderPosWindows.Y - 15, 30, 30);
            graphics.FillEllipse(JointBrush, elbowPosWindows.X - 15, elbowPosWindows.Y - 15, 30, 30);

            graphics.DrawLine(ManipulatorPen, shoulderPos.X, shoulderPos.Y, 
				shoulderPosWindows.X, shoulderPosWindows.Y);
            graphics.DrawLine(ManipulatorPen, shoulderPosWindows.X, 
				shoulderPosWindows.Y, elbowPosWindows.X, elbowPosWindows.Y);
            graphics.DrawLine(ManipulatorPen, elbowPosWindows.X, 
				elbowPosWindows.Y, wristPosWindows.X, wristPosWindows.Y);
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
