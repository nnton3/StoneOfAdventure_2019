using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StoneOfAdventure.Core
{
    public class SceneSwapper : MonoBehaviour
    {
        #region Variables
        [SerializeField] private int activatedSceneNumber;
        [SerializeField] private bool loadBossFight;

        private LocationPointsStorage pointsStorage;
        private ProgressSaver progressSaver;
        private Transform player;
        #endregion

        private void Start()
        {
            player = FindObjectOfType<PlayerStateController>().transform;
            progressSaver = FindObjectOfType<ProgressSaver>();
            pointsStorage = FindObjectOfType<LocationPointsStorage>();
        }

        public void SwapScene()
        {
            SceneManager.LoadScene(activatedSceneNumber);
            progressSaver.InstanceNecessaryPrefs();
            player.transform.position = Vector3.zero;

            pointsStorage.ResetPointValue();
        }
    }
}
