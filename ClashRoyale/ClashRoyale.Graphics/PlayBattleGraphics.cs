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
    class PlayBattleGraphics : PlayBattle
    {
        GraphicsGameLoop BattleGameLoop;
        public PlayBattleGraphics(RenderWindow renderWindow)
        {
            BattleGameLoop = new GraphicsGameLoop(this, renderWindow);
            BattleGameLoop.Run();
        }
    }
}
