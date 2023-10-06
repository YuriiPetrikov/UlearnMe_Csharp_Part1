using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 10;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
	}

    public class Sack : ICreature
    {
        int FallingTime = 0;
        public CreatureCommand Act(int x, int y)
        {
            var cmd = new CreatureCommand { DeltaX = 0, DeltaY = 1 };

            if (CanFallTo(x + cmd.DeltaX, y + cmd.DeltaY))
            {
                FallingTime++;
            }
            else
            {
                if (FallingTime > 1)
                {
                    cmd.TransformTo = new Gold();
                }
                FallingTime = 0;
                cmd.DeltaY = 0;
            }
            return cmd;
        }

        private bool CanFallTo(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Game.MapWidth || y >= Game.MapHeight)
                return false;
            var cell = Game.Map.GetValue(x, y);
            
            return (cell == null) || (FallingTime!= 0 && ((cell is Player)));
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        int ICreature.GetDrawingPriority()
        {
            return 10;
        }

        string ICreature.GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var cmd = new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
			
			if (Game.KeyPressed.CompareTo(Keys.Up) == 0 && y - 1 >= 0)
                cmd.DeltaY = -1;
            else if (Game.KeyPressed.CompareTo(Keys.Down) == 0 && y + 1 < Game.MapHeight)
                cmd.DeltaY = 1;
            else if (Game.KeyPressed.CompareTo(Keys.Left) == 0 && x - 1 >= 0)
                cmd.DeltaX = -1;
            else if (Game.KeyPressed.CompareTo(Keys.Right) == 0 && x + 1 < Game.MapWidth)
                cmd.DeltaX = 1;

            if (!CanWalkTo(x + cmd.DeltaX, y + cmd.DeltaY)) cmd.DeltaX = cmd.DeltaY = 0;
            else if (Game.Map.GetValue(x + cmd.DeltaX, y + cmd.DeltaY) is Gold) Game.Scores += 10;
            
            return cmd;
        }

        private bool CanWalkTo(int x, int y)
        {
            if (x < 0 || y < 0 || Game.MapWidth <= x || Game.MapHeight <= y) return false;
            var cell = Game.Map.GetValue(x, y);
            return !(cell is Sack);
		}

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return 10;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }
}
