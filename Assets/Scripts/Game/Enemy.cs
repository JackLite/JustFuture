using System.Collections;
using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {

        private float speed = 1f;
        private float health = 4000f;
        private float strength = 2f;
        private float attackSpeed = 0.1f;
        private float technology = 3f;

        private bool isAttacking;
        private bool isAttackInProcess;
        private LifeManager lifeManager;
        private TechnologyManager technologyManager;
        private LevelProgressManager levelProgressManager;
        private SpriteRenderer healthSpriteRenderer;
        private float defaultHealthSize;
        private EnemyPool enemyPool;
        
        public float defaultHealth;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public float Health
        {
            get { return health; }
            set { health = value; }
        }

        public float Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public float AttackSpeed
        {
            get { return attackSpeed; }
            set { attackSpeed = value; }
        }

        public float Technology
        {
            get { return technology; }
            set { technology = value; }
        }

        // Use this for initialization
        private void Start()
        {
            lifeManager = GameObject.Find("LifeManager").GetComponent<LifeManager>();
            technologyManager = GameObject.Find("TechnologyManager").GetComponent<TechnologyManager>();
            levelProgressManager = GameObject.Find("LevelProgressManager").GetComponent<LevelProgressManager>();
            healthSpriteRenderer = transform.Find("HealthBG/EhemyHealth").GetComponent<SpriteRenderer>();
            defaultHealthSize = healthSpriteRenderer.size.x;
            defaultHealth = health;
            enemyPool = GameObject.Find("EnemyPool").GetComponent<EnemyPool>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (LevelManager.Instance.IsGamePaused())
                return;
            if (!isAttacking)
                transform.Translate(new Vector3(-1f, 0) * speed * Time.deltaTime);
            else if (!isAttackInProcess)
                StartCoroutine(Attack());
        }

        public void SetDamage(Bullet bulletScript)
        {
            health -= bulletScript.damage;
            var healthSize = defaultHealthSize * health / defaultHealth;
            healthSpriteRenderer.size = new Vector2(healthSize, healthSpriteRenderer.size.y);
            if (health <= 0)
            {
                enemyPool.RemoveEnemy(gameObject);
            }
        }

        private IEnumerator Attack()
        {
            isAttackInProcess = true;
            lifeManager.ApplyDamage(strength);
            yield return new WaitForSeconds(1 / attackSpeed);
            isAttackInProcess = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Fort"))
            {
                isAttacking = true;
            }
        }

        private void OnDestroy()
        {
            levelProgressManager.Decrease();
            technologyManager.AddTechnology(technology);
        }
    }
}