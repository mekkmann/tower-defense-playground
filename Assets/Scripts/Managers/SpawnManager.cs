using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemiesToSpawn;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private float _spawnSpeed;

    // TODO: Check best practice for handling this
    public static List<EnemyBase> SpawnedEnemies = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TestSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TestSpawn()
    {
        EnemyBase temp = Instantiate(RandomEnemyPrefab(), new Vector3(_spawnPoint.position.x, _spawnPoint.position.y + 0.5f), Quaternion.identity).GetComponent<EnemyBase>();
        temp.SetWaypoints(_wayPoints);
        SpawnedEnemies.Add(temp);
        Invoke(nameof(TestSpawn), _spawnSpeed);
    }

    private GameObject RandomEnemyPrefab()
    {
        return _enemiesToSpawn[Random.Range(0, _enemiesToSpawn.Count)];
    }
}
