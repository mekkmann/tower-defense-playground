using UnityEngine;

public class Tank : EnemyBase
{
    [Header("Tank Specific")]
    [SerializeField] private float _tauntDuration = 5f;
    [SerializeField] private float _tauntCooldown = 10f;
    private float _cooldownTracker = 0f;
    [SerializeField] private float _tauntRange = 10f;

    void Start()
    {
        _cooldownTracker = _tauntCooldown;
    }
    protected override void Update()
    {
        if (_cooldownTracker > 0f)
        {
            _cooldownTracker -= Time.deltaTime;
            if (_cooldownTracker <= 0f)
            {
                Taunt();
            }
        }

        base.Update();
    }

    // TODO: Refactor to use hurtbox
    private void Taunt()
    {
        var towers = GameObject.FindGameObjectsWithTag("Tower");
        // for every tower in _tauntrange
        foreach (var tower in towers)
        {
            if (Vector3.Distance(tower.transform.position, transform.position) > _tauntRange)
            {
                continue;
            }

            // set tower target to this gameobjects aimpoint
            StartCoroutine(
                        tower.GetComponent<StandardProjectileTower>().TauntedCoroutine(TowerAimPoint, _tauntDuration)
                        );
        }

        _cooldownTracker = _tauntCooldown;
    }
}
