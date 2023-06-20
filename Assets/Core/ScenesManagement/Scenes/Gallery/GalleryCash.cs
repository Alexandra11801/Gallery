using System.Collections.Generic;
using UnityEngine;

namespace Gallery.ScenesManagement.Gallery
{
    public class GalleryCash : MonoBehaviour
    {
        private static GalleryCash instance;
        [SerializeField]private List<Sprite> cashedImages;
        private float scrollRectPosition;
        private Sprite pickedImage;

        public static GalleryCash Instance => instance;
        public List<Sprite> CashedImages => cashedImages;

        public float ScrollRectPosition
        {
            get => scrollRectPosition;
            set => scrollRectPosition = value;
        }
        
        public Sprite PickedImage
        {
            get => pickedImage;
            set => pickedImage = value;
        }

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            cashedImages = new List<Sprite>();
            scrollRectPosition = 1;
        }
    }
}