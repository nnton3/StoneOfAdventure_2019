using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoneOfAdventure.Core
{
    public class SceneSwapper : MonoBehaviour
    {
        [SerializeField] private int activatedSceneNumber;

        public void SwapScene()
        {
            SceneManager.LoadScene(activatedSceneNumber);
        }
    }
}
