using UnityEngine;

namespace Sandsoft.Match3
{
    public abstract class GenericDotweenConfig<T> : ScriptableObject where T : Component
    {
        public abstract void PlayEffect(T component);
    }
}