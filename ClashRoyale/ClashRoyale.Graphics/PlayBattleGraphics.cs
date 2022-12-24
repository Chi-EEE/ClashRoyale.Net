using ClashRoyale.Game;
using ClashRoyale.Game.Types;
using SFML.Graphics;
using SFML.System;
using System.Diagnostics;

namespace ClashRoyale.Graphics
{
    class PlayBattleGraphics : PlayBattle
    {
        RenderWindow RenderWindow;
        public PlayBattleGraphics(RenderWindow renderWindow)
        {
            this.RenderWindow = renderWindow;
        }
        protected override void GameLoop()
        {
            Clock clock = new();
            //float timeSinceLastUpdate = 0.0f;
            const float fps = 60;
            float TIME_UNTIL_UPDATE = 1.0f / fps; // 60 fps

            float totalTimeBeforeUpdate = 0.0f;
            float previousTimeElapsed = 0.0f;
            float deltaTime = 0.0f;
            float totalTimeElapsed = 0.0f;
            while (this.battleState == BattleState.InProgress)
            {
                this.RenderWindow.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if (totalTimeElapsed >= TIME_UNTIL_UPDATE)
                {
                    Update(deltaTime);
                }
                Draw();
            }
        }
        protected void Draw()
        {
            this.RenderWindow.Clear(Color.White);
            foreach (EntityContext entityContext in this.Arena.Entities)
            {
                CircleShape circle = new CircleShape(entityContext.EntityInformation.CollisionRadius / 20);
                circle.Position = new Vector2f(entityContext.Position.X, entityContext.Position.Y);
                circle.FillColor = Color.Black;
                this.RenderWindow.Draw(circle);
            }
            this.RenderWindow.Display();
        }
    }
}
