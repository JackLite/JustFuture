using UnityEngine;

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