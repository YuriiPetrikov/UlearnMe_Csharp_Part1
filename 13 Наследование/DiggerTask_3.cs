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
	public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var cmd = new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = this };
            if (IsPlayerInSection(0, 0, x, Game.MapHeight) && CanWalkTo(x - 1, y))
                cmd.DeltaX = -1;
            else if (IsPlayerInSection(x + 1, 0, Game.MapWidth, Game.MapHeight) &&
                     CanWalkTo(x + 1, y))
                cmd.DeltaX = 1;
            else if (IsPlayerInSection(0, 0, Game.MapWidth, y) && CanWalkTo(x, y - 1))
                cmd.DeltaY = -1;
            else if (IsPlayerInSection(0, y + 1, Game.MapWidth, Game.MapHeight) &&
                     CanWalkTo(x, y + 1))
                cmd.DeltaY = 1;
            return cmd;
        }

        private bool IsPlayerInSection(int x0, int y0, int x1, int y1)
        {
            for (var x = x0; x < x1; ++x)
            {
                for (var y = y0; y < y1; ++y)
                {
                    if (Game.Map.GetValue(x, y) is Player) return true;
                }
            }
            return false;
        }

        private bool CanWalkTo(int x, int y)
        {
            if (x < 0 || y < 0 || Game.MapWidth <= x || Game.MapHeight <= y) return false;
            var cell = Game.Map.GetValue(x, y);
            return (cell == null) ||
                !((cell is Sack) || (cell is Monster) || (cell is Terrain));
        }

        public bool DeadInConflict(ICreature conflictedObject) =>
            (conflictedObject is Monster) ||
            ((conflictedObject is Sack) && (conflictedObject as Sack).IsFalling());

        public int GetDrawingPriority()
        {
            return 20;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
		}
    }

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
        private int counter = 0;
        public CreatureCommand Act(int x, int y)
        {
            var cmd = new CreatureCommand { DeltaX = 0, DeltaY = 1 };

            if (CanFallTo(x + cmd.DeltaX, y + cmd.DeltaY))
            {
                counter++;
            }
            else
            {
                if (counter > 1)
                {
                    cmd.TransformTo = new Gold();
                }
                counter = 0;
                cmd.DeltaY = 0;
            }
            return cmd;
        }

        private bool CanFallTo(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Game.MapWidth || y >= Game.MapHeight)
                return false;
            var cell = Game.Map.GetValue(x, y);

            return (cell == null) || (IsFalling() && ((cell is Player) || (cell is Monster)));
        }

        public bool IsFalling()
        {
            return counter > 0;
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
            return 2;
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
            if (conflictedObject is Gold)
                Game.Scores += 10;
            return (conflictedObject is Sack) || (conflictedObject is Monster);
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
