using DG.Tweening;
using UnityEngine;

namespace UI.Sidebar
{
    public abstract class SidebarPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        private Tween _anchorPosTween;

        public virtual void Show()
        {
            _anchorPosTween.Kill();
            _anchorPosTween = _rectTransform.DOAnchorPosY(-1000, 0.5f);
        }

        public virtual void Hide()
        {
            _anchorPosTween.Kill();
            _anchorPosTween = _rectTransform.DOAnchorPosY(-1000, 0.5f);
        }
    }
}