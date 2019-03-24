using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class EnemyPlace : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Gun gunScript;

        private void Start()
        {
            gunScript = GameObject.Find("MainGun").GetComponent<Gun>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!LevelManager.instance.isGamePaused())
            {
                gunScript.startFire();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            gunScript.stopFire();
        }
    }
}