using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Tweede_Grid_Oorlog
{

    public class Game1 : Game
    {
        public static Point screenSize = new Point(1080, 1920);
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameEnvironment gameEnvironment;
        InputHelper inputHelper;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = true;
            //this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
            graphics.PreferredBackBufferHeight = screenSize.X;
            graphics.PreferredBackBufferWidth = screenSize.Y;
        }


        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameEnvironment = new GameEnvironment();
            inputHelper = new InputHelper();

        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameEnvironment.HandleInput(inputHelper);
            gameEnvironment.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            gameEnvironment.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
