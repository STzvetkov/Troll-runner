using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Pickup : Obstacle
    {
        public const int NumberOfRows = 3;
        public const int NumberOfCols = 4;
        public enum PickupType {Slow, Fire, Life};

        private PickupType bonusType;
        private int duration;
        public bool IsActive { get; private set; }
        public Pickup(int x, int y, PickupType type)
            : base(x, y)
        {
            this.form = new char[NumberOfRows, NumberOfCols];
            this.bonusType = type;
            this.IsActive = false;
            this.duration = 0;
            FillPickup();             
        }

        public void Activate()
        {
            switch (this.bonusType)
            {
                case PickupType.Slow:
                    this.IsActive = true;
                    this.duration = Game.generator.Next(10000, 30001);
                    Game.gameSpeed = Game.MinSpeed;
                    break;
                case PickupType.Fire:
                    this.IsActive = true;
                    this.duration = Game.generator.Next(5000, 20001);
                    Game.ableToFire = true;
                    break;
                case PickupType.Life:
                    Game.currentLifes = Math.Min(Game.InitialLifes, Game.currentLifes + 1);
                    break;
            }
        }

        public void Deactivate()
        {

            this.IsActive = false;
            switch (this.bonusType)
            {
                case PickupType.Slow:
                    Game.gameSpeed = Math.Min(Game.MaxSpeed, Game.gameSpeed + 1);
                    break;
                case PickupType.Fire:
                    Game.ableToFire = false;
                    break;
                default:
                    break;
            }
        }

        public void Expire ()
        {
            this.duration = Math.Max(0, this.duration - Game.SleepTime);
            if ( this.duration == 0)
            {
            }
        }
        private void FillPickup()
        {
            string fileName = this.bonusType.ToString();
            this.form = GraphicsManagement.GetGraphic(fileName);
        }
    }
}
