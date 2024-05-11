using DG.Tweening;
using UnityEngine;

namespace Sandsoft.Match3
{
    [CreateAssetMenu(fileName = "ShowAnimationEffect", menuName = "Sandsoft/AnimationEffects/ShowAnimationEffect", order = 1)]

    public class ShowAnimationEffect : GenericDotweenConfig<RectTransform>
    {
        [SerializeField] 
        private float _durationScaleUp;
        [SerializeField] 
        private float _durationScaleDown;
        
        [SerializeField] 
        private float _scaleFinalMod;
        
        [SerializeField] 
        private Ease _easeScaleUp;
        [SerializeField] 
        private Ease _easeScaleDown;
        
        public override void PlayEffect(RectTransform component)
        {
            var originalScale = component.localScale.x;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(component.DOScale(originalScale*_scaleFinalMod, _durationScaleUp).SetEase(_easeScaleUp));
            sequence.Append(component.DOScale(originalScale, _durationScaleDown).SetEase(_easeScaleDown));
        }
    }
}