﻿using ClashRoyale.Files.CsvLogic;
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
        public List<EntityContext> Entities = new List<EntityContext>();
        private Grid grid = new(36, 64);
        public Arena()
        {
        }
        public void Start()
        {
            Entities.Add(new TroopContext(this, Csv.Tables.Get(Csv.Files.Characters).GetDataWithInstanceId<Characters>(0), new Vector2(0, 0)));
        }
        public void Tick()
        {
            foreach (var ctx in Entities)
            {
                ctx.Tick();
            }
        }
    }
}
