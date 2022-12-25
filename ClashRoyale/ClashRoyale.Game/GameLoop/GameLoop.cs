using SFML.Graphics;

namespace ClashRoyale.Game.GameLoop
{
    public interface GameLoop
    {
        const int TARGET_FPS = 60;
        const float TIME_UNTIL_UPDATE = 1.0f / TARGET_FPS;
        GameTime GameTime
        {
            get;
        }
        void Initalize();
        void Update(GameTime gameTime);
        void Run();
        void Draw();
    }
}
