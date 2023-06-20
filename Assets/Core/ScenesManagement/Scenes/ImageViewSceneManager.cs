using Gallery.ScenesManagement.Gallery;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gallery.ScenesManagement
{
    public class ImageViewSceneManager : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private string gallerySceneName;

        private void Awake()
        {
            if (GalleryCash.Instance.PickedImage != null)
            {
                image.sprite = GalleryCash.Instance.PickedImage;
                image.SetNativeSize();
            }
        }

        public void Close()
        {
            SceneManager.LoadScene(gallerySceneName);
        }
    }
}