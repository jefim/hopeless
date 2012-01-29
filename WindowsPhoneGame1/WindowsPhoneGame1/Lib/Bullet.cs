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
        public float Power { get; set; }
        public float AltitudeSpeed { get; set; }
        public float Altitude { get; set; }

        public Bullet(GameScreen gameScreen)
            : base("bullet", 16, 16, 0.05f, gameScreen)
        {
            this.Load(gameScreen.Game.Content);
            //this.Origin = new Vector2(8, 8);
            this.Paused = false;
            this.IsVisible = true;
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

            this.AltitudeSpeed += -130 * elapsed;
            this.Altitude += AltitudeSpeed * elapsed;

            if (this.Altitude < 0) this.Speed = 0;

            if (this.Scale < 1 && !alreadyDidHitTest)
            {
                foreach (var target in this.gameScreen.Enemies.Where(o => o.IsVisible).ToList())
                {
                    if (target.GetBounds().Intersects(this.GetBounds()))
                    {
                        target.IsVisible = false;
                    }
                }
            }

            if (this.Scale < -5) this.IsVisible = false;
        }

        private bool alreadyDidHitTest = false;

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            if (this.IsVisible)
            {
                if (this.Scale > 1)
                {
                    this.Scale = 1 + this.Altitude / 20;
                }
                else
                {
                    this.Scale = 1 + this.Altitude / 2;
                }

                base.Draw(sb);
            }

        }
    }
}
