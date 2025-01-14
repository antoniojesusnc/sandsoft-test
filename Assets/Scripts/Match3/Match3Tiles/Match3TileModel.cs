using UnityEngine;

namespace Sandsoft.Match3
{
    public class Match3TileModel
    {
        public Match3TileConfig Config { get; private set; }
        public Match3TilesColors TileColor => Config.TileColor;
        public Sprite Image => Config.Image;
        public Vector2Int Position { get; set; }

        public Match3TileModel(Match3TileConfig config)
        {
            SetConfig(config);
        }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
        }
        
        public void SetConfig(Match3TileConfig config)
        {
            Config = config;
        }
    }
}