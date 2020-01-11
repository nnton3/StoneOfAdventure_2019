using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StoneOfAdventure.Core
{
    public class SceneSwapper : MonoBehaviour
    {
        [SerializeField] private int activatedSceneNumber;
        [SerializeField] private bool loadBossFight;

        private LocationPointsStorage pointsStorage;
        private ProgressSaver progressSaver;
        private GameObject button;

        private void Start()
        {
            progressSaver = FindObjectOfType<ProgressSaver>();
            pointsStorage = FindObjectOfType<LocationPointsStorage>();
            button = GetComponentInChildren<Button>().gameObject;

            button.SetActive(false);

            pointsStorage.LocationCompleted.AddListener(Activate);
        }

        public void SwapScene()
        {
            SceneManager.LoadScene(activatedSceneNumber);
            progressSaver.InstanceNecessaryPrefs();

            pointsStorage.ResetPointValue();
        }

        public void Activate()
        {
            button.gameObject.SetActive(true);
        }
    }
}
