using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using WindowsPhoneGame1.Lib.Screens;

namespace WindowsPhoneGame1.Lib
{
    public class StartButton : Button
    {
        Texture2D texturePressed;
        Texture2D textureUnpressed;
        StartScreen screen;

        public bool IsEnabled { get; set; }

        public StartButton(StartScreen screen)
            : base("button1")
        {
            IsEnabled = true;
        }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            base.Load(contentManager);
            textureUnpressed = this.Texture;
            texturePressed = contentManager.Load<Texture2D>("button2");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.IsEnabled)
            {
                if (this.IsPressed) Texture = texturePressed;
                else Texture = textureUnpressed;
                base.Update(gameTime);
            }
        }
    }
}
