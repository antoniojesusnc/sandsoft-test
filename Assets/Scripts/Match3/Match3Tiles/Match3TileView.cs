using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sandsoft.Match3
{
    public class Match3TileView : MonoBehaviour
    {
        public Vector2Int Position { get; private set; }
        public Match3TileModel Model { get; private set; }

        [SerializeField] 
        private ShowAnimationEffect _showAnimationEffect;
        
        [SerializeField] 
        private Image _image;

        private void Awake()
        {
            _image.gameObject.SetActive(false);
            
        }
        public void DoShowEffect()
        {
            if (_showAnimationEffect != null)
            {
                _showAnimationEffect.PlayEffect(_image);
            }
        }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
        }

        public void SetModel(Match3TileModel model)
        {
            name = model.TileColor.ToString();
            Model = model;
            SetImage();
        }

        private void SetImage()
        {
            _image.sprite = Model.Image;
        }
    }
}