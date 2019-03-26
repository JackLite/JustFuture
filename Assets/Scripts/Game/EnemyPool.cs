using UnityEngine;

namespace Game
{
    public class EnemyPool : MonoBehaviour
    {
        public GameObject enemy;
        
        public GameObject GetEnemy()
        {
            
            for (var i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.activeSelf)
                {
                    return transform.GetChild(i).gameObject;
                }
            }

            var newBullet = Instantiate(enemy, transform);
            newBullet.SetActive(false);
            return newBullet;
        }

        public void RemoveEnemy(GameObject enemyObj)
        {
            enemyObj.SetActive(false);
        }
    }
}