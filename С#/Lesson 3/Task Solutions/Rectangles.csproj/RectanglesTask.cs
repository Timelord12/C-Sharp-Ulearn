using System;
using System.Xml.Schema;

namespace Rectangles
{
	public static class RectanglesTask
	{
		/*
		 * Ох, перепиши с методами и без длиннющих строк~
		 * --Jarl
		 */
		
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			// так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top

			Func<int, int, int, int, bool> segmentIntersect = (a, b, x, y) => (b >= x) && (y >= a);

			return segmentIntersect(r1.Left, r1.Left + r1.Width, r2.Left, r2.Left + r2.Width) && segmentIntersect(r1.Top, r1.Top + r1.Height, r2.Top, r2.Top + r2.Height);
		}


		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			Func<int, int, int, int, bool> segmentIntersect = (a, b, x, y) => (b >= x) && (y >= a);

			Func<int, int, int, int, int> segmentDifference = (a, b, x, y) => Math.Min(Math.Min(b - x, y - a), Math.Min(b - a, y - x)) * Convert.ToInt32(segmentIntersect(a, b, x, y));

			return segmentDifference(r1.Left, r1.Left + r1.Width, r2.Left, r2.Left + r2.Width) * segmentDifference(r1.Top, r1.Top + r1.Height, r2.Top, r2.Top + r2.Height);
		}

		// Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
		// Иначе вернуть -1
		// Если прямоугольники совпадают, можно вернуть номер любого из них.
		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
			Func<int, int, int, bool> belongs = (a, x, y) => (a >= x) && (a <= y);

			Func<Rectangle, int> square = x => x.Width * x.Height;

			Rectangle small = square(r1) < square(r2) ? r1 : r2;

			Rectangle big = square(r1) < square(r2) ? r2 : r1;

			bool xcond = belongs(small.Left, big.Left, big.Left + big.Width) && belongs(small.Left + small.Width, big.Left, big.Left + big.Width);

			bool ycond = belongs(small.Top, big.Top, big.Top + big.Height) && belongs(small.Top + small.Height, big.Top, big.Top + big.Height);

			if (xcond && ycond)
			{
				return small == r1 ? 0 : 1;
			}
			else
				return -1;
		}
	}
}