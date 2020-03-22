using UnityEngine;
using Zenject;

namespace StoneOfAdventure.Combat
{
    public class SkullOfDecay_buff : MonoBehaviour
    {
        private float damageInPercent;
        private float lifestealValueInPercent;
        private float periodicity;
        private Health health;
        [Inject] private DiContainer Container;

        public void Initialize(float damageInPercent, float lifesteal, float periodicity)
        {
            this.damageInPercent = damageInPercent;
            this.lifestealValueInPercent = lifesteal;
            this.periodicity = periodicity;
            layerMask = LayerMask.GetMask("Enemie");

            Container.Inject(this);
        }

        private void Start()
        {
            health = GetComponent<Health>();

            InvokeRepeating("SkullAreaTick", 1f, periodicity);
        }

        private void SkullAreaTick()
        {
            var centerInRelationUnitDirection =
                    transform.position + applicationAreaCenter;
            Collider2D[] enemiesInApplicationArea = Physics2D.OverlapBoxAll(
                centerInRelationUnitDirection,
                applicationArea,
                0f,
                layerMask);

            var heal = 0f;
            if (enemiesInApplicationArea.Length != 0)
            {
                foreach (var enemie in enemiesInApplicationArea)
                {
                    var enemieHealth = enemie.GetComponent<Health>();
                    var damage = (int)(enemieHealth.HealthPoints * damageInPercent);
                    enemieHealth.ApplyDamage(damage);
                    heal += damage * lifestealValueInPercent;
                }
                health.Heal((int)heal);
            }
        }

        [SerializeField] private Vector3 applicationAreaCenter = new Vector3(0f, 0.5f, 0f);
        [SerializeField] private Vector3 applicationArea = new Vector3(5f, 1f, 1f);
        [SerializeField] private bool applicationAreaVisible;
        [SerializeField] private LayerMask layerMask;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (applicationAreaVisible)
                Gizmos.DrawWireCube(transform.position + applicationAreaCenter, applicationArea);
        }
    }
}
