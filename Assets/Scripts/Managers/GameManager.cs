using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    [Header("UI")]
    [SerializeField] private HealthBar _healthBar;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _healthBar.UpdateBar(_currentHealth, _maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Private Methods

    #endregion
    #region Public Methods
    public void TakeDamage(float incomingDamage)
    {
        _currentHealth -= (int)incomingDamage;
        _healthBar.UpdateBar(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
        {
            Debug.Log("CASE 1: [NO MORE HEALTH, ENEMY DEFEATED PLAYER]");
        }
    }
    #endregion
}
