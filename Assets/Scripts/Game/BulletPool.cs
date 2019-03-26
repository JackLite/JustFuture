using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BulletPool : MonoBehaviour
    {
        public GameObject bullet;

        public GameObject GetBullet()
        {
            
            for (var i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.activeSelf)
                {
                    return transform.GetChild(i).gameObject;
                }
            }

            var newBullet = Instantiate(bullet, transform);
            newBullet.SetActive(false);
            return newBullet;
        }

        public void RemoveBullet(GameObject bulletObj)
        {
            bulletObj.SetActive(false);
        }
    }
}