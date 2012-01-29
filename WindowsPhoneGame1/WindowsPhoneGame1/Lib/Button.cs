using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame1.Lib
{
    public class Button : Sprite
    {
        public Button(string texturePath)
            : base(texturePath)
        {
        }

        protected override void OnTouch(TouchLocation touch)
        {
            base.OnTouch(touch);

            var rect = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
            if (rect.Contains(new Point((int)touch.Position.X, (int)touch.Position.Y)))
            {
                var evt = this.Touch;
                if (evt != null) evt(this, new TouchEventArgs(touch));
            }
        }

        public event EventHandler<TouchEventArgs> Touch;
    }
}
