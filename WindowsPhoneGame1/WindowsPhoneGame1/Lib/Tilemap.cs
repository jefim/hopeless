using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame1.Lib
{
    public class Tilemap : Sprite
    {
        public float DistanceToMove = 0.0f;

        public Tilemap(string texturePath)
            : base(texturePath)
        {
            this.Speed = 5;
        }

        private int[,] map =
        {
            { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 9, 9, 10, 9, 9, 9, 9, 9, 10, 9, 9, 9, 9, 9, 9 },
            { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 11, 9, 9, 9, 9 },
            { 9, 0, 3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 9, 1, 4, 7, 9, 9, 11, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 9, 2, 5, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
            { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 }
        };

        private readonly Vector2 tileSize = new Vector2(32, 32);

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
                if (moveVector.Length() > this.DistanceToMove) moveVector *= this.DistanceToMove / moveVector.Length();

                // reduce move distance
                this.DistanceToMove -= moveVector.Length();

                // if distance is small - stop
                if (this.DistanceToMove < 1) { this.DistanceToMove = 0.0f; }
                this.Position += moveVector;
                
                //this.Position = new Vector2(this.Position.X % Game1.ScreenWidth, this.Position.Y % Game1.ScrrenHeight);
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            //base.Draw(sb);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    var tilePosition = this.Position + (new Vector2(i * tileSize.X, j * tileSize.Y));
                    var sourceRect = new Rectangle((int)(map[i,j] * tileSize.X), 0, (int)tileSize.X, (int)tileSize.Y);
                    sb.Draw(this.Texture, tilePosition, sourceRect, Color.White);
                }
            }
        }

        protected override void OnTouch(Microsoft.Xna.Framework.Input.Touch.TouchLocation touch)
        {
            base.OnTouch(touch);

            this.Direction = -1 * (touch.Position - new Vector2(Game1.ScreenWidth / 2, Game1.ScrrenHeight / 2));
            this.DistanceToMove = this.Direction.Length();
            this.Direction.Normalize();
        }
    }
}
