using System.Runtime.ExceptionServices;

namespace Mazes
{
	// Просмотрено вторым; --J
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			width -= 3;
			height -= 3;
			for (int i = 0; i < (width + height); i++)
				StepExecution(robot, width, i);
		}

		// Проверка выполняется на каждой итерации цикла, хотя входные данные не меняются.
		// Это не так дорого, но это можно проверить лишь однажды, что делает действия куда понятнее.
		// Ну и дебажить проще.
		static void StepExecution(Robot robot, int width, int step)
		{
			if ((step - width) < 0)
				robot.MoveTo(Direction.Right);
			else
				robot.MoveTo(Direction.Down);
		}
	}
}