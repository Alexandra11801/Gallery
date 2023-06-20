using System.Collections;
using Gallery.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace Gallery.ImagesDownload
{
    public class ImageDownloader
    {
        private const string URL = "http://data.ikppbb.com/test-task-unity-data/pics/";
        private Sprite downloadedImage;
        
        public Sprite DownloadedImage => downloadedImage;
        
        public IEnumerator DownloadImage(int imageIndex)
        {
            var request = UnityWebRequestTexture.GetTexture(URL + imageIndex + ".jpg");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                downloadedImage = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
        }

        public IEnumerator DownloadImageWithProgressObservation(int imageIndex, float progressCoefficient, LoadingView view)
        {
            var request = UnityWebRequestTexture.GetTexture(URL + imageIndex + ".jpg");
            var operation = request.SendWebRequest();
            var progressOld = request.downloadProgress;
            float progressDelta;
            while (!operation.isDone)
            {
                progressDelta = request.downloadProgress - progressOld;
                progressOld = request.downloadProgress;
                view.AddPercents(progressDelta * progressCoefficient * 100);
                yield return null;
            }
            if (request.result == UnityWebRequest.Result.Success)
            {
                var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                downloadedImage = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
        }
    }
}