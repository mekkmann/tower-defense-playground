using UnityEngine;

public class Projectile : MonoBehaviour, IExplosive
{
    private float _projectileSpeed;
    private float _direction;
    [Header("Normal")]
    [SerializeField] private int _damage = 1;
    public int Damage => _damage;
    [Header("Explosive")]
    [SerializeField] private bool _isExplosive = false;
    [SerializeField] private float _blastRadius = 1f;
    #region Explosive Interface Variables
    public float BlastRadius => _blastRadius;
    #endregion
    [SerializeField] private int _explosionDamage = 1;
    public int ExplosionDamage => _explosionDamage;


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

        if (_isExplosive)
        {
            Explode();
        }

        Destroy(gameObject);
    }
    #region Explosive Interface Methods
    public void Explode()
    {
        // GameObject to hold collider
        GameObject blastZone = new("ExplosionRadius");
        blastZone.transform.position = transform.position; // Set blastZone position to the projectiles position

        // Add the SphereCollider
        SphereCollider collider = blastZone.AddComponent<SphereCollider>();
        collider.tag = "Explosion";
        //collider.isTrigger = true; // Don't block physics
        collider.radius = _blastRadius;

        // Add script to gameobject
        blastZone.AddComponent<Explosion>().Init(_explosionDamage);

        // Destroy GameObject
        Destroy(blastZone, 0.1f);
    }
    #endregion
}
