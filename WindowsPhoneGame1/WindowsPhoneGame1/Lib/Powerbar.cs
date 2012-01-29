using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame1.Lib
{
    public class Powerbar : Sprite
    {
        public Texture2D TextureFill { get; set; }
        public float PercentFull { get; set; }
        public bool IsAnimating { get; set; }

        private int percentDirection = 1;

        public Powerbar()
            : base("powerbar")
        {
            this.PercentFull = 0;
        }

        public override void Load(ContentManager contentManager)
        {
            base.Load(contentManager);
            this.TextureFill = contentManager.Load<Texture2D>("powerbar_fill");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.IsAnimating)
            {
                this.PercentFull += percentDirection * 200 * elapsed;
                if (this.PercentFull >= 100 || this.PercentFull <= 0) this.percentDirection *= -1;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            var sourceRect = new Rectangle(0, 0, this.TextureFill.Width * (int)this.PercentFull / 100, this.TextureFill.Height);
            sb.Draw(this.TextureFill, this.Position, sourceRect, Color.White);
            base.Draw(sb);
        }
    }
}
