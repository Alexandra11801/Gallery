using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallery.UI
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
        [SerializeField] private TextMeshProUGUI progressLabel;
        private float percents;

        public void AddPercents(float percents)
        {
            this.percents += percents;
            progressBar.fillAmount = this.percents / 100;
            progressLabel.text = Mathf.Floor(this.percents) + "%";
        }
    }
}