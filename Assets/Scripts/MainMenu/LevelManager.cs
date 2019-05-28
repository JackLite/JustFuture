using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class LevelManager : MonoBehaviour
    {
        public void StartGame()
        {
            GameManager.instance.StartGame();
        }

        public void GoToCompanions()
        {
            SceneManager.LoadScene("CompanionsMenu");
        }
    }
}