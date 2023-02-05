using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<GameObject> enemies;
}
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float distanceFromCenter = 5;
    [SerializeField] private Vector2 yRange = new Vector2();
    [SerializeField] List<Wave> waves;
    [SerializeField] float maxTimeBetweenWaves;



    private float currentTime;
    private int currentIndex = -1;
    private float enemyAlive = 0;
    private bool started = false;
    private CameraController camController;
    private void Start()
    {
        camController = FindObjectOfType<CameraController>();
    }
    public void Trigger()
    {
        started = true;
    }


    private void Update()
    {
        if (!started)
        {
            if (camController.transform.position.x > transform.position.x)
            {
                camController.ChangeMode(false);
                Trigger();
            }
            return;
        }
        if (currentIndex >= waves.Count)
        {
            print(currentIndex);
            camController.ChangeMode(true);
            Destroy(gameObject);
            return;
        }
        currentTime += Time.deltaTime;
        if (enemyAlive == 0 || currentTime > maxTimeBetweenWaves)
        {
            currentIndex += 1;
            StartWave(currentIndex);
            currentTime = 0;
        }
    }

    private void StartWave(int index)
    {
        foreach (GameObject enemy in waves[index].enemies)
        {
            int random = Random.Range(0, 2);
            Vector3 pos = new Vector3(distanceFromCenter * (random == 0 ? -1 : 1) + transform.position.x, 0, 0);
            float yRand = Random.Range(yRange.x, yRange.y);
            pos += yRand * (Vector3.up + Vector3.forward / Mathf.Tan(Global.depthSlope / 180 * Mathf.PI));
            Health enemyHealth = Instantiate(enemy, pos, Quaternion.identity).GetComponent<Health>();
            enemyHealth.deathEvent.AddListener(EnemyDeath);
            enemyAlive += 1;
        }
    }

    private void EnemyDeath()
    {
        enemyAlive -= 1;
    }
}
