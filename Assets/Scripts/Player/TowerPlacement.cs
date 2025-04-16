using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _placementCheckMask;
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
            Ray camRay = _playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out RaycastHit hitInfo))
            {
                _currentTowerToPlace.transform.position = hitInfo.point;
            }

            if (hitInfo.collider.gameObject.CompareTag("CanPlace"))
            {
                _currentTowerToPlace.GetComponent<TowerBase>().SetColor(Color.white);
                if (Input.GetMouseButtonDown(0))
                {
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
