using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Spaceship
{
    class Ship
    {
        public int radius = 30;
        private const int speed = 180;
        static public Vector2 defaultPosition = new Vector2(640, 360);
        public Vector2 position = defaultPosition;
        public void shipUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Right) && position.X < 1280) position.X += speed * dt;
            if (kState.IsKeyDown(Keys.Left) && position.X > 0) position.X -= speed * dt;
            if (kState.IsKeyDown(Keys.Down) && position.Y < 720) position.Y += speed * dt;
            if (kState.IsKeyDown(Keys.Up) && position.Y > 0) position.Y -= speed * dt;
        }
    }
}
