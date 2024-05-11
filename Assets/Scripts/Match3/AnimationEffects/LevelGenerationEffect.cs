using UnityEngine;

namespace Sandsoft.Match3
{
    [CreateAssetMenu(fileName = "LevelGenerationEffect", menuName = "Sandsoft/AnimationEffects/LevelGenerationEffect", order = 1)]

    public class LevelGenerationEffect : GenericDotweenConfig<Transform>
    {
        [field: Header("LevelGeneration")]
        [field: SerializeField] 
        public float TileDelay { get; private set; }
        
        public override void PlayEffect(Transform image)
        {
            
        }
    }
}