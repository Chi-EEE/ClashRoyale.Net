using ClashRoyale.Game;
using ClashRoyale.Game.Logic;
using SFML.Graphics;

namespace ClashRoyale.Graphics
{
    class PlayBattleGraphics : PlayBattle
    {
        GraphicsGameLoop BattleGameLoop;
        public PlayBattleGraphics(PlayerSide playerSide_1, PlayerSide playerSide_2, RenderWindow renderWindow) : base(playerSide_1, playerSide_2)
        {
            BattleGameLoop = new GraphicsGameLoop(this, renderWindow);
        }
        public new async Task StartBattleAsync()
        {
            await base.StartBattleAsync();
            BattleGameLoop.Run();
        }
    }
}
