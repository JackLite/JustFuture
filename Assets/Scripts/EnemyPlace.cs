using UnityEngine;

namespace Game
{
    public class EnemyPlace : MonoBehaviour
    {
        public Gun gunScript;

        private void Start()
        {
            gunScript = GameObject.Find("MainGun").GetComponent<Gun>();
        }
        
        
        }
    }