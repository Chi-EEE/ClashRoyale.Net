using System;
using System.Collections.Generic;
using System.Diagnostics;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Game.Types;
using System.Numerics;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Logic;

namespace ClashRoyale.Game
{
    public abstract class PlayBattle
    {
        PlayerSide PlayerSide_1;
        PlayerSide PlayerSide_2;
        public Arena Arena { get; set; }
        public float BattleTime { get; set; }
        public BattleState battleState = BattleState.InProgress;
        public PlayBattle(PlayerSide playerSide_1, PlayerSide playerSide_2)
        {
            PlayerSide_1 = playerSide_1;
            PlayerSide_2 = playerSide_2;
            Arena = new Arena();
            BattleTime = 0.0f;
        }
        //public void SpawnEntity(Vector2)
        //{
        //    ss
        //}
        public async Task StartBattleAsync()
        {
            Arena.Start();
        }
        public void Update(GameTime gameTime)
        {
            Arena.Tick(gameTime);

            PlayerSide_1.Tick(gameTime);
            PlayerSide_2.Tick(gameTime);

            BattleTime += gameTime.TotalTimeElapsed;
            //if (BattleTime >= EndTick)
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
