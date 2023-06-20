using System.Collections;
using Gallery.ImagesDownload;
using Gallery.UI;
using UnityEngine;

namespace Gallery.ScenesManagement.Gallery
{
    public class GalleryManager : MonoBehaviour
    {
        [SerializeField] private GalleryLayoutController layoutController;
        [SerializeField] private int startImagesAmount;
        [SerializeField] private int maxImagesAmount;
        [SerializeField] private LoadingView loadingView;

        public int MaxImagesAmount => maxImagesAmount;

        private void Start()
        {
            StartCoroutine(nameof(SetupImages));
        }

        private IEnumerator SetupImages()
        {
            AddCashedImages();
            if (layoutController.GetImagesCount() < startImagesAmount)
            {
                var initDownloadsCount = startImagesAmount - layoutController.GetImagesCount();
                for (int i = 0; i < initDownloadsCount; i++)
                {
                    StartCoroutine(nameof(LoadNewImageWithProgressObservation), initDownloadsCount);
                }
                yield return new WaitUntil(() => layoutController.GetLoadedImagesCount() == startImagesAmount);
            }
            loadingView.transform.root.gameObject.SetActive(false);
            layoutController.SetScrollRectPosition(GalleryCash.Instance.ScrollRectPosition);
        }
        
        private void AddCashedImages()
        {
            var images = GalleryCash.Instance.CashedImages;
            for(var i = 0; i < images.Count; i++)
            {
                layoutController.AddEmptyImage();
                if (images[i] != null)
                {
                    layoutController.SetSpriteToImage(i, images[i]);
                }
                else
                {
                    StartCoroutine(nameof(LoadImage), i);
                }
            }
        }
        
        public IEnumerator LoadNewImage()
        {
            var imageIndex = layoutController.AddEmptyImage();
            GalleryCash.Instance.CashedImages.Add(null);
            yield return LoadImage(imageIndex);
        }

        public IEnumerator LoadImage(int imageIndex)
        {
            var downloader = new ImageDownloader();
            yield return downloader.DownloadImage(imageIndex + 1);
            var image = downloader.DownloadedImage;
            layoutController.SetSpriteToImage(imageIndex, image);
            GalleryCash.Instance.CashedImages[imageIndex] = image;
        }

        public IEnumerator LoadNewImageWithProgressObservation(int totalDownloadsCount)
        {
            var imageIndex = layoutController.AddEmptyImage();
            GalleryCash.Instance.CashedImages.Add(null);
            var downloader = new ImageDownloader();
            yield return downloader.DownloadImageWithProgressObservation(imageIndex + 1, 
                1f / totalDownloadsCount, loadingView);
            var image = downloader.DownloadedImage;
            layoutController.SetSpriteToImage(imageIndex, image);
            GalleryCash.Instance.CashedImages[imageIndex] = image;
        }
    }
}