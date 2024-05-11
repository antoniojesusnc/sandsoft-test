using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sandsoft.Match3
{
    public class LevelGeneratorView : MonoBehaviour
    {
        [Header("Config")] [SerializeField] private Match3TilesConfig _match3TilesConfig;

        [Header("Level Objects")] [SerializeField]
        private GridLayoutGroup _gridLayout;

        [SerializeField] private Transform _tileContainer;

        private Dictionary<Vector2Int, Match3TileView> _tiles = new();
        private LevelGeneratorController _levelGeneratorController;
        private LevelModel _levelModel;

        IEnumerator Start()
        {
            _levelGeneratorController = new LevelGeneratorController();
            yield return 0;
            CalculateGridSize();
        }

        private void CalculateGridSize()
        {
            var size = GetComponent<RectTransform>().rect;
            var xSize =
                (size.width - _gridLayout.padding.left - _gridLayout.padding.right -
                 _gridLayout.spacing.x * (_match3TilesConfig.TileSize.x - 1)) / _match3TilesConfig.TileSize.x;

            var ySize =
                (size.height - _gridLayout.padding.top - _gridLayout.padding.bottom -
                 _gridLayout.spacing.y * (_match3TilesConfig.TileSize.y - 1)) / _match3TilesConfig.TileSize.y;

            var finalSize = Mathf.Min(xSize, ySize);
            _gridLayout.cellSize = new Vector2(finalSize, finalSize);
        }

        private void DestroyCurrentLevel()
        {
            _levelModel.CleanLevel();

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
            _levelModel = _levelGeneratorController.GenerateLevel(_match3TilesConfig);

            CreateLevel();
        }

        private void CreateLevel()
        {
            _gridLayout.enabled = true;
            foreach (var tileModel in _levelModel.Tiles)
            {
                var tile = GenerateTile();
                tile.SetPosition(tileModel.Position);
                tile.SetModel(tileModel);
                _tiles.Add(tileModel.Position, tile);
            }
        }

        private Match3TileView GenerateTile()
        {
            var prefab = _match3TilesConfig.Match3TileViewPrefab;
            return Instantiate(prefab, _tileContainer);
        }
    }
}
