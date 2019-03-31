using System.Collections;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        private Camera cam;
        private bool isEnemySpawn;
        private GameObject enemyPlace;
        private int enemyLeft;
        // enemy per second
        public float spawnSpeed = 1f;
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
        private void Update()
        {
            if (isEnemySpawn || enemyLeft <= 0 || LevelManager.Instance.IsGamePaused()) return;
            
            --enemyLeft;
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            isEnemySpawn = true;
            CreateEnemy();
            yield return new WaitForSeconds(1 / spawnSpeed);
            isEnemySpawn = false;
        }

        private void CreateEnemy()
        {
            var newEnemy = enemyPool.GetEnemy();
            newEnemy.transform.position = CalculateEnemySpawnPosition(newEnemy);
            InitializeEnemy(newEnemy);
        }

        private Vector3 CalculateEnemySpawnPosition(GameObject newEnemy)
        {
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

            return new Vector3(xCoords, yCoords);
        }
        
        private void InitializeEnemy(GameObject newEnemy)
        {
            var enemy = newEnemy.GetComponent<Enemy>();
            enemy.Speed = enemySo.speed;
            enemy.Strength = enemySo.strength;
            enemy.Health = enemySo.health;
            enemy.Technology = enemySo.technology;
            enemy.AttackSpeed = enemySo.attackSpeed;
            newEnemy.GetComponent<SpriteRenderer>().sprite = enemySo.sprite;

            newEnemy.SetActive(true);
        }
    }
}