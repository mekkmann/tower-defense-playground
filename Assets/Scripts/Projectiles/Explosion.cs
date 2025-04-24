using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int ExplosionDamage { get; private set; } = 0;
    internal void Init(int explosionDamage)
    {
        ExplosionDamage = explosionDamage;
    }
}
