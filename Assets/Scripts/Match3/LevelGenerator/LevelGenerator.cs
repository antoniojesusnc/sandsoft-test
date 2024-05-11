using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
            for (int i = 0; i < _match3TilesConfig.TileSize.x; i++)
            {
                for (int j = 0; j < _match3TilesConfig.TileSize.y; j++)
                {
                    var tile = GenerateTile();
                    var position = new Vector2Int(i, j);
                    tile.SetPosition(position);
                    tile.SetModel(GetModelForPosition(i, j));
                    _tiles.Add(position, tile);
                }
            }
            
        }

        private Match3TileModel GetModelForPosition(int x, int y)
        {
            var config = _match3TilesConfig.Match3Tiles[Random.Range(0, _match3TilesConfig.Match3Tiles.Count)];
            return new Match3TileModel(config);
        }

        private Match3TileView GenerateTile()
        {
            var prefab = _match3TilesConfig.Match3TileViewPrefab;
            return Instantiate(prefab, _tileContainer);
        }
    }
}
