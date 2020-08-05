using System;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			int longSide = Math.Max(width, height) - 2;
			int shortSide = Math.Min(width, height) - 2;
			Direction dirLong = (longSide + 2 == width) ? Direction.Right : Direction.Down;
			Direction dirShort = (longSide + 2 == width) ? Direction.Down : Direction.Right;
			int longOffset = longSide / shortSide;
			DiagonalSteps(robot, dirLong, dirShort, shortSide, longOffset);
		}

		public static void MoveToN(Robot robot, Direction dir, int times)
        {
			for (int i = 0; i < times; i++)
            {
				robot.MoveTo(dir);
            }
        }

		public static void DiagonalSteps(Robot robot, Direction dirLong, Direction dirShort, int shortSide, int longOffset)
        {
			for (int i = 0; i < shortSide; i++)
			{
				MoveToN(robot, dirLong, longOffset);

				if (!robot.Finished)
					robot.MoveTo(dirShort);
			}
		}

		
	}
}