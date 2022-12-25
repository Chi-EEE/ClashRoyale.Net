using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game;
using ClashRoyale.Game.Types;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Diagnostics;
using System.Numerics;

namespace ClashRoyale.Graphics
{
    class PlayBattleGraphics : PlayBattle
    {
        public RenderWindow RenderWindow
        {
            get;
        }
        public GameTime GameTime
        {
            get;
        }
        const int TARGET_FPS = 60;
        const float TIME_UNTIL_UPDATE = 1.0f / TARGET_FPS;
        public PlayBattleGraphics(RenderWindow renderWindow) : base()
        {
            this.RenderWindow = renderWindow;
            this.GameTime = new();
            Initalize();
        }
        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                this.Arena.Entities.Add(new EntityContext(this.Arena, Csv.Tables.Get(Csv.Files.Characters).GetDataWithInstanceId<Characters>(0), new Vector2(e.X, e.Y)));
            }
        }
        private void Initalize()
        {
            this.RenderWindow.MouseButtonPressed += MouseButtonPressed;
        }
        protected override void GameLoop()
        {
            Clock clock = new();

            float totalTimeBeforeUpdate = 0.0f;
            float previousTimeElapsed = 0.0f;
            float deltaTime;
            float totalTimeElapsed;
            while (this.battleState == BattleState.InProgress)
            {
                this.RenderWindow.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if (totalTimeElapsed >= TIME_UNTIL_UPDATE)
                {
                    this.GameTime.Update(deltaTime, totalTimeElapsed);
                    totalTimeBeforeUpdate = 0.0f;
                    Update(this.GameTime);
                    Draw();
                }
            }
        }
        protected void Draw()
        {
            this.RenderWindow.Clear(Color.White);
            foreach (EntityContext entityContext in this.Arena.Entities)
            {
                var radius = entityContext.EntityInformation.CollisionRadius / 20;
                CircleShape circle = new CircleShape(radius);
                circle.Position = new Vector2f(entityContext.Position.X - radius, entityContext.Position.Y - radius);
                circle.FillColor = Color.Black;
                this.RenderWindow.Draw(circle);
            }
            this.RenderWindow.Display();
        }

    }
}
