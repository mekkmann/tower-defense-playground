using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _placementCheckMask;
    [SerializeField] private LayerMask _placementCollideMask;
    [SerializeField] private GameObject _currentTowerToPlace;
    [SerializeField] private GameObject _testTower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && _currentTowerToPlace == null)
        {
            SetTowerToPlace(_testTower);
        }

        if (_currentTowerToPlace != null)
        {
            BoxCollider towerToPlaceCollider = _currentTowerToPlace.GetComponent<BoxCollider>();
            towerToPlaceCollider.isTrigger = true;
            Ray camRay = _playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out RaycastHit hitInfo, 100f, _placementCollideMask, QueryTriggerInteraction.Ignore))
            {
                _currentTowerToPlace.transform.position = hitInfo.point;
                Debug.Log($"Hit: {hitInfo.collider.gameObject}");
            }
            Vector3 boxCenter = _currentTowerToPlace.transform.position + towerToPlaceCollider.center;
            Vector3 halfExtents = towerToPlaceCollider.size * 0.5f;
            bool isCollidingWithOtherObject = Physics.CheckBox(boxCenter, halfExtents, Quaternion.identity, _placementCheckMask, QueryTriggerInteraction.Ignore);
            if (hitInfo.collider.gameObject.CompareTag("CanPlace") && !isCollidingWithOtherObject)
            {
                _currentTowerToPlace.GetComponent<TowerBase>().SetColor(Color.white);
                if (Input.GetMouseButtonDown(0))
                {
                    towerToPlaceCollider.isTrigger = false;
                    _currentTowerToPlace = null;
                }
            }
            else
            {
                _currentTowerToPlace.GetComponent<TowerBase>().SetColor(Color.red);
            }
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        _currentTowerToPlace = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }
}
