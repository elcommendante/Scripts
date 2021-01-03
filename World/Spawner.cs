using UnityEngine;
using RPG.Core;


public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject monster;
    public bool respawn;
    public float spawnDelay;
    public float currentTime;
    private bool spawning;




    void Start()
    {
        Spawn();
        currentTime = spawnDelay;
    }

    void Update()
    {
        if(spawning)
        {
            currentTime -=Time.deltaTime;
            if(currentTime <= 0)
            {
                Spawn();
            }
        }
    }

    public void Respawn()
    {
        spawning = true;
        currentTime = spawnDelay;
    }

    void Spawn()
    {
        IEnemy instance = Instantiate(monster, transform.position, Quaternion.identity).GetComponent<IEnemy>();
        spawning = false;
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 position = transform.position;
        position[1] = transform.position[1] + 5f;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawIcon(position, "T_5_sword_.png", true);
        Gizmos.DrawLine(transform.position,position);
    }
}
