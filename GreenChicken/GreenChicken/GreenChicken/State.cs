﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GreenChicken
{
    public abstract class State
    {



        public abstract void Update(GameTime gt);

        public abstract void Draw(GameTime gt);
    }
}
