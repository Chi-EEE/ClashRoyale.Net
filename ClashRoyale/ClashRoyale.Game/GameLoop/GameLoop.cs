using SFML.Graphics;

namespace ClashRoyale.Game.GameLoop
{
    public interface GameLoop
    {
        RenderWindow RenderWindow
        {
            get;
            protected set;
        }
        GameTime GameTime
        {
            get;
            protected set;
        }
        const int TARGET_FPS = 60;
        const float TIME_UNTIL_UPDATE = 1.0f / TARGET_FPS;
        //public GameLoop(RenderWindow renderWindow) : base()
        //{
        //    this.RenderWindow = renderWindow;
        //    this.GameTime = new();
        //    Initalize();
        //}
        void Initalize();
        void Run();
        void Draw();
    }
}
