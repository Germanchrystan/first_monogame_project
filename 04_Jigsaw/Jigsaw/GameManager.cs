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

        public static void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("font");
        }

        public static void Init(GraphicsDevice graphicsDevice)
        {
            gameObjectPool.Add(new Slot(graphicsDevice, 1, new Vector2(100, 100), 40));
            gameObjectPool.Add(new Piece(graphicsDevice, 1, new Vector2(40, 40), 40));
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
