using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsPhoneGame1.Lib.Screens;

namespace WindowsPhoneGame1.Lib
{
    public class Target : GameScreenSprite
    {
        public Target(string texturePath, int frameWidth, int frameHeight, int frameChangeInterval, GameScreen gameScreen)
            : base(texturePath, frameWidth, frameHeight, frameChangeInterval, gameScreen)
        {

        }
    }
}
