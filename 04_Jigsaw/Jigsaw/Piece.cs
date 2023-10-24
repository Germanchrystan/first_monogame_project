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

        public Piece(int newPieceNumber, Vector2 newPosition, int newSize, Rectangle newRect)
        {
            tag = Globals.PIECE_TAG;
            pieceNumber = newPieceNumber;
            // Transform
            sideSize = newSize;
            size = new Vector2(newSize, newSize);
            position = newPosition;
            centeredPosition = new Vector2(newPosition.X + size.X / 2, newPosition.Y + size.Y / 2);
            // Quad
            texture = GameManager.jigsawImage;
            rect = newRect;
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
                Vector2 mStateCenteredPosition = new Vector2(mState.Position.ToVector2().X - sideSize / 2, mState.Position.ToVector2().Y - sideSize / 2);
                setPosition(mStateCenteredPosition);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
           spriteBatch.Draw(texture, position, rect, isSelected ? Color.Gray : Color.White);
        }

        private void setPosition(Vector2 newPosition)
        {
            //rect.X = (int)(newPosition.X);
            //rect.Y = (int)(newPosition.Y);
            position.X = (int)newPosition.X;
            position.Y = (int)newPosition.Y;
            centeredPosition.X = position.X + size.X / 2;
            centeredPosition.Y = position.Y + size.Y / 2;
        }

        public override void HandleCollision(GameObject gameObject)
        {
            if (gameObject.tag == "Slot" && !isSelected) setPosition(new Vector2(gameObject.rect.X, gameObject.rect.Y));
        }
    }
}
