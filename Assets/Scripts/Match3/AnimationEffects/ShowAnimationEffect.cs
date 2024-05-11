using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sandsoft.Match3
{
    [CreateAssetMenu(fileName = "ShowAnimationEffect", menuName = "Sandsoft/AnimationEffects/ShowAnimationEffect", order = 1)]

    public class ShowAnimationEffect : GenericDotweenConfig<Image>
    {
        [Header("ScaleUp")]
        [SerializeField] 
        private float _durationScaleUp;
        [SerializeField] 
        private Ease _easeScaleUp;
        [SerializeField] 
        private float _scaleFinalMod;
        
        [Header("ScaleDown")]
        private float _durationScaleDown;
        [SerializeField] 
        private Ease _easeScaleDown;
        
        [Header("FadeIn")]
        [SerializeField] 
        private float _fadeInDuration;
        [SerializeField] 
        private Ease _easeFadeIn;
        
        public override void PlayEffect(Image image)
        {
            image.gameObject.SetActive(true);
            image.color = Color.clear;
            var originalScale = image.transform.localScale.x;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(image.transform.DOScale(originalScale*_scaleFinalMod, _durationScaleUp).SetEase(_easeScaleUp));
            sequence.Append(image.transform.DOScale(originalScale, _durationScaleDown).SetEase(_easeScaleDown));
            
            sequence.Insert(0, image.DOColor(Color.white, _fadeInDuration)).SetEase(_easeFadeIn);
        }
    }
}