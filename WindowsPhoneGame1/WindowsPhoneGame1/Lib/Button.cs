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

        private int touchThatStartedPressedState = -1;

        public bool IsPressed { get { return this.touchThatStartedPressedState != -1; } }

        protected override void OnTouch(TouchLocation touch)
        {
            base.OnTouch(touch);

            if (touch.Id == touchThatStartedPressedState && touch.State == TouchLocationState.Released)
            {
                touchThatStartedPressedState = -1;
                var evt = this.Released;
                if (evt != null) evt(this, new TouchEventArgs(touch));
                touchThatStartedPressedState = -1;
            }

            var rect = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
            if (rect.Contains(new Point((int)touch.Position.X, (int)touch.Position.Y)))
            {
                if (touch.State == TouchLocationState.Pressed)
                {
                    var evt = this.Pressed;
                    if (evt != null) evt(this, new TouchEventArgs(touch));
                    touchThatStartedPressedState = touch.Id;
                }
            }
        }

        public event EventHandler<TouchEventArgs> Pressed;
        public event EventHandler<TouchEventArgs> Released;
    }
}
