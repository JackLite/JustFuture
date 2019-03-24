using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager _instance;
        private bool paused = false;
        private GameObject gameMenu;

        public static LevelManager instance
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

        public bool isGamePaused()
        {
            return paused;
        }

        public void pauseGame()
        {
            gameMenu.SetActive(true);
            paused = true;
        }

        public void resumeGame()
        {
            gameMenu.SetActive(false);
            paused = false;
        }
    }
}