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

            Map.Draw();


            //camera.Draw(spriteBatch);



            spriteBatch.End();
            //UpdateEventMouse();
            Map.EnteredTile();
            
            
            // TODO: Add your drawing code here
            Desktop.Render();
            base.Draw(gameTime);
        }

        public class Camera
        {
            public float Zoom { get; set; }
            public Vector2 Position { get; set; }
            public Rectangle Bounds { get; protected set; }
            public Rectangle VisibleArea { get; protected set; }
            public Matrix Transform { get; protected set; }

            private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

            public Camera(Viewport viewport)
            {
                Bounds = viewport.Bounds;
                Zoom = 1f;
                Position = Vector2.Zero;
            }


            private void UpdateVisibleArea()
            {
                var inverseViewMatrix = Matrix.Invert(Transform);

                var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
                var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
                var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
                var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

                var min = new Vector2(
                    MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                    MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
                var max = new Vector2(
                    MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                    MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
                VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
            }

            private void UpdateMatrix()
            {
                Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
                UpdateVisibleArea();
            }

            public void MoveCamera(Vector2 movePosition)
            {
                //if (Keyboard.GetState().IsKeyDown(Keys.W))
                    
                Vector2 newPosition = Position + movePosition;
                Position = newPosition;
            }

            public void AdjustZoom(float zoomAmount)
            {
                Zoom += zoomAmount;
                if (Zoom < .35f)
                {
                    Zoom = .35f;
                }
                if (Zoom > 2f)
                {
                    Zoom = 2f;
                }
            }

            public void UpdateCamera(Viewport bounds)
            {
                Bounds = bounds.Bounds;
                UpdateMatrix();

                Vector2 cameraMovement = Vector2.Zero;
                int moveSpeed;

                if (Zoom > .8f)
                {
                    moveSpeed = 15;
                }
                else if (Zoom < .8f && Zoom >= .6f)
                {
                    moveSpeed = 20;
                }
                else if (Zoom < .6f && Zoom > .35f)
                {
                    moveSpeed = 25;
                }
                else if (Zoom <= .35f)
                {
                    moveSpeed = 30;
                }
                else
                {
                    moveSpeed = 10;
                }


                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    cameraMovement.Y = -moveSpeed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    cameraMovement.Y = moveSpeed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    cameraMovement.X = -moveSpeed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    cameraMovement.X = moveSpeed;
                }

                previousMouseWheelValue = currentMouseWheelValue;
                currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

                if (currentMouseWheelValue > previousMouseWheelValue)
                {
                    AdjustZoom(.05f);
                    Console.WriteLine(moveSpeed);
                }

                if (currentMouseWheelValue < previousMouseWheelValue)
                {
                    AdjustZoom(-.05f);
                    Console.WriteLine(moveSpeed);
                }

                previousZoom = zoom;
                zoom = Zoom;
                if (previousZoom != zoom)
                {
                    Console.WriteLine(zoom);

                }

                MoveCamera(cameraMovement);
            }
        }
    }
}
