﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozyGrove.Sprites
{
    public class BarrierSprite : Sprite
    {
        public BarrierSprite(Rectangle textureRectangle) : base(Game1.textures["barriers"], textureRectangle)
        {

        }
    }
}