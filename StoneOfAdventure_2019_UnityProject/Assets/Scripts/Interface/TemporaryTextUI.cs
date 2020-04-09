using System;
using UnityEngine;
using UnityEngine.UI;

namespace StoneOfAdventure.UI
{
    public class TemporaryTextUI : MonoBehaviour
    {
        [SerializeField] private float lifetime;
        [SerializeField] private float height;

        private CanvasGroup canvasGroup;
        private Text textUI;

        private float currentLifetime;

        private void Awake()
        {
            textUI = GetComponentInChildren<Text>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetText(string text)
        {
            SetText(text, Color.white);
        }

        public void SetText(string text, Color color)
        {
            textUI.text = text;
            textUI.color = color;
        }

        private void FixedUpdate()
        {
            CheckToDestroy();
            ApplyEffects();
        }

        private void CheckToDestroy()
        {
            currentLifetime += Time.deltaTime;
            if (currentLifetime >= lifetime)
                ReturnToPool();
        }

        private void ApplyEffects()
        {
            canvasGroup.alpha = Mathf.MoveTowards(1f, 0f, currentLifetime / lifetime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up * height, currentLifetime / lifetime);
        }

        private void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
