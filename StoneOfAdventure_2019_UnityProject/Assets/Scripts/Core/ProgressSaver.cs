using UnityEngine;

namespace StoneOfAdventure.Core
{
    public class ProgressSaver : MonoBehaviour
    {
        [SerializeField] private GameObject followCamera;
        private Transform player;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            player = FindObjectOfType<PlayerStateController>().transform;
        }

        public void InstanceNecessaryPrefs()
        {
            var followCameraInstance = Instantiate(followCamera, Vector3.zero, Quaternion.identity);
            followCameraInstance.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = player;
        }
    }
}
