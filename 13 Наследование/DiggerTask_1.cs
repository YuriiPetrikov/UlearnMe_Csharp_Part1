// Вставьте сюда финальное содержимое файла DiggerTask.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand() {DeltaX = 0, DeltaY = 0};
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            //return "Terrain.png";
            //return "Sack.png";
            return "Gold.png";
        }
    }

    public class Player : ICreature
    {
        CreatureCommand ICreature.Act(int x, int y)
        {
            if (Game.KeyPressed.CompareTo(Keys.Up) == 0 && y - 1 >= 0)
                return new CreatureCommand() { DeltaX = 0, DeltaY = -1};
            else if (Game.KeyPressed.CompareTo(Keys.Down) == 0 && y + 1 < Game.MapHeight)
                return new CreatureCommand() { DeltaX = 0, DeltaY = 1};
            else if (Game.KeyPressed.CompareTo(Keys.Left) == 0 && x - 1 >= 0)
                return new CreatureCommand() { DeltaX = -1, DeltaY = 0};
            else if (Game.KeyPressed.CompareTo(Keys.Right) == 0 && x + 1 < Game.MapWidth)
                return new CreatureCommand() { DeltaX = 1, DeltaY = 0};
            else
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0};
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject == this;
        }

        int ICreature.GetDrawingPriority()
        {
            return 1;
        }

        string ICreature.GetImageFileName()
        {
            return "Digger.png";
        }
    }
}
