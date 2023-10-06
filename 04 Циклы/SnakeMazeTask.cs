namespace Mazes
{
	public static class SnakeMazeTask
	{
		static void MoveWidth(Robot robot, int width, bool isRight) 
		{
			while (width != 3)
			{
				if (isRight)
					robot.MoveTo(Direction.Right);
				else
					robot.MoveTo(Direction.Left);
				width--;
			}
		}

		static void MoveDown(Robot robot, int stepCount)
		{
			while (stepCount != 0)
			{
				robot.MoveTo(Direction.Down);
				stepCount--;
			}
		}

		public static void MoveOut(Robot robot, int width, int height)
		{
			bool isRight = true;
			MoveWidth(robot, width, isRight);
			
			while (height > 3)
			{
				isRight = !isRight;
				MoveDown(robot, 2);
				MoveWidth(robot, width, isRight);
				height -= 2;
			}
		}
	}
}
