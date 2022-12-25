using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Game;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Types;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Numerics;

namespace ClashRoyale.Graphics
{
    public class GraphicsGameLoop : GameLoop
    {
        private readonly PlayBattle PlayBattle;
        public GameTime GameTime { get; }
        private readonly RenderWindow RenderWindow;
        private readonly float ZOOM;
        public GraphicsGameLoop(PlayBattle playBattle, RenderWindow renderWindow)
        {
            this.PlayBattle = playBattle;
            this.RenderWindow = renderWindow;
            this.GameTime = new();
            this.ZOOM = Arena.REAL_ARENA_HEIGHT / this.RenderWindow.Size.Y;
            Initalize();
        }
        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                this.PlayBattle.Arena.Entities.Add(new EntityContext(this.PlayBattle.Arena, Csv.Tables.Get(Csv.Files.Characters).GetDataWithInstanceId<Characters>(0), new Vector2((e.X - (this.RenderWindow.Size.X / 2)) * this.ZOOM, (e.Y - (this.RenderWindow.Size.Y / 2)) * this.ZOOM)));
            }
        }
        public void Initalize()
        {
            View view = new(new FloatRect(0, 0, Convert.ToSingle(this.RenderWindow.Size.X), Convert.ToSingle(this.RenderWindow.Size.Y)));
            view.Zoom(this.ZOOM);
            this.RenderWindow.SetView(view);
            this.RenderWindow.MouseButtonPressed += MouseButtonPressed;
        }
        public void Run()
        {
            Clock clock = new();

            float totalTimeBeforeUpdate = 0.0f;
            float previousTimeElapsed = 0.0f;
            float deltaTime;
            float totalTimeElapsed;
            while (this.PlayBattle.battleState == BattleState.InProgress)
            {
                this.RenderWindow.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if (totalTimeElapsed >= GameLoop.TIME_UNTIL_UPDATE)
                {
                    this.GameTime.Update(deltaTime, totalTimeElapsed);
                    totalTimeBeforeUpdate = 0.0f;
                    Update(this.GameTime);
                    Draw();
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            PlayBattle.Update(gameTime);
        }

        public void Draw()
        {
            this.RenderWindow.Clear(Color.White);
            foreach (EntityContext entityContext in this.PlayBattle.Arena.Entities)
            {
                var radius = entityContext.EntityInformation.CollisionRadius;
                CircleShape circle = new CircleShape(radius);
                circle.Position = new Vector2f(entityContext.Position.X - radius, entityContext.Position.Y - radius);
                circle.FillColor = Color.Black;
                this.RenderWindow.Draw(circle);
            }
            this.RenderWindow.Display();
        }
    }
}
