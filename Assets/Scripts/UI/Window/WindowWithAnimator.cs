using System.Collections;
using UnityEngine;

namespace Dennis.UI
{
    public class WindowWithAnimator : Window
    {
        [Header("Delay for Animations")]
        [SerializeField]
        private float _delayShow = 0.3f;
        [SerializeField]
        private float _delayHide = 0.3f;

        [Header("Animation Triggers")]
        [SerializeField]
        private string _showTrigger = "Show";
        [SerializeField]
        private string _hideTrigger = "Hide";

        private Coroutine _showHideCoroutine;
        private bool _isInitialized;
        private Animator _animator;

        private Animator _Animator
        {
            get
            {
                if(_animator == null)
                {
                    _animator = GetComponent<Animator>();
                }

                return _animator;
            }
        }

        #region Open Coroutine

        public override void Open()
        {
            gameObject.SetActive(true);
            StopCoroutine();
            _showHideCoroutine = StartCoroutine(DoShowAnimation());
        }

        private IEnumerator DoShowAnimation()
        {
            if(!_isInitialized)
            {
                yield return new WaitForSecondsRealtime(_delayShow);
                _isInitialized = true;
            }

            if(_Animator)
            {
                _Animator.SetTrigger(_showTrigger);
            }

            _showHideCoroutine = null;
        }

        #endregion

        #region Close Coroutine

        public override void Close()
        {
            StopCoroutine();
            _showHideCoroutine = StartCoroutine(DoHideAnimation());
        }

        private IEnumerator DoHideAnimation()
        {
            if(_Animator)
            {
                _Animator.SetTrigger(_hideTrigger);
            }

            yield return new WaitForSecondsRealtime(_delayHide);

            _showHideCoroutine = null;
            gameObject.SetActive(false);

            yield break;
        }

        #endregion

        #region Stop Coroutine

        private void StopCoroutine()
        {
            if(_showHideCoroutine != null)
            {
                StopCoroutine(_showHideCoroutine);
                _showHideCoroutine = null;
            }
        }

        #endregion
    }
}