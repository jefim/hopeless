using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsPhoneGame1.Lib.Screens
{
    public class GameScreen : SpriteManager
    {
        private Sprite tank;
        private Tilemap tilemap;

        public GameScreen(Game1 game)
            : base(game)
        {
            this.tilemap = new Tilemap("tilemap2");
            this.Add(tilemap);

            var offset = new Vector2(7, 155);

            var buttonUp = new Button("joystick_button_v") { Position = offset + new Vector2(115, 0) };
            buttonUp.Touch += new EventHandler<TouchEventArgs>(buttonUp_Touch);
            this.Add(buttonUp);

            var buttonDown = new Button("joystick_button_v") { Position = offset + new Vector2(115, 210) };
            buttonDown.Touch += new EventHandler<TouchEventArgs>(buttonDown_Touch);
            this.Add(buttonDown);

            var buttonLeft = new Button("joystick_button_h") { Position = offset + new Vector2(0, 115) };
            buttonLeft.Touch += new EventHandler<TouchEventArgs>(buttonLeft_Touch);
            this.Add(buttonLeft);

            var buttonRight = new Button("joystick_button_h") { Position = offset + new Vector2(210, 115) };
            buttonRight.Touch += new EventHandler<TouchEventArgs>(buttonRight_Touch);
            this.Add(buttonRight);
            
            this.tank = new Animated8DirSprite("horse", 73, 77, 0.05f);
            tank.Position = new Vector2(400 - 35, 240 - 35);
            this.Add(tank);
        }

        Vector2 sceneOffset = new Vector2(0, 0);

        private void StopMoving()
        {
            this.tilemap.Direction = Vector2.Zero;
        }

        void buttonRight_Touch(object sender, TouchEventArgs e)
        {
            if (e.Touch.State == TouchLocationState.Released)
            {
                this.StopMoving();
                return;
            }

            if (e.Touch.State == TouchLocationState.Moved)
            {
                this.tilemap.Direction = new Vector2(1, 0);
            }
        }

        void buttonLeft_Touch(object sender, TouchEventArgs e)
        {
            if (e.Touch.State == TouchLocationState.Released)
            {
                this.StopMoving();
                return;
            }

            if (e.Touch.State == TouchLocationState.Moved)
            {
                this.tilemap.Direction = new Vector2(-1, 0);
            }
        }

        void buttonDown_Touch(object sender, TouchEventArgs e)
        {
            if (e.Touch.State == TouchLocationState.Released)
            {
                this.StopMoving();
                return;
            }

            if (e.Touch.State == TouchLocationState.Moved)
            {
                this.tilemap.Direction = new Vector2(0, 1);
            }
        }

        void buttonUp_Touch(object sender, TouchEventArgs e)
        {
            if (e.Touch.State == TouchLocationState.Released)
            {
                this.StopMoving();
                return;
            }

            if (e.Touch.State == TouchLocationState.Moved)
            {
                this.tilemap.Direction = new Vector2(0, -1);
            }
        }
    }
}
