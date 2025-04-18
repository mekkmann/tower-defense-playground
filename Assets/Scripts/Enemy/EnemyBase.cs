using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Transform TowerAimPoint;

    [Header("Movement")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private List<Transform> _waypoints;
    private int _currentWaypointIndex = 0;

    [Header("Health and Damage Reduction")]
    [SerializeField] private FloatingHealthBar _healthBar;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    [Header("Damage and Multipliers")]
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _damageMultiplier;
    public float Damage => _baseDamage * _damageMultiplier;

    private bool _isReady = false;

    private void Awake()
    {
        _maxHealth = _currentHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void FollowPath()
    {
        if (_waypoints == null) return;
        Vector3 currentWaypointWithFixedY = new(_waypoints[_currentWaypointIndex].position.x, transform.position.y, _waypoints[_currentWaypointIndex].position.z);
        if (_currentWaypointIndex <= _waypoints.Count)
        {
            transform.LookAt(currentWaypointWithFixedY);
            transform.position += _speed * Time.deltaTime * transform.forward;
        }
        if (Vector3.Distance(transform.position, currentWaypointWithFixedY) <= 0.1f)
        {
            if (_currentWaypointIndex == _waypoints.Count - 1) Destroy(gameObject);

            _currentWaypointIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isReady) return;

        // Test Heal 
        if (Input.GetKeyDown(KeyCode.H)) Heal(3);

        // Test Damage
        if (Input.GetKeyDown(KeyCode.G)) TakeDamage(3);

        FollowPath();
    }
    #region Private Methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(3);
        }
    }
    private void Die()
    {
        // TO BE CHANGED
        Destroy(gameObject);
    }

    private void Heal(int incomingHeal)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += incomingHeal;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        }
    }

    private void TakeDamage(float incomingDamage)
    {
        _currentHealth -= (int)incomingDamage;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    #endregion

    #region Public Methods
    public void SetWaypoints(List<Transform> waypoints)
    {
        _waypoints = waypoints;
        _isReady = true;
    }

    #endregion


}
