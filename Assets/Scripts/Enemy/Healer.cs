using Unity.VisualScripting;
using UnityEngine;

public class Healer : EnemyBase
{
    [Header("Single Target Heal")]
    [SerializeField] private float _singleTargetHealRange = 10f;
    [SerializeField] private int _singleTargetHealValue = 5;
    [SerializeField] private float _singleTargetHealCooldown = 5f;
    [SerializeField] private ParticleSystem _singleTargetHealParticleSystem;
    private float _singleTargetHealCooldownTracker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _singleTargetHealCooldownTracker = _singleTargetHealCooldown;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (_singleTargetHealCooldownTracker >= 0)
        {
            _singleTargetHealCooldownTracker -= Time.deltaTime;
            if (_singleTargetHealCooldownTracker < 0)
            {
                HealLowestInRange(_singleTargetHealValue);
            }
        }

        base.Update();
    }

    private void HealLowestInRange(int value)
    {
        float lowestHealthPercentage = 1f; // 1f == 100%
        EnemyBase enemyToHeal = null;
        foreach (GameObject enemyGameObject in SpawnManager.SpawnedEnemies)
        {
            if (enemyGameObject.IsDestroyed()) continue;

            float tempDistance = Vector3.Distance(enemyGameObject.transform.position, transform.position);
            if (tempDistance > _singleTargetHealRange) continue; // if an enemy is out of range, go to next iteration
            EnemyBase enemyScript = enemyGameObject.GetComponent<EnemyBase>();
            if (enemyScript.CurrentHealthPercentage < lowestHealthPercentage)
            {
                lowestHealthPercentage = enemyScript.CurrentHealthPercentage;
                enemyToHeal = enemyScript;
            }
        }
        _animator.SetTrigger("heal");
        Instantiate(_singleTargetHealParticleSystem, enemyToHeal.TowerAimPoint.transform.position, Quaternion.identity);
        enemyToHeal.RestoreHealth(value);
        _singleTargetHealCooldownTracker = _singleTargetHealCooldown;
    }
}
