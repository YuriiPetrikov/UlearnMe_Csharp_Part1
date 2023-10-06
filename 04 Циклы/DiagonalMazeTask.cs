// Вставьте сюда финальное содержимое файла DiagonalMazeTask.cs
namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveToDown(Robot robot, int step) 
		{
			for (int i = 0; i < step; i++)
				robot.MoveTo(Direction.Down);
		}

		public static void MoveToRight(Robot robot, int step)
		{
			for (int i = 0; i < step; i++)
				robot.MoveTo(Direction.Right);
		}
		
		public static void MoveFirstToDown(Robot robot, int width, int height)
		{
			int count = 0;
			int step = (int)System.Math.Round(height * 1.0 / width);
			MoveToDown(robot, step);

			while (count++ < width - 3)
			{
				robot.MoveTo(Direction.Right);
				MoveToDown(robot, step);
			}
		}

		public static void MoveFirstToRight(Robot robot, int width, int height)
		{
			int count = 0;
			int step = (int)System.Math.Round(width * 1.0 / height);
			MoveToRight(robot, step);

			while (count++ < height - 3)
			{
				robot.MoveTo(Direction.Down);
				MoveToRight(robot, step);
			}
		}

		public static void MoveOut(Robot robot, int width, int height)
		{
			bool firstDirectionWidth = width > height;
			
			if(!firstDirectionWidth)
				MoveFirstToDown(robot, width, height);
			else
				MoveFirstToRight(robot, width, height);
		}
	}
}
