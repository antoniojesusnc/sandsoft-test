using System.Collections.Generic;

namespace Sandsoft.Match3
{
    public class LevelModel
    {
        public List<Match3TileModel> Tiles { get; private set; } = new List<Match3TileModel>();

        public void AddTile(Match3TileModel tileModel)
        {
            Tiles.Add(tileModel);
        }

        public void CleanLevel()
        {
            Tiles.Clear();
        }
    }
}