using System;
using System.Collections.Generic;
using System.Diagnostics;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Game.Types;
using System.Numerics;
using ClashRoyale.Game.GameLoop;

namespace ClashRoyale.Game
{
    public abstract class PlayBattle
    {
        public Arena Arena { get; set; }
        public float BattleTime { get; set; }
        public BattleState battleState = BattleState.InProgress;
        public PlayBattle()
        {
            Arena = new Arena();
            BattleTime = 0.0f;
        }

        public async Task StartBattleAsync()
        {
            Arena.Start();
        }
        public void Update(GameTime gameTime)
        {
            Arena.Tick(gameTime);

            BattleTime += gameTime.TotalTimeElapsed;
            //BattleTick++;
            //if (BattleTick >= EndTick)
            //{
            //    battleState = BattleState.Draw;
            //}
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
