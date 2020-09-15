﻿using DozyGrove.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DozyGrove.Models
{
    public class Tile
    {
        protected Sprite sprite;
        public Sprite entity;
        protected Vector2 position;
        protected bool hasSprite { get { return sprite != null; } }
        protected bool hasEntity { get { return entity != null; } }
        public static int tileSize = 10;

        public Tile(Vector2 position)
        {
            this.position = position;
        }

        public void SetSprite(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public void Update(GameTime gameTime)
        {
            if (hasSprite)
                sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (hasSprite)
                sprite.Draw(spriteBatch, position);
            if (hasEntity)
                entity.Draw(spriteBatch, position);
        }
    }
}