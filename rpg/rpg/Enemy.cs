﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg
{
    internal class Enemy
    {
        public static List<Enemy> enemies = new List<Enemy>();
        private Vector2 position = new Vector2(0,0);
        private int speed = 150;
        public SpriteAnimation anim;

        public Enemy(Vector2 position, Texture2D spriteSheet)
        {
            this.position = position;
            anim = new SpriteAnimation(spriteSheet, 10, 6);
        }

        public Vector2 Position { get { return position; } }

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            anim.Position = new Vector2(position.X - 48, position.Y - 66);
            anim.Update(gameTime);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 moveDir = playerPos - position;
            moveDir.Normalize();
            position += moveDir * speed * dt;
        }
    }
}
