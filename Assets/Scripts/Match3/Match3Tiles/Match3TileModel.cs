using UnityEngine;

namespace Sandsoft.Match3
{
    public class Match3TileModel : MonoBehaviour
    {
        public Match3TileConfig Config { get; private set; }
        public Sprite Image => Config.Image;

        public Match3TileModel(Match3TileConfig config)
        {
            SetConfig(config);
        }
        
        public void SetConfig(Match3TileConfig config)
        {
            Config = config;
        }
    }
}