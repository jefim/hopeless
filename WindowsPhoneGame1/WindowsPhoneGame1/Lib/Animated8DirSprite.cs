using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsPhoneGame1.Lib
{
    public class Animated8DirSprite : Sprite
    {
        private int frameWidth;
        private int frameHeight;
        private float frameChangeIntervalSeconds;
        private List<AnimatedSprite> directions;

        public Animated8DirSprite(string texturePath, int frameWidth, int frameHeight, float frameChangeIntervalSeconds)
            : base(texturePath)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameChangeIntervalSeconds = frameChangeIntervalSeconds;
            this.directions = new List<AnimatedSprite>();

            this.FaceDirection = 4;
        }

        public int FaceDirection { get; set; }

        public override void Load(ContentManager contentManager)
        {
            for (int i = 0; i < 8; i++)
            {
                var sprite = new AnimatedSprite(this.TexturePath, this.frameWidth, this.frameHeight, this.frameChangeIntervalSeconds) { TextureTopOffset = i * frameHeight };
                sprite.Position = this.Position;
                this.directions.Add(sprite);

            }

            this.directions.ForEach(o => o.Load(contentManager));
            
            base.Load(contentManager);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var touches = TouchPanel.GetState();
            foreach (var touch in touches)
            {
                if (touch.State == TouchLocationState.Released)
                {
                    this.OnTouch(touch);
                }
            }

            this.directions[this.FaceDirection].Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            //base.Draw(sb);

            this.directions[this.FaceDirection].Draw(sb);
        }

        protected override void OnTouch(TouchLocation touch)
        {
            base.OnTouch(touch);

            if (touch.State == TouchLocationState.Moved)
            {
                var direction = touch.Position - this.Position;
                if (direction.X > 0)
                {
                    if (direction.Y > 0) this.FaceDirection = 7;
                    else this.FaceDirection = 0;
                }
                else
                {
                    if (direction.Y > 0) this.FaceDirection = 3;
                    else this.FaceDirection = 5;
                }
            }
        }
    }
}
