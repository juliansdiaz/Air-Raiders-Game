using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveConfig;
    EnemySpawner enemySpawner;
    Transform[] waypoints;
    int waypointIndex = 0;

    void Start()
    {
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waveConfig.GetStartingWaypoint().position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if(waypointIndex < waypoints.Length)
        {
            Vector3 targetPos = waypoints[waypointIndex].position;
            float moveDelta = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveDelta);
            if(transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}