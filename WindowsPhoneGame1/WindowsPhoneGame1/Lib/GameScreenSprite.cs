using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame1.Lib.Screens;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame1.Lib
{
    public class GameScreenSprite : AnimatedSprite
    {
        protected GameScreen gameScreen;

        public GameScreenSprite(string texturePath, int frameWidth, int frameHeight, int frameChangeInterval, GameScreen gameScreen)
            : base(texturePath, frameWidth, frameHeight, frameChangeInterval)
        {
            this.gameScreen = gameScreen;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            this.RenderPosition = CalculateNextRenderPosition(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            //base.Draw(sb);
            this.RenderAtPosition(sb, this.RenderPosition);
        }

        public override Vector2 CalculateNextRenderPosition(GameTime gameTime)
        {
            return this.Position + this.gameScreen.SceneOffset;
        }


        public override Rectangle CalculateNextBounds(GameTime gameTime)
        {
            var nextPos = this.CalculateNextRenderPosition(gameTime) - this.Origin;
            return new Rectangle((int)nextPos.X, (int)nextPos.Y, this.frameWidth, this.frameHeight);
        }
    }
}
