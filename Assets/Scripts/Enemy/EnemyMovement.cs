using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavePointIndex = 0;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = WayPoint.points[wavePointIndex];
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(target.position, transform.position) <= 0.4f)
        {
            GetToNextWavePoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetToNextWavePoint()
    {
        if (wavePointIndex >= WayPoint.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavePointIndex++;
        target = WayPoint.points[wavePointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        SpawnManager.EnemiesAlive--;
        Destroy(gameObject);
    }
}
