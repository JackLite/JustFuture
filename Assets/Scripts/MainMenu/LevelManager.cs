using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class LevelManager : MonoBehaviour
    {
        public void startGame()
        {
            GameManager.instance.StartGame();
        }

        public void goToCompanions()
        {
            SceneManager.LoadScene("Companions");
        }
    }
}