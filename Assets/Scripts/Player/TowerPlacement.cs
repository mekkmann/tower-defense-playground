using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _placementCheckMask;
    [SerializeField] private LayerMask _placementCollideMask;
    [SerializeField] private GameObject _currentTowerToPlace;
    private BoxCollider _currentTowerToPlaceCollider;
    [SerializeField] private GameObject _testTower;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && _currentTowerToPlace == null)
        {
            SetTowerToPlace(_testTower);
        }

        if (_currentTowerToPlace != null)
        {
            RaycastHit hitInfo = MoveUnplacedTowerToRaycastPosition();


            if (CanPlaceTower(hitInfo))
            {
                // TODO: Refactor color change
                _currentTowerToPlace.GetComponent<StandardProjectileTower>().SetColor(Color.white);
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceTower();
                }
            }
            else
            {
                // TODO: Refactor color change
                _currentTowerToPlace.GetComponent<StandardProjectileTower>().SetColor(Color.red);
            }
        }
    }

    private bool CanPlaceTower(RaycastHit hitInfo)
    {
        return hitInfo.collider.gameObject.CompareTag("CanPlace") && !IsCollidingWithAnotherObject();
    }
    private bool IsCollidingWithAnotherObject()
    {
        Vector3 boxCenter = _currentTowerToPlace.transform.position + _currentTowerToPlaceCollider.center;
        Vector3 halfExtents = _currentTowerToPlaceCollider.size * 0.5f;
        return Physics.CheckBox(boxCenter, halfExtents, Quaternion.identity, _placementCheckMask, QueryTriggerInteraction.Ignore);
    }
    private RaycastHit MoveUnplacedTowerToRaycastPosition()
    {
        Ray camRay = _playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out RaycastHit hitInfo, 100f, _placementCollideMask, QueryTriggerInteraction.Ignore))
        {
            _currentTowerToPlace.transform.position = hitInfo.point;
        }

        return hitInfo;
    }
    private void PlaceTower()
    {
        _currentTowerToPlaceCollider.isTrigger = false;
        _currentTowerToPlace = null;
        _currentTowerToPlaceCollider = null;
    }
    public void SetTowerToPlace(GameObject tower)
    {
        _currentTowerToPlace = Instantiate(tower, Vector3.zero, Quaternion.identity);
        _currentTowerToPlaceCollider = _currentTowerToPlace.GetComponent<BoxCollider>();
        _currentTowerToPlaceCollider.isTrigger = true;
    }
}
