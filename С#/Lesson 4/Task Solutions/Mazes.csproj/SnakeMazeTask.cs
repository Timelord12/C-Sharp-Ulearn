using System;

namespace Mazes
{
	public static class SnakeMazeTask
	{
		// Просмотрено первой; --J
		public static void MoveOut(Robot robot, int width, int height)
		{
			for (int i = 1; i < height / 2; i++)
            {
				Direction inverseDirection = (Direction)((i % 2) + 2);
				// Строку выше можно вынести в отдельный метод, тогда его можно было бы использовать еще где-нибудь. -J
				MoveToEnd(robot, inverseDirection, width);
				MoveDown(robot);
            }
			MoveToEnd(robot, Direction.Left, width);
		}

		// Более универсальным был бы метод  
		// public static void MoveFor(Robot robot, Direction direction, int count) {}
		// -J
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