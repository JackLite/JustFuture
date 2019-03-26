using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        public EnemyPool enemyPool;

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

            var newEnemy = enemyPool.GetEnemy();
           
            var enemyHalfWidth = newEnemy.GetComponent<BoxCollider2D>().size.x / 2;
            var enemyHeight = newEnemy.GetComponent<BoxCollider2D>().size.y;
            var xCoords = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0)).x + enemyHalfWidth;

            var corners = new Vector3[4];
            enemyPlace.GetComponent<RectTransform>().GetWorldCorners(corners);
            var leftBottomCorner = cam.WorldToScreenPoint(corners[0]);
            var leftTopCorner = cam.WorldToScreenPoint(corners[1]);

            var yMin = cam.ScreenToWorldPoint(new Vector3(0, leftBottomCorner.y)).y;
            var yMax = cam.ScreenToWorldPoint(new Vector3(0, leftTopCorner.y)).y - enemyHeight / 2;

            var yCoords = Random.Range(yMin, yMax);

            var spawnPosition = new Vector3(xCoords, yCoords);

            newEnemy.transform.position = spawnPosition;
            newEnemy.SetActive(true);
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