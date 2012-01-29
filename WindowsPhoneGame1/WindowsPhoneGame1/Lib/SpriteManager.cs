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
        public Game1 Game1 { get { return this.Game as Game1; } }

        public bool IsActive { get; set; }

        public SpriteManager(Game1 game)
            : base(game)
        {
            this.sprites = new List<Sprite>();
            this.IsActive = false;
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

        public void Load()
        {
            this.LoadContent();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.sprites.ForEach(o => o.Load(this.Game.Content));
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.IsActive) return;
            base.Update(gameTime);

            this.sprites.ForEach(o => o.Update(gameTime));
        }

        public override void Draw(GameTime gameTime)
        {
            if (!this.IsActive) return;

            base.Draw(gameTime);
            this.spriteBatch.Begin();

            this.sprites.ForEach(o => o.Draw(this.spriteBatch));

            this.spriteBatch.End();
        }
    }
}
