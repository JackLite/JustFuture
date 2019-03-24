using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Companions
{
    public class LevelManager : MonoBehaviour
    {
        public void goToMainMenu()
        {
            GameManager.instance.LoadMainMenu();
        }
    }
}