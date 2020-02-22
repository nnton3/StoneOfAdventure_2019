using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoneOfAdventure.UI
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private GameObject endGameWindow;

        public void EnableEndScreen()
        {
            endGameWindow.SetActive(true);
        }

        public void DisableEndScreen()
        {
            endGameWindow.SetActive(false);
        }

        public void ResetGame()
        {
            DisableEndScreen();
            Destroy(GameObject.Find("Core"));
            SceneManager.LoadScene(0);
        }

        public void ExitGame()
        {
            DisableEndScreen();
            Application.Quit();
        }
    }
}
