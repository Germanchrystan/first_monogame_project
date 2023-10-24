using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace Jigsaw
{
    class GameManager
    {
        public static List<GameObject> gameObjectPool = new List<GameObject>();
        public static bool isSolved = false;
        private static SpriteFont font;

        public static Texture2D jigsawImage;
        public static Texture2D slotImage;

        public static Rectangle[] Rectangles;

        static Random random = new Random();
        public static void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("font");
            jigsawImage = content.Load<Texture2D>("jigsawImage");
            slotImage = content.Load<Texture2D>("Slot");
        }

        public static void Init(GraphicsDevice graphicsDevice)
        {
            // Loading Slots
            for(int i = 0; i < Globals.PIECES_AMOUNT; i++)
            {
                float xPosition = (int)(i / 6)* Globals.PIECE_DIMENSION + 800;
                float yPosition = (int)Math.Truncate(i % 6f) * Globals.PIECE_DIMENSION + 100;
                Vector2 position = new Vector2(xPosition, yPosition);
                gameObjectPool.Add(new Slot(graphicsDevice, i + 1, position, 40));
            }
            // Rectangle array
            Rectangles = new Rectangle[Globals.PIECES_AMOUNT];
            for(int i = 0; i < Globals.PIECES_AMOUNT; i++)
            {
                int xPosition = (int)(i / 6) * Globals.PIECE_DIMENSION;
                int yPosition = (int)Math.Truncate(i % 6f) * Globals.PIECE_DIMENSION;
                Rectangles[i] = new Rectangle(
                   xPosition,
                   yPosition,
                   Globals.PIECE_DIMENSION,
                   Globals.PIECE_DIMENSION
                );
            }
            // Loading Pieces
            for(int i = 0; i < Globals.PIECES_AMOUNT; i++)
            {
                int randomXPosition = random.Next(40, 501);
                int randomYPosition = random.Next(40, 300);
                gameObjectPool.Add(new Piece(i + 1, new Vector2(randomYPosition, randomYPosition), 40, Rectangles[i]));
            }
        }
        public static void Update(GameTime gameTime) 
        {
                UpdateAllObjects(gameTime);
                CheckCollisions();
                IsSolved();
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(GameObject gameObject in gameObjectPool)
            {
                gameObject.Draw(spriteBatch);
            }
            if (isSolved) spriteBatch.DrawString(font, "Resuelto!", new Vector2(10, 10), Color.White);
        }
        private static void UpdateAllObjects(GameTime gameTime)
        {
            foreach(GameObject gameObject in gameObjectPool)
            {
                gameObject.Update(gameTime);
            }
        }
        public static void CheckCollisions()
        {
            for(int i = 0; i < gameObjectPool.Count(); i++)
            {
                for(int j = i + 1; j < gameObjectPool.Count(); j++)
                {
                    if (j >= gameObjectPool.Count()) break;

                    float sum = (gameObjectPool[i].sideSize / 2) + (gameObjectPool[j].sideSize / 2);

                    if (Vector2.Distance(gameObjectPool[i].centeredPosition, gameObjectPool[j].centeredPosition) < sum)
                    {
                        CollisionManager.HandleCollision(gameObjectPool[i], gameObjectPool[j]);
                    }
                }
            }
        }
        public static void IsSolved()
        {
            bool solved = true;
            foreach(GameObject gameObject in gameObjectPool)
            {
                if (gameObject.tag == Globals.SLOT_TAG)
                {
                    Slot slot = (Slot)gameObject;
                    if (slot.correctedPiece) continue;
                    else solved = false;
                }
            }
            isSolved = solved;
        }
    }
}
