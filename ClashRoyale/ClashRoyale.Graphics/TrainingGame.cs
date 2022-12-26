using System.Collections.Generic;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using ClashRoyale.Game;

namespace ClashRoyale.Graphics
{
    public class TrainingGame
    {
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
            Environment.Exit(0);
        }
        public async Task PlayAsync()
        {
            // Create the main window
            RenderWindow window = new RenderWindow(new VideoMode(Arena.REAL_ARENA_WIDTH / 50, Arena.REAL_ARENA_HEIGHT / 50), "SFML Game", Styles.Titlebar);
            window.Closed += OnClose;
            window.SetFramerateLimit(24);
            window.SetVerticalSyncEnabled(true); // Don't want to overuse my GPU!
            PlayBattleGraphics playBattleGraphics = new(window);
            await playBattleGraphics.StartBattleAsync();
        }
    }
}