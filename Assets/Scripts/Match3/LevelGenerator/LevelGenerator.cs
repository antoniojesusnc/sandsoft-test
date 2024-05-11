using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Sandsoft.Match3
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Match3TilesConfig _match3TilesConfig;
        
        [Header("Level Objects")]
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private Transform _tileContainer;

        private Dictionary<Vector2Int, Match3TileView> _tiles = new ();

        IEnumerator Start()
        {
            yield return 0;
            CalculateGridSize();
        }

        private void CalculateGridSize()
        {
            var size = GetComponent<RectTransform>().rect;
            var xSize =
                (size.width - _gridLayout.padding.left - _gridLayout.padding.right- _gridLayout.spacing.x*(_match3TilesConfig.TileSize.x-1))/_match3TilesConfig.TileSize.x;
            
            var ySize =
                (size.height - _gridLayout.padding.top - _gridLayout.padding.bottom- _gridLayout.spacing.y*(_match3TilesConfig.TileSize.y-1))/_match3TilesConfig.TileSize.y;

            var finalSize = Mathf.Min(xSize, ySize);
            _gridLayout.cellSize = new Vector2(finalSize, finalSize);
        }

        private void DestroyCurrentLevel()
        {
            foreach (var tile in _tiles)
            {
                GameObject.Destroy(tile.Value.gameObject);
            }
            _tiles.Clear();
        }


        public void GenerateLevel()
        {
            if (_tiles.Count > 0)
            {
                DestroyCurrentLevel();
            }
            
            GenerateLevelInternal();
        }

        private void GenerateLevelInternal()
        {
            _gridLayout.enabled = true;
            for (int y = 0; y < _match3TilesConfig.TileSize.y; y++)
            {
                for (int x = 0; x < _match3TilesConfig.TileSize.x; x++)
                {
                    var tile = GenerateTile();
                    var position = new Vector2Int(x, y);
                    tile.SetPosition(position);
                    tile.SetModel(GetModelForPosition(x, y));
                    _tiles.Add(new Vector2Int(x, y), tile);
                }
            }

            if (!IsSolvable())
            {
                GenerateLevel();
            }
        }

        private bool IsSolvable()
        {
            List<(Vector2Int, Vector2Int)> groups = FindGroups();
            
            int solvableGroups = 0;
            for (int i = 0; i < groups.Count; i++)
            {
                bool solvable = IsGroupSolvable(groups[i]);
                if (solvable)
                {
                    solvableGroups++;
                }
            }

            return solvableGroups >= _match3TilesConfig.MinMoveToCountAsSolvable;
        }

        private bool IsGroupSolvable((Vector2Int, Vector2Int) group)
        {
            bool isHorizontal = (group.Item2.x - group.Item1.x) > 0;
            if (isHorizontal)
            {
                var leftTile = new Vector2Int(group.Item1.x - 1, group.Item1.y);
                if (ThereAreAroundTwoOf(leftTile, _tiles[group.Item1].Model.TileColor))
                {
                    return true;
                }
                
                var rightTile = new Vector2Int(group.Item2.x + 1, group.Item1.y);
                if (ThereAreAroundTwoOf(rightTile, _tiles[group.Item1].Model.TileColor))
                {
                    return true;
                }
            }
            else
            {
                var upTile = new Vector2Int(group.Item1.x , group.Item1.y - 1);
                if (ThereAreAroundTwoOf(upTile, _tiles[group.Item1].Model.TileColor))
                {
                    return true;
                }
                
                var botTile = new Vector2Int(group.Item2.x, group.Item2.y +1);
                if (ThereAreAroundTwoOf(botTile, _tiles[group.Item1].Model.TileColor))
                {
                    return true;
                }
            }
            
            return false;
        }

        private bool ThereAreAroundTwoOf(Vector2Int tile, Match3TilesColors tileColor)
        {
            int around = 0;
            Match3TileView neighbour;
            var leftTile = new Vector2Int(tile.x - 1, tile.y);
            if (_tiles.TryGetValue(leftTile, out neighbour) && neighbour.Model.TileColor == tileColor)
            {
                around++;
            }

            var rightTile = new Vector2Int(tile.x + 1, tile.y);
            if (_tiles.TryGetValue(rightTile, out neighbour) && neighbour.Model.TileColor == tileColor)
            {
                around++;
            }

            var upTile = new Vector2Int(tile.x, tile.y - 1);
            if (_tiles.TryGetValue(upTile, out neighbour) && neighbour.Model.TileColor == tileColor)
            {
                around++;
            }

            var botTile = new Vector2Int(tile.x, tile.y + 1);
            if (_tiles.TryGetValue(botTile, out neighbour) && neighbour.Model.TileColor == tileColor)
            {
                around++;
            }

            return around >= 2;
        }

        private List<(Vector2Int, Vector2Int)> FindGroups()
        {
            List<(Vector2Int, Vector2Int)> groups = new List<(Vector2Int, Vector2Int)>();
            
            foreach (var tile in _tiles)
            {
                var rightVector = new Vector2Int(tile.Key.x + 1, tile.Key.y);
                if(_tiles.TryGetValue(rightVector, out var rightTile) && tile.Value.Model.TileColor == rightTile.Model.TileColor)
                {
                    groups.Add((tile.Key, rightVector));
                }
                
                var upVector = new Vector2Int(tile.Key.x, tile.Key.y+1);
                if(_tiles.TryGetValue(upVector, out var upTile) && tile.Value.Model.TileColor == upTile.Model.TileColor)
                {
                    groups.Add((tile.Key, upVector));
                }
            }

            return groups;
        }

        private Match3TileModel GetModelForPosition(int x, int y)
        {
            Match3TileConfig config = default; 
            bool isOk = false;
            int maxIterations = 5000;
            while (!isOk && maxIterations-- > 0)
            {
                config = GetConfig();
                isOk = CanGeneratedCheckingLeft(x, y, config);
                if (isOk)
                {
                    isOk = CanGeneratedCheckingUp(x, y, config);
                }
            }
            
            
            return new Match3TileModel(config);
        }

       private bool CanGeneratedCheckingLeft(int x, int y, Match3TileConfig config)
        {
            if (_tiles.TryGetValue(new Vector2Int(x - 1, y), out var leftConfig))
            {
                if (leftConfig.Model.TileColor == config.TileColor)
                {
                    if (_tiles.TryGetValue(new Vector2Int(x - 2, y), out var leftLeftConfig))
                    {
                        return leftLeftConfig.Model.TileColor != config.TileColor;
                    }
                }
            }

            return true;
        }
       
        private bool CanGeneratedCheckingUp(int x, int y, Match3TileConfig config)
        {
            if (_tiles.TryGetValue(new Vector2Int(x , y-1), out var upConfig))
            {
                if (upConfig.Model.TileColor == config.TileColor)
                {
                    if (_tiles.TryGetValue(new Vector2Int(x, y-2), out var upUpConfig))
                    {
                        return upUpConfig.Model.TileColor != config.TileColor;
                    }
                }
            }

            return true;
        }

        private Match3TileConfig GetConfig()
        {
            return _match3TilesConfig.Match3Tiles[Random.Range(0, _match3TilesConfig.Match3Tiles.Count)];
        }

        private Match3TileView GenerateTile()
        {
            var prefab = _match3TilesConfig.Match3TileViewPrefab;
            return Instantiate(prefab, _tileContainer);
        }
    }
}
