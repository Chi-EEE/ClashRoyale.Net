using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Types;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Numerics;

namespace ClashRoyale.Graphics
{
    class PlayBattleGraphics : PlayBattle, GameLoop
    {
        GameLoop BattleGameLoop;
        public PlayBattleGraphics(RenderWindow renderWindow)
        {
            BattleGameLoop = new BattleGameLoop(this, renderWindow);
        }

        RenderWindow GameLoop.RenderWindow
        {
            get
            {
                return BattleGameLoop.RenderWindow;
            }
            set;
        }
        //RenderWindow GameLoop.GameTime
        //{
        //    get
        //    {
        //        return BattleGameLoop.GameTime;
        //    }
        //    set;
        //}

        GameTime GameLoop.GameTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Run()
        {
            BattleGameLoop.Run();
        }
        public void Initalize()
        {
            BattleGameLoop.Initalize();
        }
        public void Draw()
        {
            BattleGameLoop.Draw();
        }
    }
}
