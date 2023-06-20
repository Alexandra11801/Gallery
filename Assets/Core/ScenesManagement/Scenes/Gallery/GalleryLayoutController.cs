using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Gallery.ScenesManagement.Gallery
{
    public class GalleryLayoutController : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GridLayoutGroup layoutGroup;
        [SerializeField] private GalleryManager manager;
        [SerializeField] private GameObject imagePrefab;
        [SerializeField] private float scrollRectPositionThreshold;

        private void Awake()
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, layoutGroup.padding.bottom + layoutGroup.padding.top);
        }

        private void Update()
        {
            if (GetScrollRectPosition() < scrollRectPositionThreshold)
            {
                if (transform.childCount < manager.MaxImagesAmount)
                {
                    manager.StartCoroutine(nameof(manager.LoadNewImage));
                }

                if (transform.childCount < manager.MaxImagesAmount)
                {
                    manager.StartCoroutine(nameof(manager.LoadNewImage));
                }
            }
        }

        public int AddImage(Sprite sprite)
        {
            var imageIndex = AddEmptyImage();
            SetSpriteToImage(imageIndex, sprite);
            return imageIndex;
        }
        
        public int AddEmptyImage()
        {
            if (transform.childCount % 2 == 0)
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + layoutGroup.cellSize.y
                    + layoutGroup.spacing.y);
            }
            Instantiate(imagePrefab, layoutGroup.transform);
            return transform.childCount - 1;
        }
        
        public void SetSpriteToImage(int imageIndex, Sprite sprite)
        {
            transform.GetChild(imageIndex).GetComponent<Image>().sprite = sprite;
        }

        public int GetImagesCount()
        {
            return transform.childCount;
        }

        public int GetLoadedImagesCount()
        {
            return transform.GetComponentsInChildren<Image>().Count(i => i.sprite);
        }

        public float GetScrollRectPosition()
        {
            return scrollRect.normalizedPosition.y;
        }

        public void SetScrollRectPosition(float position)
        {
            scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, position);
        }
    }
}