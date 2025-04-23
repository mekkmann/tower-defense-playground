using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _projectileSpeed;
    private float _direction;
    [SerializeField] private int _damage = 1;
    public int Damage => _damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * _projectileSpeed;
    }


    public void SetSpeed(float value)
    {
        _projectileSpeed = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // logic

        Destroy(gameObject);
    }
}
