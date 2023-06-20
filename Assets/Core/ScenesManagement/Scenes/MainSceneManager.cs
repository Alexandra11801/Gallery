using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gallery.ScenesManagement.ImageView
{
    public class MainSceneManager : MonoBehaviour
    {
        [SerializeField] private string gallerySceneName;

        public void LoadGallery()
        {
            SceneManager.LoadScene(gallerySceneName);
        }
    }
}