using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Tweede_Grid_Oorlog
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameEnvironment gameEnvironment;
        InputHelper inputHelper;
        static AssetHelper assetHelper;
        internal static AssetHelper AssetHelper { get => assetHelper; }

        public Game1()
        {
            this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = true;
            //this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
            graphics.PreferredBackBufferHeight = Constants.screenSize.Y;
            graphics.PreferredBackBufferWidth = Constants.screenSize.X;
            assetHelper = new AssetHelper(Content);
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

            inputHelper.Update(gameTime);
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
