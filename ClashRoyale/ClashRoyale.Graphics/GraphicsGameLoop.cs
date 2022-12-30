
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Game;
using ClashRoyale.Game.GameLoop;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Numerics;
using ClashRoyale.Game.Logic.Types;
using ClashRoyale.Game.Logic.Types.Entity;

namespace ClashRoyale.Graphics
{
    public class GraphicsGameLoop : GameLoop
    {
        private readonly PlayBattle PlayBattle;
        public GameTime GameTime { get; }
        private readonly RenderWindow RenderWindow;
        private readonly float ZOOM;
        private readonly Texture BackgroundTexture;
        private readonly RectangleShape BackgroundRectangleShape;
        private readonly Texture ArrowTexture;
        private readonly VertexArray VertexArray = new(PrimitiveType.Lines);
        private EntityContext? selectedEntityContext = null;
        public GraphicsGameLoop(PlayBattle playBattle, RenderWindow renderWindow)
        {
            this.PlayBattle = playBattle;
            this.RenderWindow = renderWindow;
            this.GameTime = new();
            this.ZOOM = Arena.REAL_ARENA_HEIGHT / this.RenderWindow.Size.Y;
            this.BackgroundTexture = new("GameAssets/Map.png");
            this.ArrowTexture = new("GameAssets/right-arrow-icon-w.png");
            this.BackgroundRectangleShape = new(new Vector2f(Arena.REAL_ARENA_WIDTH, Arena.REAL_ARENA_HEIGHT));
            Initalize();
        }
        private bool IsPointInCircle(Vector2f point, EntityContext ctx)
        {
            return (Math.Pow(point.X - ctx.Entity.Position.X, 2) + Math.Pow(point.Y - ctx.Entity.Position.Y, 2)) < Math.Pow(ctx.Entity.EntityData.CollisionRadius, 2);
        }
        private int RoundToNearest1000(float n)
        {
            return (int)Math.Round(n / 1000.0f) * 1000 + 500;
        }
        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                Vector2f mousePosition = this.RenderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y));
                mousePosition.X = RoundToNearest1000(mousePosition.X - 500);
                mousePosition.Y = RoundToNearest1000(mousePosition.Y - 500);
                if (selectedEntityContext == null)
                {
                    this.PlayBattle.Arena.Entities.Add(new EntityContext(this.PlayBattle.Arena, Csv.Tables.Get(Csv.Files.Characters).GetDataWithInstanceId<EntityData>(0), new Vector2(mousePosition.X, mousePosition.Y)));
                }
            }
        }
        private void MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                Vector2f mousePosition = this.RenderWindow.MapPixelToCoords(new Vector2i(e.X, e.Y));
                if (selectedEntityContext != null)
                {
                    var velocity = selectedEntityContext.Velocity;
                    velocity.X = (selectedEntityContext.Entity.Position.X - mousePosition.X);
                    velocity.Y = (selectedEntityContext.Entity.Position.Y - mousePosition.Y);
                    selectedEntityContext.Velocity = velocity;
                    selectedEntityContext = null;
                }
            }
        }
        public void Initalize()
        {
            View view = new();
            view.Size = (Vector2f)this.RenderWindow.Size;
            view.Center = new Vector2f(0, 0);
            view.Zoom(this.ZOOM);
            this.RenderWindow.SetView(view);
            this.RenderWindow.MouseButtonPressed += MouseButtonPressed;
            this.RenderWindow.MouseButtonReleased += MouseButtonReleased;

            this.BackgroundRectangleShape.Position = this.RenderWindow.MapPixelToCoords(new Vector2i(0, 0)); ;
            this.BackgroundRectangleShape.Texture = this.BackgroundTexture;

            var cellSize = new Vector2f(Arena.REAL_ARENA_WIDTH / this.PlayBattle.Arena.Tilemap.Grid.Width, Arena.REAL_ARENA_HEIGHT / this.PlayBattle.Arena.Tilemap.Grid.Height);
            var centerOfArena = new Vector2f(Arena.REAL_ARENA_WIDTH, Arena.REAL_ARENA_HEIGHT) / 2.0f;
            for (int x = 0; x <= this.PlayBattle.Arena.Tilemap.Grid.Width; x++)
            {
                this.VertexArray.Append(new Vertex(new Vector2f(x * cellSize.X, 0) - centerOfArena, Color.Black));
                this.VertexArray.Append(new Vertex(new Vector2f(x * cellSize.X, Arena.REAL_ARENA_HEIGHT) - centerOfArena, Color.Black));
            }
            for (int y = 0; y <= this.PlayBattle.Arena.Tilemap.Grid.Height; y++)
            {
                this.VertexArray.Append(new Vertex(new Vector2f(0, y * cellSize.Y) - centerOfArena, Color.Black));
                this.VertexArray.Append(new Vertex(new Vector2f(Arena.REAL_ARENA_WIDTH, y * cellSize.Y) - centerOfArena, Color.Black));
            }
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
            this.RenderWindow.Draw(this.BackgroundRectangleShape);
            foreach (EntityContext entityContext in this.PlayBattle.Arena.Entities)
            {
                var radius = entityContext.Entity.EntityData.CollisionRadius;
                CircleShape circle = new CircleShape(radius);
                circle.Origin = new Vector2f(radius, radius);
                circle.Position = new Vector2f(entityContext.Entity.Position.X, entityContext.Entity.Position.Y);
                circle.Texture = this.ArrowTexture;
                circle.Rotation = entityContext.Rotation;
                circle.FillColor = Color.Black;
                this.RenderWindow.Draw(circle);
            }
            foreach (Projectile projectile in this.PlayBattle.Arena.Projectiles)
            {
                var radius = projectile.ProjectileData.Radius;
                if (radius == 0)
                {
                    radius = 50;
                }
                CircleShape circle = new CircleShape(radius);
                circle.Origin = new Vector2f(radius, radius);
                circle.Position = new Vector2f(projectile.CurrentPosition.X, projectile.CurrentPosition.Y);
                circle.FillColor = Color.Blue;
                this.RenderWindow.Draw(circle);
            }
            this.RenderWindow.Draw(this.VertexArray);

            this.RenderWindow.Display();
        }
    }
}
