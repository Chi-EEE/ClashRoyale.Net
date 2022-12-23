namespace ClashRoyale.Simulator.Types
{
    public class Troop
    {
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public float DeployTime { get; set; }
        public Troop(int level, int hitpoints, float deployTime) { 
            Level = level;
            Hitpoints = hitpoints;
            DeployTime = deployTime;
        }
    }
}
