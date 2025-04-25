using UnityEngine;
public interface IExplosive
{
    float BlastRadius { get; }
    void Explode(Vector3 position);
}
