using UnityEngine;
using UnityEngine.Events;

public class MapExit : MonoBehaviour
{
    private UnityEvent<float> _enemyGotThroughEvent;

    private void Start()
    {
        if (_enemyGotThroughEvent == null)
        {
            _enemyGotThroughEvent = new UnityEvent<float>();
        }
        _enemyGotThroughEvent.AddListener(TestFunc);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            _enemyGotThroughEvent.Invoke(other.GetComponent<EnemyBase>().Damage);
        }
    }

    private void TestFunc(float amount)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().TakeDamage(amount);
    }
}
