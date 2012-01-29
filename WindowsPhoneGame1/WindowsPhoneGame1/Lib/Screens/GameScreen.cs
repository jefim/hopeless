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
        private Button buttonShooting;
        private Powerbar powerbar;

        public GameScreen(Game1 game)
            : base(game)
        {
            // SPRITES
            this.tilemap = new Tilemap("tilemap2", this);
            this.Add(tilemap);

            this.tank = new Tank("body", "cannon", 84, 68, 0.10f);
            this.tank.Origin = new Vector2(84 / 2, 68 / 2);
            this.tank.BodyRotation = (float)Math.PI / 2;

            tank.Position = new Vector2(400, 240);

            this.Add(tank.Body);
            this.Add(tank.Cannon);

            var enemy = new Target("snake", 84, 84, 0.1f , this);
            this.enemies.Add(enemy);
            this.Add(enemy);

            enemy = new Target("snake", 84, 84, 0.1f, this) { Position = new Vector2(65, 189) };
            this.enemies.Add(enemy);
            this.Add(enemy);

            enemy = new Target("snake", 84, 84, 0.1f, this) { Position = new Vector2(560, 256) };
            this.enemies.Add(enemy);
            this.Add(enemy);

            enemy = new Target("snake", 84, 84, 0.1f, this) { Position = new Vector2(300, 300) };
            this.enemies.Add(enemy);
            this.Add(enemy);


            // UI ELEMENTS
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

            // DO NOT CHANGE THE ORDERING OF BUTTONS - IT IS EXTREMELY IMPORTANT THAT THE SHOOTING IS
            // ADDED AFTER THE ARROW BUTTONS
            var buttonShooting = new Button("transparent");
            buttonShooting.Pressed += new EventHandler<TouchEventArgs>(buttonShooting_Pressed);
            buttonShooting.Released += new EventHandler<TouchEventArgs>(buttonShooting_Released);
            this.buttonShooting = buttonShooting;
            this.Add(buttonShooting);


            var powerbar = new Powerbar() { Position = new Vector2(800 - 150, 480 - 70) };
            this.powerbar = powerbar;
            this.Add(powerbar);
        }

        void buttonShooting_Released(object sender, TouchEventArgs e)
        {
            var power = this.powerbar.PercentFull;
            this.powerbar.IsAnimating = false;
            this.powerbar.PercentFull = 0;

            // check if are not hitting any other buttons
            foreach (var button in this.sprites.OfType<Button>().Where(o => o != this.buttonShooting))
            {
                if (button.GetBounds().Contains(new Point((int)e.Touch.Position.X, (int)e.Touch.Position.Y)))
                {
                    return;
                }
            }
            
            var direction = e.Touch.Position - tank.Body.RenderPosition;
            direction.Normalize();
            this.tank.TurretDirection = direction;
            this.Add(new Bullet(this)
            {
                Power = power,
                Position = new Vector2(400, 240) + direction * 35,
                Origin = new Vector2(16 / 2, 16 / 2),
                Direction = direction,
                Speed = 300
            });
        }

        void buttonShooting_Pressed(object sender, TouchEventArgs e)
        {
            this.powerbar.IsAnimating = true;
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
            this.tank.Body.Paused = true;
        }

        void buttonRight_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(-1, 0);
            this.tank.BodyDirection = new Vector2(-1, 0);
            this.tank.Body.Paused = false;
        }

        void buttonLeft_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(1, 0);
            this.tank.BodyDirection = new Vector2(1, 0);
            this.tank.Body.Paused = false;
        }

        void buttonDown_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(0, -1);
            this.tank.BodyDirection = new Vector2(0, -1);
            this.tank.Body.Paused = false;
        }

        void buttonUp_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(0, 1);
            this.tank.BodyDirection = new Vector2(0, 1);
            this.tank.Body.Paused = false;
        }

        public override void Update(GameTime gameTime)
        {
            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var rect = this.tank.Body.CalculateNextBounds(gameTime);
            rect.Inflate(3, 3);
            foreach (var enemy in enemies)
            {
                var predictEnemyRect = enemy.CalculateNextBounds(gameTime);
                bool intersects = false;
                predictEnemyRect.Intersects(ref rect, out intersects);
                if (intersects)
                {
                    this.SceneOffset += 200 * elapsed * -1 * this.direction;
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
