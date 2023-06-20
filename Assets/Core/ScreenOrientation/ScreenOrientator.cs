using UnityEngine;

namespace Core.ScreenRotation
{
    public class ScreenOrientator : MonoBehaviour
    {
        [SerializeField] private ScreenOrientation orientation;

        private void Awake()
        {
            Screen.orientation = orientation;
        }
    }
}