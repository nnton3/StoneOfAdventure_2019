using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoneOfAdventure.Core
{
    public class SceneSwapper : MonoBehaviour
    {
        #region Variables
        [SerializeField] private int activatedSceneNumber;
        [SerializeField] private bool loadBossFight;

        private LocationPointsStorage pointsStorage;
        private LocationPointsBar pointsBar;
        private ProgressSaver progressSaver;
        private Transform player;
        [SerializeField] private GameObject bossHP_Bar;
        #endregion

        private void Start()
        {
            player = FindObjectOfType<PlayerStateController>().transform;
            progressSaver = FindObjectOfType<ProgressSaver>();
            pointsStorage = FindObjectOfType<LocationPointsStorage>();
            pointsBar = FindObjectOfType<LocationPointsBar>();
        }

        public void SwapScene()
        {
            SceneManager.LoadScene(activatedSceneNumber);
            progressSaver.InstanceNecessaryPrefs();
            player.transform.position = Vector3.zero;

            pointsStorage.ResetPointValue();
            pointsBar.SetActive(false);
            bossHP_Bar.SetActive(true);
        }
    }
}
