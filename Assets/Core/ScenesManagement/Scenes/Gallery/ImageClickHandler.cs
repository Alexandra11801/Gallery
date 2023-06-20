using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gallery.ScenesManagement.Gallery
{
    public class ImageClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private string viewSceneName;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (image.sprite)
            {
                GalleryCash.Instance.PickedImage = image.sprite;
                GalleryCash.Instance.ScrollRectPosition = GetComponentInParent<GalleryLayoutController>().GetScrollRectPosition();
                SceneManager.LoadScene(viewSceneName);
            }
        }
    }
}