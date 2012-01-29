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
        private List<Target> enemies = new List<Target>();
        private Vector2 direction;

        public GameScreen(Game1 game)
            : base(game)
        {
            this.tilemap = new Tilemap("tilemap2", this);
            this.Add(tilemap);

            var offset = new Vector2(7, 155);

            var buttonUp = new Button("joystick_button_v") { Position = offset + new Vector2(115, 0) };
            buttonUp.Pressed += new EventHandler<TouchEventArgs>(buttonUp_Touch);
            buttonUp.Released += new EventHandler<TouchEventArgs>(buttonNavigation_Released);
            this.Add(buttonUp);

            var buttonDown = new Button("joystick_button_v") { Position = offset + new Vector2(115, 210) };
            buttonDown.Pressed += new EventHandler<TouchEventArgs>(buttonDown_Touch);
            buttonDown.Released += new EventHandler<TouchEventArgs>(buttonNavigation_Released);
            this.Add(buttonDown);

            var buttonLeft = new Button("joystick_button_h") { Position = offset + new Vector2(0, 115) };
            buttonLeft.Pressed += new EventHandler<TouchEventArgs>(buttonLeft_Touch);
            buttonLeft.Released += new EventHandler<TouchEventArgs>(buttonNavigation_Released);
            this.Add(buttonLeft);

            var buttonRight = new Button("joystick_button_h") { Position = offset + new Vector2(210, 115) };
            buttonRight.Pressed += new EventHandler<TouchEventArgs>(buttonRight_Touch);
            buttonRight.Released += new EventHandler<TouchEventArgs>(buttonNavigation_Released);
            this.Add(buttonRight);

            this.tank = new AnimatedSprite("horse", 73, 77, 0.05f) { Origin = new Vector2(73 / 2, 77 / 2), Rotation = (float)Math.PI / 2 };
            tank.Position = new Vector2(400 - 35, 240 - 35);
            this.Add(tank);

            var enemy = new Target("enemy1", 64, 64, 1, this);
            this.enemies.Add(enemy);
            this.Add(enemy);

            enemy = new Target("enemy1", 64, 64, 1, this) { Position = new Vector2(65, 189) };
            this.enemies.Add(enemy);
            this.Add(enemy);

            enemy = new Target("enemy1", 64, 64, 1, this) { Position = new Vector2(560, 256) };
            this.enemies.Add(enemy);
            this.Add(enemy);

            enemy = new Target("enemy1", 64, 64, 1, this) { Position = new Vector2(300, 300) };
            this.enemies.Add(enemy);
            this.Add(enemy);
        }

        void buttonNavigation_Released(object sender, TouchEventArgs e)
        {
            this.StopMoving();
        }

        public Vector2 SceneOffset { get; set; }

        private void StopMoving()
        {
            this.tilemap.Direction = Vector2.Zero;
            this.direction = Vector2.Zero;
        }

        void buttonRight_Touch(object sender, TouchEventArgs e)
        {
            this.tilemap.Direction = new Vector2(-1, 0);
            this.direction = new Vector2(-1, 0);
            this.tank.Direction = new Vector2(-1, 0);
        }

        void buttonLeft_Touch(object sender, TouchEventArgs e)
        {
            this.tilemap.Direction = new Vector2(1, 0);
            this.direction = new Vector2(1, 0);
            this.tank.Direction = new Vector2(1, 0);
        }

        void buttonDown_Touch(object sender, TouchEventArgs e)
        {
            this.tilemap.Direction = new Vector2(0, -1);
            this.direction = new Vector2(0, -1);
            this.tank.Direction = new Vector2(0, -1);
        }

        void buttonUp_Touch(object sender, TouchEventArgs e)
        {
            this.tilemap.Direction = new Vector2(0, 1);
            this.direction = new Vector2(0, 1);
            this.tank.Direction = new Vector2(0, 1);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.SceneOffset += 200 * elapsed * this.direction;
        }

        /// <summary>
        /// Sets movement direction, but checks that the sprite is rotated properly first
        /// </summary>
        /// <param name="direction"></param>
        public void SetDirection(Vector2 direction)
        {
            //var angle = VectorToAngle(direction);
            //if (angle == tank.Rotation)
            //{
            //    this.tilemap.Direction = direction;
            //}
            //else
            //{
            //}
        }
    }
}
