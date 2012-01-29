using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame1.Lib.Screens
{
    public class GameScreen : SpriteManager
    {
        private Tank tank;
        private Tilemap tilemap;
        public List<Target> Enemies = new List<Target>();
        private Vector2 direction;
        private Button buttonShooting;
        private Powerbar powerbar;
        private SpriteFont font;
        
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

            var offset0 = new Vector2(1000, 1000);
            var enemy = new Target("snake", 84, 84, 0.1f, this) { Position = new Vector2(0, 0) + offset0 };
            this.Enemies.Add(enemy);
            this.Add(enemy);

            enemy = new Target("snake", 84, 84, 0.1f, this) { Position = new Vector2(65, 189) + offset0 };
            this.Enemies.Add(enemy);
            this.Add(enemy);

            enemy = new Target("snake", 84, 84, 0.1f, this) { Position = new Vector2(560, 256) + offset0 };
            this.Enemies.Add(enemy);
            this.Add(enemy);

            enemy = new Target("snake", 84, 84, 0.1f, this) { Position = new Vector2(300, 300) + offset0 };
            this.Enemies.Add(enemy);
            this.Add(enemy);

            // UI ELEMENTS
            var offset = new Vector2(7, 155);

            var buttonUp = new Button("joystick_button_v") { Position = offset + new Vector2(115, 0) };
            buttonUp.Pressed += new EventHandler<TouchEventArgs>(buttonUp_Touch);
            buttonUp.Released += new EventHandler<TouchEventArgs>(buttonNavigation_Released);
            this.Add(buttonUp);

            var buttonDown = new Button("joystick_button_v2") { Position = offset + new Vector2(115, 210) };
            buttonDown.Pressed += new EventHandler<TouchEventArgs>(buttonDown_Touch);
            buttonDown.Released += new EventHandler<TouchEventArgs>(buttonNavigation_Released);
            this.Add(buttonDown);

            var buttonLeft = new Button("joystick_button_h") { Position = offset + new Vector2(0, 115) };
            buttonLeft.Pressed += new EventHandler<TouchEventArgs>(buttonLeft_Touch);
            buttonLeft.Released += new EventHandler<TouchEventArgs>(buttonNavigation_Released);
            this.Add(buttonLeft);

            var buttonRight = new Button("joystick_button_h2") { Position = offset + new Vector2(210, 115) };
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

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(this.Game1.MySong);

            this.SceneOffset = new Vector2(-1000, -1000);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.font = this.Game.Content.Load<SpriteFont>("SegoeUI");
        }

        bool TouchHitsAnyButtons(TouchLocation touch)
        {
            // check if are not hitting any other buttons
            foreach (var button in this.sprites.OfType<Button>().Where(o => o != this.buttonShooting))
            {
                if (button.GetBounds().Contains(new Point((int)touch.Position.X, (int)touch.Position.Y)))
                {
                    return true;
                }
            }
            //this.Game1.BangSfx().Play();
            return false;
        }

        void buttonShooting_Released(object sender, TouchEventArgs e)
        {
            var power = this.powerbar.PercentFull;
            this.powerbar.IsAnimating = false;
            this.powerbar.PercentFull = 0;
            this.powerbar.PercentDirection = 1;

            if (TouchHitsAnyButtons(e.Touch)) return;

            var direction = e.Touch.Position - tank.Body.RenderPosition;
            direction.Normalize();
            this.tank.TurretDirection = direction;
            this.Add(new Bullet(this)
            {
                AltitudeSpeed = power + 10,
                Position = new Vector2(400, 240) + direction * 35 - this.SceneOffset,
                Origin = new Vector2(16 / 2, 16 / 2),
                Direction = direction,
                Speed = 300
            });
            this.Game1.BangSfx.Play();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!this.IsActive) return;

            base.Draw(gameTime);
            this.spriteBatch.Begin();

            this.sprites.ForEach(o => o.Draw(this.spriteBatch));

            spriteBatch.DrawString(this.font, "Snakes remaining: " + this.Enemies.Count(o => o.IsVisible), new Vector2(10, 10), Color.Black);

            this.spriteBatch.End();
        }

        void buttonShooting_Pressed(object sender, TouchEventArgs e)
        {
            if (TouchHitsAnyButtons(e.Touch)) return;
            this.powerbar.IsAnimating = true;
        }

        void buttonNavigation_Released(object sender, TouchEventArgs e)
        {
            this.StopMoving();
            this.Game1.TankSfx.Stop(true);
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
            this.Game1.TankSfx.Play();
        }

        void buttonLeft_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(1, 0);
            this.tank.BodyDirection = new Vector2(1, 0);
            this.tank.Body.Paused = false;
            this.Game1.TankSfx.Play();
        }

        void buttonDown_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(0, -1);
            this.tank.BodyDirection = new Vector2(0, -1);
            this.tank.Body.Paused = false;
            this.Game1.TankSfx.Play();
        }

        void buttonUp_Touch(object sender, TouchEventArgs e)
        {
            this.direction = new Vector2(0, 1);
            this.tank.BodyDirection = new Vector2(0, 1);
            this.tank.Body.Paused = false;
            this.Game1.TankSfx.Play();
        }

        float? timer = null;

        public override void Update(GameTime gameTime)
        {
            if (!this.IsActive) return;

            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.Enemies.Where(o => o.IsVisible).Count() == 0 || timer != null)
            {
                if (timer == null) timer = 0;
                timer += elapsed;
                if (timer > 1)
                {
                    MediaPlayer.Stop();
                    this.IsActive = false;
                    this.Game1.StartScreen.IsActive = true;
                    this.Game1.StartScreen.StartButton.IsEnabled = true;
                }
            }


            var rect = this.tank.Body.CalculateNextBounds(gameTime);
            rect.Inflate(3, 3);
            foreach (var enemy in Enemies.Where(o => o.IsVisible))
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