﻿using DozyGrove.Managers;
using DozyGrove.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace DozyGrove
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        private Color backgroundColor { get; set; }

        public static Dictionary<string, Texture2D> textures { get; set; }
        public static Dictionary<string, SoundEffect> sounds { get; set; }
        public static Dictionary<string, Animation> animations { get; set; }

        public static Camera2D camera { get; set; }
        public static LocationManager locationManager { get; set; }
        public static InputManager inputManager { get; set; }

        public static Vector2 screenSize { get { return new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height); } }
        public Vector2 windowSize { get { return new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height); } }
        public static Vector2 zoomedScreenSize { get { return screenSize / camera.Zoom; } }
        public static Vector2 topLeft { get { return Vector2.Transform(Vector2.Zero, camera.GetInverseViewMatrix()); } }
        public static Vector2 mousePosition 
        { 
            get 
            {
                Point _mousePos = Mouse.GetState().Position;
                return Vector2.Transform(new Vector2(_mousePos.X, _mousePos.Y), camera.GetInverseViewMatrix()); 
            } 
        }
        public static Random r;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = (int)screenSize.X;
            graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            graphics.IsFullScreen = false;
            r = new Random();
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            backgroundColor = new Color(48, 48, 61);
            camera = new Camera2D(GraphicsDevice) { Zoom = 3, Position = (new Vector2(300, 210) - windowSize) / 2f };
            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textures = new Dictionary<string, Texture2D>()
            {
                { "barriers", Content.Load<Texture2D>("barriers") },
                { "decorations", Content.Load<Texture2D>("decorations") },
                { "soil", Content.Load<Texture2D>("soil") },
                { "house", Content.Load<Texture2D>("house") },
                { "plants", Content.Load<Texture2D>("plants") }
            };
            animations = new Dictionary<string, Animation>()
            {
                { "player_left", new Animation(Content.Load<Texture2D>("player"), 2, 1f) }
            };
            sounds = new Dictionary<string, SoundEffect>()
            {
                { "player_move", Content.Load<SoundEffect>("Sounds/player_move") },
                { "player_block", Content.Load<SoundEffect>("Sounds/player_block") },
            };

            locationManager = new LocationManager();
            inputManager = new InputManager();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            inputManager.Update(gameTime);
            locationManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformMatrix: camera.GetViewMatrix()); GraphicsDevice.Clear(backgroundColor);
            locationManager.Draw(spriteBatch);

            // Temp display renders
            //spriteBatch.DrawRectangle(new Rectangle((int)topLeft.X, (int)topLeft.Y, 10, 10), Color.Red);
            //spriteBatch.DrawRectangle(new Rectangle(0, 0, 300, 210), Color.Green);
            spriteBatch.DrawPoint(mousePosition, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
