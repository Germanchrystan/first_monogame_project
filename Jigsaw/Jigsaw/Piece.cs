﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Jigsaw
{
    class Piece
    {
        private MouseState mState;
        public Texture2D texture;
        public Rectangle rect;
        public Vector2 size;
        public Vector2 position;
        public Vector2 positionWithOffset;
        public bool isSelected = false;

        public Piece(GraphicsDevice graphicsDevice, Vector2 newPosition, Vector2 newSize)
        {
            size = newSize;
            position = newPosition;
            positionWithOffset = new Vector2(newPosition.X + size.X / 2, newPosition.Y + size.Y / 2);
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.White });
            rect = new Rectangle((int)position.X, (int)position.Y, (int)newSize.X, (int)newSize.Y);
        }
        public void Update(GameTime gameTime)
        {
            mState = Mouse.GetState(); 
               
            if (mState.LeftButton == ButtonState.Pressed)
            {
                if(!isSelected)
                {
                    float mouseTargetDist = Vector2.Distance (
                        new Vector2(position.X + size.X / 2, position.Y + size.Y / 2),
                        mState.Position.ToVector2()
                    );
                    if (mouseTargetDist < size.X ) // TODO: rework this
                    {
                        isSelected = true;
                    }
                    else isSelected = false;
                }
            }
            if (mState.LeftButton == ButtonState.Released) isSelected = false;

            if (isSelected)
            {
                rect.X = (int)(mState.Position.ToVector2().X - size.X / 2);
                rect.Y = (int)(mState.Position.ToVector2().Y - size.Y / 2);
                position.X = (int)rect.X;
                position.Y = (int)rect.Y;
                positionWithOffset.X = position.X + size.X / 2;
                positionWithOffset.Y = position.Y + size.Y / 2;
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
           spriteBatch.Draw(texture, rect, isSelected ? Color.Gray : Color.White);
        }
    }
}
