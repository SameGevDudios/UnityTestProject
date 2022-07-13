using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float spawnDelay;

    private void Start()
    {
        StartCoroutine(SpawningEnemies());
    }

    private IEnumerator SpawningEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            GameObject buffer = Instantiate(bullet, transform.position, transform.rotation);
            buffer.GetComponent<Bullet>().SetSpeed(bulletSpeed);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
