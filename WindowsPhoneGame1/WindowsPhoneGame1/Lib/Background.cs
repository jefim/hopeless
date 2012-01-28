using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame1.Lib
{
    public class Background : Sprite
    {
        public float DistanceToMove = 0.0f;

        public Background(string texturePath)
            : base(texturePath)
        {
            this.Speed = 5;
        }

        public override void Update(GameTime gameTime)
        {
            this.HandleInput();
            //base.Update(gameTime);
            float elapsedS = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.DistanceToMove > 0)
            {
                // calculate movement
                var moveVector = this.Speed * this.Direction * elapsedS;

                // if move length is less that what we need to move - clip the move vector
                if(moveVector.Length() > this.DistanceToMove) moveVector *= this.DistanceToMove / moveVector.Length();

                // reduce move distance
                this.DistanceToMove -= moveVector.Length();

                // if distance is small - stop
                if (this.DistanceToMove < 1) { this.DistanceToMove = 0.0f; }
                this.Position += moveVector;
                this.Position = new Vector2(this.Position.X % Game1.ScreenWidth, this.Position.Y % Game1.ScrrenHeight);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            var dirX = this.Position.X > 0 ? 1 : -1;
            sb.Draw(this.Texture, this.Position - new Vector2(Game1.ScreenWidth * dirX, 0), Color.White);

            var dirY = this.Position.X > 0 ? -1 : 1;
            sb.Draw(this.Texture, this.Position - new Vector2(0, Game1.ScrrenHeight * dirY), Color.White);

            sb.Draw(this.Texture, this.Position - new Vector2(Game1.ScreenWidth * dirX, Game1.ScrrenHeight * dirY), Color.White);

            sb.Draw(this.Texture, this.Position, Color.White);
        }

        protected override void OnTouch(TouchLocation touch)
        {
            base.OnTouch(touch);

            this.Direction = -1 * (touch.Position - new Vector2(Game1.ScreenWidth / 2, Game1.ScrrenHeight / 2));
            this.DistanceToMove = this.Direction.Length();
            this.Direction.Normalize();
        }
    }
}
