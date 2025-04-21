using UnityEngine;

public class Tank : EnemyBase
{
    [Header("Tank Specific")]
    [SerializeField] private float _tauntCooldown = 15f;
    private float _cooldownTracker = 0f;
    [SerializeField] private float _tauntRange = 10f;

    void Start()
    {
        _cooldownTracker = _tauntCooldown;
    }
    protected override void Update()
    {
        base.Update();

        if (_cooldownTracker > 0f)
        {
            _cooldownTracker -= Time.deltaTime;
            if (_cooldownTracker <= 0f)
            {
                Taunt();
            }
        }
    }
    private void Taunt()
    {
        // for every tower in _tauntrange
        // set tower target to this gameobject

        _cooldownTracker = _tauntCooldown;
    }
}
