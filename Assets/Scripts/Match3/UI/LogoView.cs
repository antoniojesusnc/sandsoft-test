using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sandsoft.Match3
{
    public class LogoView : MonoBehaviour
    {
        [SerializeField]
        private ShowLogoAnimationEffect _animation;

        void Awake()
        {
            DoLogoAnimation();
        }

        private void DoLogoAnimation()
        {
            if (_animation != null)
            {
                _animation.PlayEffect(GetComponent<RectTransform>());
            }
        }

        public void ClickOnButton()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}