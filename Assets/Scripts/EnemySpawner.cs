using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        private Camera cam;
        private bool isEnemySpawn = false;
        private GameObject enemyPlace;
        private int enemyLeft;
        // enemy per second
        public float spawnSpeed = 1f;
        public GameObject enemyPrefab;
        public int enemyCount = 100;
        public EnemySo enemySo;

        // Use this for initialization
        void Start()
        {
            cam = Camera.main;
            enemyPlace = GameObject.Find("EnemyPlace");
            enemyLeft = enemyCount;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isEnemySpawn && enemyLeft > 0 && !LevelManager.instance.isGamePaused())
            {
                --enemyLeft;
                StartCoroutine(spawnEnemy());
            }
        }

        IEnumerator spawnEnemy()
        {
            isEnemySpawn = true;
            createEnemy();
            yield return new WaitForSeconds(1 / spawnSpeed);
            isEnemySpawn = false;
        }

        private void createEnemy()
        {
            float enemyHalfWidth = enemyPrefab.GetComponent<BoxCollider2D>().size.x / 2;
            float enemyHeight = enemyPrefab.GetComponent<BoxCollider2D>().size.y;
            float xCoords = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0)).x + enemyHalfWidth;

            Vector3[] corners = new Vector3[4];
            enemyPlace.GetComponent<RectTransform>().GetWorldCorners(corners);
            Vector3 leftBottomCorner = cam.WorldToScreenPoint(corners[0]);
            Vector3 leftTopCorner = cam.WorldToScreenPoint(corners[1]);

            float yMin = cam.ScreenToWorldPoint(new Vector3(0, leftBottomCorner.y)).y;
            float yMax = cam.ScreenToWorldPoint(new Vector3(0, leftTopCorner.y)).y - enemyHeight / 2;

            float yCoords = Random.Range(yMin, yMax);

            Vector3 spawnPosition = new Vector3(xCoords, yCoords);
            
            var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, 0, 0), null);
            var enemy = newEnemy.GetComponent<Enemy>();
            enemy.Speed = enemySo.speed;
            enemy.Strength = enemySo.strength;
            enemy.Health = enemySo.health;
            enemy.Technology = enemySo.technology;
            enemy.AttackSpeed = enemySo.attackSpeed;
            newEnemy.GetComponent<SpriteRenderer>().sprite = enemySo.sprite;
        }
    }
}