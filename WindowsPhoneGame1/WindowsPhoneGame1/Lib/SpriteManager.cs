using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame1.Lib
{
    public class SpriteManager : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        protected List<Sprite> sprites;

        public SpriteManager(Game1 game)
            : base(game)
        {
            this.sprites = new List<Sprite>();
        }

        public void Add(Sprite sprite)
        {
            this.sprites.Add(sprite);
        }

        public void Remove(Sprite sprite)
        {
            this.sprites.Remove(sprite);
        }

        public override void Initialize()
        {
            base.Initialize();

            this.spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.sprites.ForEach(o => o.Load(this.Game.Content));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.sprites.ForEach(o => o.Update(gameTime));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            this.spriteBatch.Begin();

            this.sprites.ForEach(o => o.Draw(this.spriteBatch));

            this.spriteBatch.End();
        }
    }
}
