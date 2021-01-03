using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.World
{
public class SpawnManager : MonoBehaviour
    {
        // Singleton
        static SpawnManager instance;
        public SpawnManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SpawnManager>();
                }

                return instance;
            }
        }

        [SerializeField] GameObject enemyPrefab;    // Link the enemy prefab to make clones of -- this could be a list of prefabs that you randomly choose from
        [SerializeField] int spawnCount = 1;        // as many as you want

        int currentEnemyCount;

        // Our property will take care of spawning as it decrements
        public int CurrentEnemyCount
        {
            get { return currentEnemyCount; }
            set
            {
                currentEnemyCount = value;
                if (currentEnemyCount <= 0)
                {
                    CreateEnemies();
                }
            }
        }


        void Awake()
        {
            CreateEnemies();
        }

        void CreateEnemies()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(enemyPrefab);   // Add in spawn locations - whatever you want
                currentEnemyCount++;
            }
        }

        /// <summary>
        /// CALL THIS FROM ENEMY SCRIPT WHEN IT DIES
        /// The call would look like SpawnManager.Instance.OnEnemyDeath();
        /// </summary>
        public void OnEnemyDeath()
        {
            // As this gets to zero, it will spawn more through the property
            CurrentEnemyCount--;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 position = transform.position;
            position[1] = transform.position[1] + 5f;
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.DrawIcon(position, "T_5_sword_.png", true);
            Gizmos.DrawLine(transform.position, position);
        }
    }
}


