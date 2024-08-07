using UnityEngine;

namespace Objects.Blocks.Helpers
{
    public static class BlockHelper
    {
        public static string GetPlayerTagFromBlockHit(RaycastHit hit)
        {
            return hit.transform.gameObject.tag;
        }
    }
}