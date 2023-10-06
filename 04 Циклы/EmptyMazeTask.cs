// Вставьте сюда финальное содержимое файла EmptyMazeTask.cs
namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			while (height > 3 || width > 3)
			{
				if(width-- > 3)
					robot.MoveTo(Direction.Right);
				if(height-- > 3)
					robot.MoveTo(Direction.Down);
			}
		}
	}
}
