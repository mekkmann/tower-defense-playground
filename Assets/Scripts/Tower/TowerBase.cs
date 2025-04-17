using Unity.VisualScripting;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [SerializeField] private Transform _headPivot;
    [SerializeField] private Transform _target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TargetClosestEnemy();
        AimAtTarget();
    }
    private void TargetClosestEnemy()
    {
        float closestDistance = float.MaxValue;
        EnemyBase closestEnemy = null;
        foreach (EnemyBase enemy in SpawnManager.SpawnedEnemies)
        {
            if (enemy.IsDestroyed()) continue;

            float tempDistance = Vector3.Distance(enemy.gameObject.transform.position, transform.position);
            if (tempDistance < closestDistance)
            {
                closestDistance = tempDistance;
                closestEnemy = enemy;
            }
        }

        _target = closestEnemy.gameObject.transform;
    }
    private void AimAtTarget()
    {
        if (_headPivot == null) return;
        _headPivot.LookAt(_target);
    }

    public void SetColor(Color color)
    {
        var allTemp = GetComponentsInChildren<MeshRenderer>();

        foreach (var temp in allTemp)
        {
            temp.material.color = color;
        }
    }
}
