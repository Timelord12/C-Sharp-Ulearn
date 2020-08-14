using System;

namespace Mazes
{
	public static class SnakeMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			for (int i = 1; i < height / 2; i++)
            {
				Direction inverseDirection = (Direction)((i % 2) + 2);
				MoveToEnd(robot, inverseDirection, width);
				MoveDown(robot);
            }
			MoveToEnd(robot, Direction.Left, width);
		}

		public static void MoveToEnd(Robot robot, Direction dir, int width)
        {
			for (int i = 0; i < width - 3; i++)
            {
				robot.MoveTo(dir);
            }
        }

		static void MoveDown(Robot robot)
        {
			for (int i = 0; i < 2; i++)
				robot.MoveTo(Direction.Down);
		}
	}
}