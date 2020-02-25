using Game1.Engine;
using Game1.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        GUI.GUI menu;
        Camera2d camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 728;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
           
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Console.WriteLine("test");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameConfig.graphics = GraphicsDevice;
            GameConfig.spriteBatch = spriteBatch;

            MyraEnvironment.Game = this;
            //Загрузка текстур
            Textures.LoadTextures(GraphicsDevice);
            //Создание GUI
            menu = new GUI.GUI(graphics);
            //Создание карты
            //map = new Map(20, GraphicsDevice, spriteBatch);

            camera = new Camera2d();
            
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

            
            Maps.Update();

            camera.Update();

            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(transformMatrix: camera.get_transformation(GraphicsDevice));

            Maps.Draw();


            //camera.Draw(spriteBatch);



            spriteBatch.End();
            
            
            
            // TODO: Add your drawing code here
            Desktop.Render();
            base.Draw(gameTime);
        }

        
    }
}
