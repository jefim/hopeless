using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsPhoneGame1.Lib
{
    public  class TouchEventArgs : EventArgs
    {
        public TouchEventArgs(TouchLocation touch)
        {
            this.Touch = touch;
            this.Handled = false;
        }

        public TouchLocation Touch { get; set; }
        public bool Handled { get; set; }
    }
}
