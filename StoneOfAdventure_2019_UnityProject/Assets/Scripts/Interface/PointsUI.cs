using StoneOfAdventure.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace StoneOfAdventure.UI
{
    public class PointsUI : MonoBehaviour
    {
        [SerializeField] private GameObject pointsUI;
        [Inject (Id = "HP_UI")] private ObjectPool hp_uiPool;

        private void Start()
        {
            GetComponentInParent<Flip>().Flipped.AddListener(() =>
            {
                transform.localScale = new Vector3(transform.localScale .x * - 1f, 1f, 1f);
            });
        }

        public void CreatePointsUI(string text, Color pointsColor, int size = 14)
        {
            var hp_ui = hp_uiPool.GetObject();
            hp_ui.SetActive(true);
            hp_ui.transform.position = transform.position;
            var textComponent = hp_ui.GetComponentInChildren<Text>();
            textComponent.text = text;
            textComponent.color = pointsColor;
            textComponent.fontSize = size;
            hp_ui.GetComponent<DysableTimer>().StartCoroutine("ReturnToPool");
            hp_ui.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 3f, ForceMode2D.Impulse);
        }
    }
}
