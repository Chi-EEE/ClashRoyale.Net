using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using System;
using System.Numerics;

namespace ClashRoyale.Simulator.Types
{
    public abstract class EntityContext
    {
        public Arena Arena { get; set; }

        public EntityContext(Arena arena)
        {
            Arena = arena;
        }

        public abstract void Tick();
    }
}