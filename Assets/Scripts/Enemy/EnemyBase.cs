using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Transform TowerAimPoint;

    [Header("Animation")]
    [SerializeField] protected Animator _animator;

    [Header("Movement")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private List<Transform> _waypoints;
    private int _currentWaypointIndex = 0;

    [Header("Health and Damage Reduction")]
    [SerializeField] private bool _isDead = false;
    public bool IsDead => _isDead;
    [SerializeField] private FloatingHealthBar _healthBar;
    [SerializeField] private int _currentHealth;
    public int CurrentHealth => _currentHealth;
    [SerializeField] private int _maxHealth;
    public int MaxHealth => _maxHealth;
    public float CurrentHealthPercentage => _currentHealth / _maxHealth;

    [Range(-1f, 1f)]
    [Tooltip("0 is no reduction, under 0 is increased damage, over 0 is reduced damage")]
    [SerializeField] private float _damageModifier = 0f;

    [Header("Damage and Multipliers")]
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _damageMultiplier;
    public float Damage => _baseDamage * _damageMultiplier;

    private bool _isReady = false;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
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
    protected virtual void Update()
    {
        if (!_isReady || _isDead) return;

        FollowPath();
    }
    #region Private Methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().Damage);
            return;
        }
    }

    // TODO: Refactor to destroy gameobject .5 seconds after death animation is finished
    private IEnumerator Die()
    {
        _isDead = true;
        _healthBar.gameObject.SetActive(false);
        _animator.SetBool("isDead", true);

        yield return new WaitForSeconds(2f);
        SpawnManager.RemoveFromSpawnedEnemies(gameObject);
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
        Debug.Log($"{gameObject.name} healed {incomingHeal} hp");
    }

    public void RestoreHealth(int incomingHeal)
    {
        Heal(incomingHeal);
    }

    private void TakeDamage(float incomingDamage)
    {
        // TODO: Add damage reduction
        _currentHealth -= CalculateIncomingDamage(incomingDamage);
        _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        Debug.Log($"{gameObject.name} took {CalculateIncomingDamage(incomingDamage)} damage");
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private int CalculateIncomingDamage(float incomingDamage)
    {
        return (int)(incomingDamage * (1f - _damageModifier));
    }
    #endregion

    #region Public Methods
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    public void SetWaypoints(List<Transform> waypoints)
    {
        _waypoints = waypoints;
        _isReady = true;
    }

    #endregion


}
