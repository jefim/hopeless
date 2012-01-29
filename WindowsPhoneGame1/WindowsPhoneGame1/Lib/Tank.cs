using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame1.Lib
{
    class Tank
    {
        public Tank(string bodyTexturePath, string turretTextureSprite, int frameWidth, int frameHeight, float frameChangeIntervalSeconds)
        {
            bodySprite = new AnimatedSprite(bodyTexturePath, frameWidth, frameHeight, frameChangeIntervalSeconds);
            turretSprite = new Sprite(turretTextureSprite);
        }

        public AnimatedSprite Body
        {
            get
            {
                return bodySprite;
            }
        }

        public Sprite Cannon
        {
            get
            {
                return turretSprite;
            }
        }

        public Vector2 Position { 
            get
            {
                return bodySprite.Position;
            }
            
            set
            {
                bodySprite.Position =  value;
                turretSprite.Position = value;
            }
        }

        public Vector2 BodyDirection { 
            get
            {
                return bodySprite.Direction;
            }
            
            set
            {
                bodySprite.Direction = value;
            }
        }

        public Vector2 TurretDirection {
            get
            {
                return turretSprite.Direction;
            }
            
            set
            {
                turretSprite.Direction = value;
            }
        }

        public float Speed { 
                   get
            {
                return bodySprite.Speed;
            }
            
            set
            {
                bodySprite.Speed =  value;
                turretSprite.Speed = value;
            }
        }

        public float BodyRotation {
            get
            {
                return bodySprite.Rotation;
            }
            
            set
            {
                bodySprite.Rotation = value;
            }
        }

        public float TurretRotation {
            get
            {
                return turretSprite.Rotation;
            }
            
            set
            {
                turretSprite.Rotation = value;
            }
        }

        public Vector2 Origin {
            get
            {
                return bodySprite.Origin;
            }
            
            set
            {
                bodySprite.Origin =  value;
                turretSprite.Origin = value;
            }
        }

        private AnimatedSprite bodySprite;
        private Sprite turretSprite;
    }

    


}
