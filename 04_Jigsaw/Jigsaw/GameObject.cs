using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Jigsaw
{
    abstract class GameObject
    {
        public string tag;
        // Methods
        abstract public void Update(GameTime gameTime);
        abstract public void Draw(SpriteBatch spriteBatch);
        abstract public void HandleCollision(GameObject gameObject);
        // Transform
        public Vector2 position;
        public Vector2 centeredPosition;
        public Vector2 size;
        public int sideSize;
        // Sprite
        public Texture2D texture;
        public Rectangle rect;

        public int pieceNumber;
    }
}
