namespace ClashRoyale.Simulator.Types
{
    public class Troop : Entity
    {
        public float DeployTime { get; set; }
        public Troop(int level, int hitpoints, float deployTime): base(level, hitpoints)
        {
            DeployTime = deployTime;
        }
    }
}
