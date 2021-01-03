using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        NavMeshAgent navMesh;

        bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0); //Health will become maximum at 0
            print(healthPoints);
            if(healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            if(!gameObject.tag.Equals("Player"))
            {
                Destroy(gameObject, 5);
            }
            else if(gameObject.tag.Equals("Player"))
            {
                print("Player is dead");
            }
            
        }


    }
}
