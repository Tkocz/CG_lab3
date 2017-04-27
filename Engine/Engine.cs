using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static Manager.Core;


namespace Manager
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Engine : Game
    {
        private static Engine inst;
        private GameImpl gameImpl;
        public GraphicsDeviceManager graphics;
        public readonly List<Core> Subsystems = new List<Core>();
        public readonly Dictionary<int, Entity> Entities = new Dictionary<int, Entity>();
        private int entityId = 1;

        public Entity addEntity(Component[] components)
        {
            var entity = new Entity();
            entity.id = entityId++;
            entity.AddComponents(components);
            Entities[entity.id] = entity;
            return entity;
        }
        public Engine(GameImpl gameImpl)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
			//graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            this.gameImpl = gameImpl;
        }
        public static void RunGame(GameImpl gameImpl)
        {
            inst = new Engine(gameImpl);
            inst.Run();
        }
        public static Engine GetInst()
        {
            return inst;
        }

        protected override void Initialize()
        {
            gameImpl.init();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice); // Not needed for this project
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var subsystem in Subsystems)
            {
                subsystem.update(gameTime);
            }
            gameImpl.update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (var subsystem in Subsystems)
            {
                subsystem.draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}