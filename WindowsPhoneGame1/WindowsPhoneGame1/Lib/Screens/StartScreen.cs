using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsPhoneGame1.Lib.Screens
{
    public class StartScreen : SpriteManager
    {
        public StartButton StartButton = null;
        public StartScreen(Game1 game)
            : base(game)
        {
            var sprite = new Sprite("main");
            this.Add(sprite);

            var button = new StartButton(this);
            button.Released += new EventHandler<TouchEventArgs>(button_Released);
            this.Add(button);
            this.StartButton = button;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        void button_Released(object sender, TouchEventArgs e)
        {
            this.IsActive = false;
            if (this.Game1.GameScreen != null)
            {
                this.Game1.GameScreen.IsActive = false;
                this.Game1.GameScreen.Dispose();
                this.Game1.Components.Remove(this.Game1.GameScreen);
            }
            var gameScreen = new GameScreen(this.Game1);
            this.Game1.GameScreen = gameScreen;
            this.Game1.Components.Add(gameScreen);
            gameScreen.Initialize();
            gameScreen.Load();

            this.Game1.GameScreen.IsActive = true;

            this.StartButton.IsEnabled = false;
        }
    }
}
