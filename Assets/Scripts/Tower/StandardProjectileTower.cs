using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StandardProjectileTower : MonoBehaviour
{
    [SerializeField] private Transform _headPivot;
    [SerializeField] private Transform _target;
    [Header("Firing and Projectile")]
    [SerializeField] private Transform _firingPoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    [Header("Enemy Effects")]
    [SerializeField] private bool _isTaunted;

    private void Start()
    {
        Invoke(nameof(Fire), 1f);
    }
    // Update is called once per frame
    void Update()
    {
        if (!_isTaunted)
        {
            TargetClosestEnemy();
        }
        AimAtTarget();
    }
    private void Fire()
    {
        Bullet projectile = Instantiate(_projectilePrefab, _firingPoint.position, Quaternion.identity).GetComponent<Bullet>();
        projectile.transform.rotation = _headPivot.transform.rotation;
        projectile.SetSpeed(_projectileSpeed);
        Invoke(nameof(Fire), 1f);
    }
    private void TargetClosestEnemy()
    {
        float closestDistance = float.MaxValue;
        EnemyBase closestEnemy = null;
        foreach (GameObject enemyGameObject in SpawnManager.SpawnedEnemies)
        {
            if (enemyGameObject.IsDestroyed()) continue;

            float tempDistance = Vector3.Distance(enemyGameObject.transform.position, transform.position);
            EnemyBase enemyScript = enemyGameObject.GetComponent<EnemyBase>();

            if (tempDistance < closestDistance) // TODO: Implement max range for tower
            {
                closestDistance = tempDistance;
                closestEnemy = enemyScript;
            }
        }

        if (closestEnemy == null)
        {
            // TODO: Idle animation
            // Remove scale placeholder
            transform.localScale = new Vector3(2, 2, 2);
        }
        else
        {
            // Remove scale placeholder
            transform.localScale = new Vector3(1, 1, 1);

            _target = closestEnemy.TowerAimPoint;
        }
    }
    private void AimAtTarget()
    {
        if (_headPivot == null) return;
        _headPivot.LookAt(_target);
    }

    public IEnumerator TauntedCoroutine(Transform taunterAimPoint, float duration)
    {
        _isTaunted = true;

        _target = taunterAimPoint;
        yield return new WaitForSeconds(duration);

        _isTaunted = false;
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
