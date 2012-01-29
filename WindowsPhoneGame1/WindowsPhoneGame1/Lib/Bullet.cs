using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame1.Lib.Screens;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame1.Lib
{
    public class Bullet : AnimatedSprite
    {
        public float Power { get; set; }
        public float AltitudeSpeed { get; set; }
        public float Altitude { get; set; }

        public Bullet(GameScreen gameScreen)
            : base("bullet", 16, 16, 0.05f)
        {
            this.Load(gameScreen.Game.Content);
            //this.Origin = new Vector2(8, 8);
            this.Paused = false;
        }

        public override void Load(ContentManager contentManager)
        {
            base.Load(contentManager);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // make up the initial bitmap rotation
            this.Rotation -= (float)Math.PI / 2;

            this.AltitudeSpeed += -1 * elapsed;
            this.Altitude += AltitudeSpeed * elapsed;

            //if (this.Altitude < 0) this.Speed = 0;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            this.Scale = 1;
            base.Draw(sb);

        }
    }
}
