using System;
using System.Collections.Generic;
using System.Diagnostics;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Game.Types;
using System.Numerics;

namespace ClashRoyale.Game
{
    public abstract class PlayBattle
    {
        static private readonly int EndTick = 300000; // 5 Minutes = 300,000 milliseconds
        public Arena Arena { get; set; }
        public int BattleTick { get; set; }
        public BattleState battleState = BattleState.InProgress;
        public PlayBattle()
        {
            Arena = new Arena();
        }

        public async Task StartBattleAsync()
        {
            Arena.Start();
            this.GameLoop();
        }
        protected abstract void GameLoop();
        public void Update(float dt)
        {
            Arena.Tick(dt);

            BattleTick++;
            if (BattleTick >= EndTick)
            {
                battleState = BattleState.Draw;
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
