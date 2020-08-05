using System.Runtime.ExceptionServices;

namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			width -= 3;
			height -= 3;
			for (int i = 0; i < (width + height); i++)
				StepExecution(robot, width, i);
		}

		static void StepExecution(Robot robot, int width, int step)
		{
			if ((step - width) < 0)
				robot.MoveTo(Direction.Right);
			else
				robot.MoveTo(Direction.Down);
		}
	}
}