using System;

namespace Enemies.Overworld.Types
{
    public class StompableOverworldEnemy: AbstractOverworldEnemy
    {
        private void Reset()
        {
            _canStomp = true;
            _canHammer = true;
        }
    }
}