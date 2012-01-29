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
        private Tank tank;
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
            this.tank = new Tank("body", "cannon", 84, 68, 0.10f);
	    this.tank.Origin = new Vector2(84 / 2, 68 / 2);
	this.tank.BodyRotation = (float)Math.PI / 2;

            tank.Position = new Vector2(400 - 35, 240 - 35);

            this.Add(tank.Body);
            this.Add(tank.Cannon); 

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
            this.tank.BodyDirection = Vector2.Zero;
        }

        void buttonRight_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(-1, 0);
            this.tank.BodyDirection = new Vector2(-1, 0);
        }

        void buttonLeft_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(1, 0);
            this.tank.BodyDirection = new Vector2(1, 0);
        }

        void buttonDown_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(0, -1);
            this.tank.BodyDirection = new Vector2(0, -1);
        }

        void buttonUp_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(0, 1);
            this.tank.BodyDirection = new Vector2(0, 1);
        }

        public override void Update(GameTime gameTime)
        {
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var enemy in enemies)
            {
                var predictEnemyRect = enemy.CalculateNextBounds(gameTime);
                bool intersects = false;
                var rect = this.tank.Body.CalculateNextBounds(gameTime);
                rect.Inflate(5, 5);
                predictEnemyRect.Intersects(ref rect, out intersects);
                if(intersects)
                {
                    this.StopMoving();
                }
            }

            this.SceneOffset += 200 * elapsed * this.direction;

            base.Update(gameTime);
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
