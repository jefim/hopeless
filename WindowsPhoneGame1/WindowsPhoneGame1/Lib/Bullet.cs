using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame1.Lib.Screens;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame1.Lib
{
    public class Bullet : GameScreenSprite
    {
        public Bullet(GameScreen gameScreen)
            : base("bullet", 16, 16, 0.05f, gameScreen)
        {
            this.Load(gameScreen.Game.Content);
            this.Origin = new Vector2(8, 8);
            this.Paused = false;
        }

        public override void Load(ContentManager contentManager)
        {
            base.Load(contentManager);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // make up the initial bitmap rotation
            this.Rotation -= (float)Math.PI / 2;
        }
    }
}
