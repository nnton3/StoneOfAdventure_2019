using System;
using System.Collections;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace StoneOfAdventure.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Fader : MonoBehaviour
    {
        [SerializeField] private float period;

        private CanvasGroup canvasGroup;
        private bool startShow;
        private bool startHide;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            BindEvents();
        }

        private void BindEvents()
        {
            this.UpdateAsObservable()
                            .Where(_ => startShow)
                            .Subscribe(_ =>
                            {
                                if (canvasGroup.alpha < 1f)
                                    canvasGroup.alpha += Time.fixedDeltaTime / period;
                                else
                                    startShow = false;
                            });

            this.UpdateAsObservable()
                .Where(_ => startHide)
                .Subscribe(_ =>
                {
                    if (canvasGroup.alpha > 0f)
                        canvasGroup.alpha -= Time.fixedDeltaTime / period;
                    else
                        startHide = false;
                });
        }

        public void Show()
        {
            startShow = true;
        }

        public void Hide()
        {
            startHide = true; 
        }
    }
}
