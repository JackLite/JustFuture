using UnityEngine;

namespace Game
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager _instance;
        private bool paused;
        private GameObject gameMenu;

        public static LevelManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LevelManager>();
                }
                return _instance;
            }
        }

        private void Start()
        {
            gameMenu = GameObject.FindWithTag("Game Menu");
            gameMenu.SetActive(false);
        }

        public bool IsGamePaused()
        {
            return paused;
        }

        public void PauseGame()
        {
            gameMenu.SetActive(true);
            paused = true;
        }

        public void ResumeGame()
        {
            gameMenu.SetActive(false);
            paused = false;
        }

        public void ExitGame()
        {
            GameManager.instance.LoadMainMenu();
        }
    }
}