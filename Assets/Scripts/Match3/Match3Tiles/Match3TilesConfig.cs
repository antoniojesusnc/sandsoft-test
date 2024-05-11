using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sandsoft.Match3
{
    [CreateAssetMenu(fileName = "Match3TilesConfig", menuName = "Sandsoft/Match3/Match3TilesConfig", order = 1)]
    public class Match3TilesConfig : ScriptableObject
    {
        [field: SerializeField]
        public Vector2 TileSize { get; private set; }
        
        [field: SerializeField]
        public Match3TileView Match3TileViewPrefab { get; private set; }
        
        [field: SerializeField]
        public List<Match3TileConfig> Match3Tiles { get; private set; }
    }
    
    [Serializable]
    public class Match3TileConfig
    {
        [field: SerializeField]
        public Match3TilesColors TilesColor { get; private set; }
        [field: SerializeField]
        public Sprite Image { get; private set; }
    }
}
