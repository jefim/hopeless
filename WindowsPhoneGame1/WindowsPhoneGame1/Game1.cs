using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using WindowsPhoneGame1.Lib;
using WindowsPhoneGame1.Lib.Screens;

namespace WindowsPhoneGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public GameScreen GameScreen = null;
        public StartScreen StartScreen = null;
        public Song MySong;
        SoundEffectInstance tankSfx;
        SoundEffect bangSfx;
        SoundEffect boomSfx;
    
        public const int ScreenWidth = 800;
        public const int ScrrenHeight = 480;
        public static Vector2 ScreenSize = new Vector2(ScreenWidth, ScrrenHeight);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferHeight = ScrrenHeight;
            this.graphics.PreferredBackBufferWidth = ScreenWidth;
            this.graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap;

            this.StartScreen = new StartScreen(this);
            this.StartScreen.IsActive = true;
            this.Components.Add(this.StartScreen);

            MySong = Content.Load<Song>("random");
            
            var tankSfx = Content.Load<SoundEffect>("tank");
            this.tankSfx = tankSfx.CreateInstance();
            this.tankSfx.IsLooped = true;
            /*bangSfx = Content.Load<SoundEffect>("bang");
            boomSfx = Content.Load<SoundEffect>("boom");
            */
          
        }

        public SoundEffectInstance TankSfx
        {
            get{
                return tankSfx;
            }
        }
        /*
        public SoundEffect BangSfx
        {
            get
            {
                return bangSfx;
            }
        }

        public SoundEffect BoomSfx
        {
            get
            {
                return boomSfx;
            }
        }
         * */
 
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Touches = TouchPanel.GetState().ToList();

            base.Update(gameTime);
        }

        public static List<TouchLocation> Touches;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
