using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 1)]
    public class EnemySo : ScriptableObject
    {
        public float speed = 1f;
        public float health = 4000f;
        public float strength = 2f;
        public float attackSpeed = 0.1f;
        public float technology = 3f;
        public Sprite sprite;
    }
}