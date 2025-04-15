using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private List<Transform> _waypoints;
    private int _currentWaypointIndex = 0;
    private bool _isReady = false;

    private NavMeshAgent _agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
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

        FollowPath();
    }

    public void SetWaypoints(List<Transform> waypoints)
    {
        _waypoints = waypoints;
        _isReady = true;
    }


}
