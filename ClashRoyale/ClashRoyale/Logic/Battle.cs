using System;
using System.Collections.Generic;
using System.Diagnostics;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Simulator.Types;
using System.Numerics;

namespace ClashRoyale.Simulator
{
    public class Battle
    {
        static private readonly int EndTick = 300000; // 5 Minutes = 300,000 milliseconds
        public Arena Arena { get; set; }
        public int BattleTick { get; set; }
        public BattleState BattleState = BattleState.InProgress;
        public Battle()
        {
            Arena = new Arena();
        }

        public void Start()
        {
            Arena.Start();
            while (BattleState == BattleState.InProgress) {
                Tick();
            }
        }

        public void Tick()
        {
            var st = new Stopwatch();
            st.Start();
            Arena.Tick();
            st.Stop();

            BattleTick++;

            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine($"Tick {BattleTick} done in {st.ElapsedMilliseconds}ms.");
            //Console.ResetColor();
            if (BattleTick >= EndTick)
            {
                BattleState = BattleState.Draw;
            }
        }
    }
    public enum BattleState
    {
        InProgress,
        Player1Win,
        Player2Win,
        Draw
    }
}
