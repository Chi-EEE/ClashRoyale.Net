using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Simulator.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Simulator
{
    public class Arena
    {
        static readonly int WIDTH = 3375;
        static readonly int HEIGHT = 6000;
        private List<TroopContext> Troops = new List<TroopContext>();
        private Grid
        public Arena()
        {
        }
        public void Start()
        {
            Troops.Add(new TroopContext(this, Csv.Tables.Get(Csv.Files.Characters).GetDataWithInstanceId<Characters>(0), new Vector2(0, 0)));
        }
        public void Tick()
        {
            foreach (var ctx in Troops)
            {
                ctx.Tick();
            }
        }
    }
}
