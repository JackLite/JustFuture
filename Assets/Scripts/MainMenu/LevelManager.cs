using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class LevelManager : MonoBehaviour
    {
        public void startGame()
        {
            GameManager.instance.startGame();
        }

        public void goToCompanions()
        {
            SceneManager.LoadScene("Companions");
        }
    }
}