using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Jigsaw
{
    class Piece: GameObject
    {
        private MouseState mState;
        public bool isSelected = false;

        public Piece(GraphicsDevice graphicsDevice, int newPieceNumber, Vector2 newPosition, int newSize)
        {
            tag = Globals.PIECE_TAG;
            pieceNumber = newPieceNumber;

            sideSize = newSize;
            size = new Vector2(newSize, newSize);
            position = newPosition;
            centeredPosition = new Vector2(newPosition.X + size.X / 2, newPosition.Y + size.Y / 2);
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.White });
            rect = new Rectangle((int)position.X, (int)position.Y, (int)newSize, (int)newSize);
        }
        public override void Update(GameTime gameTime)
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
                    if (mouseTargetDist < sideSize)
                    {
                        isSelected = true;
                    }
                    else isSelected = false;
                }
            }
            if (mState.LeftButton == ButtonState.Released) isSelected = false;

            if (isSelected)
            {
                setPosition(mState.Position.ToVector2());
            }
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
           spriteBatch.Draw(texture, rect, isSelected ? Color.Gray : Color.White);
        }

        private void setPosition(Vector2 newPosition)
        {
            rect.X = (int)(newPosition.X - size.X / 2);
            rect.Y = (int)(newPosition.Y - size.Y / 2);
            position.X = (int)rect.X;
            position.Y = (int)rect.Y;
            centeredPosition.X = position.X + size.X / 2;
            centeredPosition.Y = position.Y + size.Y / 2;
        }

        public override void HandleCollision(GameObject gameObject)
        {
            if (gameObject.tag == "Slot" && !isSelected)
            {
                rect.X = gameObject.rect.X;
                rect.Y = gameObject.rect.Y;
                position.X = (int)rect.X;
                position.Y = (int)rect.Y;
                centeredPosition.X = position.X + size.X / 2;
                centeredPosition.Y = position.Y + size.Y / 2;
            }
        }
    }
}
