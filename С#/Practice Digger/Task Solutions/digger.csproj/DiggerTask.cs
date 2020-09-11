using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.

    public class Terrain : ICreature
    {
        public string GetImageFileName() => "Terrain.png";

        public int GetDrawingPriority() => 110;

        public CreatureCommand Act(int x, int y) => new CreatureCommand { DeltaX = 0, DeltaY = 0 };

        public bool DeadInConflict(ICreature conflictedObject) => (conflictedObject is Player);
    }

    public class Player : ICreature
    {
        public string GetImageFileName() => "Digger.png";

        public int GetDrawingPriority() => 100;

        public CreatureCommand Act(int x, int y)
        {

            CreatureMovement player = new CreatureMovement();

            

            player.Right = new bool[] { Game.KeyPressed == Keys.Right, 
                                        x < Game.MapWidth - 1 && !(Game.Map[x + 1, y] is Sack)};
            player.Left = new bool[] { Game.KeyPressed == Keys.Left, x > 0 && !(Game.Map[x - 1, y] is Sack)};
            player.Up = new bool[] { Game.KeyPressed == Keys.Up, y > 0 && !(Game.Map[x, y - 1] is Sack)};
            player.Down = new bool[] { Game.KeyPressed == Keys.Down, y < Game.MapHeight - 1 && !(Game.Map[x, y + 1] is Sack)};

            return player.Move();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack) return true;
            //if (conflictedObject is Monster) return true;
            return false;
        }
    }

	public class Sack : ICreature
	{
		public string GetImageFileName() => "Sack.png";

		public int GetDrawingPriority() => 100;

		public CreatureCommand Act(int x, int y)
		{
            CreatureMovement sack = new CreatureMovement();

            var downobj = Game.Map[x, (y + 1) % Game.MapHeight];

            //bool kill = (downobj is Player || downobj is Monster) && highCount > 0;
            bool kill = (downobj is Player) && highCount > 0;


            sack.Down = new bool[] { downobj is null || kill, y < Game.MapHeight - 1};

            if (!sack.Down.Contains(false))
            {
                highCount += 1;
            }
            else if (highCount > 1)
                return new CreatureCommand { TransformTo = new Gold() };
            else
                highCount = 0;

            return sack.Move();
        }

		public bool DeadInConflict(ICreature conflictedObject) => false;

        private int highCount = 0;
	}

    public class Gold : ICreature
    {
        public string GetImageFileName() => "Gold.png";

        public int GetDrawingPriority() => 100;

        public CreatureCommand Act(int x, int y) => new CreatureCommand { DeltaX = 0, DeltaY = 0 };

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (!(conflictedObject is Sack))
            {
                if (conflictedObject is Player)
                    Game.Scores += 10;
                return true;
			}
            else
            {
                return false;
			}
        }
    }

  //  public class Monster : ICreature
  //  {
  //      public string GetImageFileName() => "Monster.png";

  //      public int GetDrawingPriority() => 100;

  //      public CreatureCommand Act(int x, int y)
  //      {
            
  //      }

  //      public bool DeadInConflict(ICreature conflictedObject)
  //      {
  //          if (conflictedObject is Sack) return true;
  //          if (conflictedObject is Monster) return true;

  //          return false;
		//}

  //  }


    public class CreatureMovement
    {
        public bool[] Right = null;
        public bool[] Left = null;
        public bool[] Up = null;
        public bool[] Down = null;

        public static int AllTrue(bool[] array) => ((array != null) && !array.Contains(false) ? 1 : 0);

        public CreatureCommand Move()
        {
            int dX = (AllTrue(Right) - AllTrue(Left));
            int dY = (AllTrue(Down) - AllTrue(Up));

            return new CreatureCommand { DeltaX = dX, DeltaY = dY };
        }
	}

 //   public class MonsterAI
 //   {
 //       public Vector FirstPoint;

 //       public TreePath PathToPlayer;

 //       public TreePath BuildPath(Vector startPoint, TreePath previousOne)
 //       {
 //           var node = new TreePath();
 //           node.Value = startPoint;

 //           if (startPoint.GetICreature() is Player)
 //               return node;




	//	}
	//}

    public class TreePath 
    {
        public TreePath[] PathDirectons = new TreePath[4];

        public Vector Value;
	}

    static class VectorExt
    {
        public static ICreature GetICreature(this Vector v)
        {
            return Game.Map[(int)v.X, (int)v.Y];
		}
	}

    enum Direction 
    {
        Up,
        Down,
        Right,
        Left
	}
}
