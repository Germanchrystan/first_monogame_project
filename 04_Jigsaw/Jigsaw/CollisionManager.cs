using System;

namespace Jigsaw
{
    class CollisionManager
    {
        public static void HandleCollision(GameObject gameObject1, GameObject gameObject2)
        {
            gameObject1.HandleCollision(gameObject2);
            gameObject2.HandleCollision(gameObject1);
        }
    }
}
