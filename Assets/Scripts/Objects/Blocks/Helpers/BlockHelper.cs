using UnityEngine;

namespace Objects.Blocks.Helpers
{
    public static class BlockHelper
    {
        public static bool CheckPlayerTagFromBlockHit(RaycastHit hit, string playerTag)
        {
            if (hit.transform.gameObject.CompareTag(playerTag))
            {
                return true;
            }
            return false;
        }
    }
}