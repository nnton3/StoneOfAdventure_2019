using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoneOfAdventure.Core
{
    public class SceneSwapper : MonoBehaviour
    {
        [SerializeField] private int activatedSceneNumber;
        private ProgressSaver progressSaver;

        private void Start()
        {
            progressSaver = FindObjectOfType<ProgressSaver>();
        }

        public void SwapScene()
        {
            SceneManager.LoadScene(activatedSceneNumber);
            progressSaver.InstanceNecessaryPrefs();
        }
    }
}
