using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace StoneOfAdventure.UI
{
    public class HealthPointsUI : MonoBehaviour
    {
        [SerializeField] private GameObject pointsUI;
        [Inject (Id = "HP_UI")] private ObjectPool hp_uiPool;
        [Inject] private Camera mainCamera;

        private Health health;

        private void Start()
        {
            health = GetComponentInParent<Health>();

            health.HPDecreased.AddListener((value) => CreatePointsUI(value.ToString(), Color.red));
            health.HPIncreased.AddListener((value) => CreatePointsUI(value.ToString(), Color.green));
        }

        private void CreatePointsUI(string text, Color pointsColor)
        {
            var hp_ui = hp_uiPool.GetObject();
            hp_ui.SetActive(true);
            hp_ui.transform.position = transform.position;
            var textComponent = hp_ui.GetComponentInChildren<Text>();
            textComponent.text = text;
            textComponent.color = pointsColor;
            hp_ui.GetComponent<DysableTimer>().StartCoroutine("ReturnToPool");
            hp_ui.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 3f, ForceMode2D.Impulse);
        }
    }
}
