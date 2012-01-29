using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsPhoneGame1.Lib.Screens;

namespace WindowsPhoneGame1.Lib
{
    public class Tilemap : GameScreenSprite
    {
        public float DistanceToMove = 0.0f;

        public Tilemap(string texturePath, GameScreen gameScreen)
            : base(texturePath, 128, 128, 1, gameScreen)
        {
        }

        private int[,] map =
        {
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 },
            /*
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},

            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},

            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},
            { 1, 2, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 2, 1, 2},*/
        };

        private readonly Vector2 tileSize = new Vector2(128, 128);

        public override void Update(GameTime gameTime)
        {
            //this.HandleInput();
            base.Update(gameTime);
            //float elapsedS = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (this.DistanceToMove > 0)
            //{
                
            //    // calculate movement
            //    var moveVector = this.Speed * this.Direction * elapsedS;

            //    // if move length is less that what we need to move - clip the move vector
            //    if (moveVector.Length() > this.DistanceToMove) moveVector *= this.DistanceToMove / moveVector.Length();

            //    // reduce move distance
            //    this.DistanceToMove -= moveVector.Length();

            //    // if distance is small - stop
            //    if (this.DistanceToMove < 1) { this.DistanceToMove = 0.0f; }
            //    this.Position += moveVector;
                
            //    //this.Position = new Vector2(this.Position.X % Game1.ScreenWidth, this.Position.Y % Game1.ScrrenHeight);
            //}
        }

        public override void Draw(SpriteBatch sb)
        {
            //base.Draw(sb);
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    var tilePosition = this.gameScreen.SceneOffset + this.Position + (new Vector2(i * tileSize.X, j * tileSize.Y));
                    var sourceRect = new Rectangle((int)(map[j, i]%16 * tileSize.X), (int)(map[j, i]/16 * tileSize.Y), (int)tileSize.X, (int)tileSize.Y);
                    sb.Draw(this.Texture, tilePosition, sourceRect, Color.White);
                }
            }
        }

        protected override void OnTouch(Microsoft.Xna.Framework.Input.Touch.TouchLocation touch)
        {
            base.OnTouch(touch);
            //if (touch.State == TouchLocationState.Moved)
            //{
            //    this.Direction = -1 * (touch.Position - new Vector2(Game1.ScreenWidth / 2, Game1.ScrrenHeight / 2));
            //    this.DistanceToMove = this.Direction.Length();
            //    this.Direction.Normalize();
            //}
        }
    }
}
