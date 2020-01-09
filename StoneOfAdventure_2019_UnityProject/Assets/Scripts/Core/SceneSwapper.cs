using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace StoneOfAdventure.Core
{
    public class SceneSwapper : MonoBehaviour
    {
        [SerializeField] private int activatedSceneNumber;
        [SerializeField] private bool loadBossFight;
        [HideInInspector] public UnityEvent TransitionToBossFight;

        private ProgressSaver progressSaver;

        private void Start()
        {
            progressSaver = FindObjectOfType<ProgressSaver>();

            AddTransitionListener();
        }

        private void AddTransitionListener()
        {
            var enemieSpawner = FindObjectOfType<EnemieFactory>();
            if (enemieSpawner != null)
            {
                TransitionToBossFight.AddListener(enemieSpawner.StopSpawn);
            }
        }

        public void SwapScene()
        {
            if (loadBossFight) TransitionToBossFight?.Invoke();

            SceneManager.LoadScene(activatedSceneNumber);
            progressSaver.InstanceNecessaryPrefs();
        }
    }
}
