using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsPhoneGame1.Lib
{
    public class Sprite
    {
        private string texturePath = string.Empty;
        private Texture2D texture = null;

        public string TexturePath { get { return texturePath; } }
        public Texture2D Texture { get { return texture; } }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }

        public Sprite(string texturePath)
        {
            this.texturePath = texturePath;
        }

        public virtual void Load(ContentManager contentManager)
        {
            this.texture = contentManager.Load<Texture2D>(this.texturePath);
        }

        public virtual void Update(GameTime gameTime)
        {
            float elapsedS = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.HandleInput();
            if (this.Speed > 0)
            {
                this.Position += this.Direction * this.Speed * elapsedS;
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.Position, null, Color.White, this.Rotation, this.Origin, 1, SpriteEffects.None, 0);
        }

        protected void HandleInput()
        {
            foreach (var touch in Game1.Touches)
            {
                this.OnTouch(touch);
            }
        }

        protected virtual void OnTouch(TouchLocation touch)
        {
        }
    }
}
