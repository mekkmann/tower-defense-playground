using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemiesToSpawn;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private float _spawnSpeed;
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
        EnemyBase temp = Instantiate(_enemiesToSpawn[0], new Vector3(_spawnPoint.position.x, _spawnPoint.position.y + 0.5f), Quaternion.identity).GetComponent<EnemyBase>();
        temp.SetWaypoints(_wayPoints);
        Invoke(nameof(TestSpawn), _spawnSpeed);
    }
}
