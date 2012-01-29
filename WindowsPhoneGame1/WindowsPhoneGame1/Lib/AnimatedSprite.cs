using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame1.Lib
{
    public class AnimatedSprite : Sprite
    {
        protected int frameWidth;
        protected int frameHeight;
        private int currentFrame;
        private int totalFrames;
        private float frameChangeIntervalSeconds;
        private float timeSinceLastFrameChange;

        public AnimatedSprite(string texturePath, int frameWidth, int frameHeight, float frameChangeIntervalSeconds)
            : base(texturePath)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameChangeIntervalSeconds = frameChangeIntervalSeconds;
        }

        public int TextureTopOffset { get; set; }

        public override void Load(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            base.Load(contentManager);
            this.totalFrames = this.Texture.Width / frameWidth;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastFrameChange += elapsed;
            if (timeSinceLastFrameChange > frameChangeIntervalSeconds)
            {
                currentFrame++;
                currentFrame = currentFrame % totalFrames;
                timeSinceLastFrameChange %= frameChangeIntervalSeconds;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            //base.Draw(sb);
            RenderAtPosition(sb, this.Position);
        }

        protected void RenderAtPosition(SpriteBatch sb, Vector2 position)
        {
            var sourceRect = new Rectangle(currentFrame * frameWidth, this.TextureTopOffset, this.frameWidth, this.frameHeight);
            sb.Draw(this.Texture, position, sourceRect, Color.White, this.Rotation, this.Origin, 1, SpriteEffects.None, 0);
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle((int)this.RenderPosition.X, (int)this.RenderPosition.Y, this.frameWidth, this.frameHeight);
        }

        public override Vector2 CalculateNextRenderPosition(GameTime gameTime)
        {
            return this.Position;
        }

        public override Rectangle CalculateNextBounds(GameTime gameTime)
        {
            var nextPos = this.CalculateNextRenderPosition(gameTime);
            return new Rectangle((int)nextPos.X, (int)nextPos.Y, this.frameWidth, this.frameHeight);
        }
    }
}
