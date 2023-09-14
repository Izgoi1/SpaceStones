using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public AsteroidScript asteroidPrefab;
    public float spawnDistance = 5f;
    public float trajectoryVariance = 5f;
    public int spawnAmount = 5;


    private void Start()
    {
        Spawn();
    }


    private void Spawn()
    {
        while (spawnAmount > 0) 
        {

            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            AsteroidScript asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);

            spawnAmount--;

        }
    }

}
