using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace StoneOfAdventure.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Fader : MonoBehaviour
    {
        [SerializeField] private float period;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator Show()
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.fixedDeltaTime / 0.5f;
                yield return null;
            }
        }

        public IEnumerator Hide()
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.fixedDeltaTime / 0.5f;
                yield return null;
            }
        }
    }
}
