using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Game;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Types;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Graphics
{
    public class BattleGameLoop : GameLoop
    {
        private readonly PlayBattle PlayBattle;
        public RenderWindow RenderWindow { get; set; }
        public GameTime GameTime { get; set; }

        public BattleGameLoop(PlayBattle playBattle, RenderWindow renderWindow)
        {
            this.PlayBattle = playBattle;
            this.RenderWindow = renderWindow;
            this.GameTime = new();
            Initalize();
        }
        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                this.PlayBattle.Arena.Entities.Add(new EntityContext(this.PlayBattle.Arena, Csv.Tables.Get(Csv.Files.Characters).GetDataWithInstanceId<Characters>(0), new Vector2(e.X, e.Y)));
            }
        }
        public void Initalize()
        {
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
        public void Draw()
        {
            this.RenderWindow.Clear(Color.White);
            foreach (EntityContext entityContext in this.PlayBattle.Arena.Entities)
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
