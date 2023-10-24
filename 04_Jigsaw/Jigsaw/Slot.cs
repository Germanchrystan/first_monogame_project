using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Jigsaw
{
    class Slot: GameObject
    {
        public bool correctedPiece = false;
        public Slot(GraphicsDevice graphicsDevice, int newPieceNumber, Vector2 newPosition, int newSize)
        {
            tag = Globals.SLOT_TAG;
            pieceNumber = newPieceNumber;
            
            sideSize = newSize;
            size = new Vector2(newSize, newSize);
            position = newPosition;
            centeredPosition = new Vector2(newPosition.X + newSize / 2, newPosition.Y + newSize / 2);

            texture = GameManager.slotImage;
            //texture.SetData<Color>(new Color[] { Color.White });
            rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }

        public override void HandleCollision(GameObject gameObject)
        {
            if (gameObject.tag == Globals.PIECE_TAG)
            {
                Piece piece = (Piece)gameObject;
                if (piece.pieceNumber == pieceNumber && !piece.isSelected) correctedPiece = true;
                else correctedPiece = false;
            }
        }
    }
}
