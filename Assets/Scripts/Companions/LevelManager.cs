using UnityEngine;

namespace Companions
{
    public class LevelManager : MonoBehaviour
    {
        public void GoToMainMenu()
        {
            GameManager.instance.LoadMainMenu();
        }
    }
}