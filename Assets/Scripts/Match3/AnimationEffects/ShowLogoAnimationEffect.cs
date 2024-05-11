using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sandsoft.Match3
{
    [CreateAssetMenu(fileName = "ShowLogoAnimationEffect", menuName = "Sandsoft/AnimationEffects/ShowLogoAnimationEffect", order = 1)]

    public class ShowLogoAnimationEffect : GenericDotweenConfig<RectTransform>
    {
        [Header("ScaleUp")]
        [SerializeField] 
        private float _duration;
        [SerializeField] 
        private Ease _ease;
        
        public override void PlayEffect(RectTransform logo)
        {
            logo.anchoredPosition = Vector2.up * 1000f;
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(logo.DOAnchorPos(Vector2.zero, _duration).SetEase(_ease));
        }
    }
}