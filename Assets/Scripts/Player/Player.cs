using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _playerMovementInput;
    private Vector3 _playerMouseInput;
    private float _xRot;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController _controller;
    [Space]
    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _sensitivity;
    // Update is called once per frame
    void Update()
    {
        _playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _playerMouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(_playerMovementInput);

        if (Input.GetKey(KeyCode.Space))
        {
            _velocity.y = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            _velocity.y = -1f;
        }

        _controller.Move(moveVector * _speed * Time.deltaTime);
        _controller.Move(_velocity * _speed * Time.deltaTime);

        _velocity.y = 0f;
    }

    private void MovePlayerCamera()
    {
        _xRot -= _playerMouseInput.y * _sensitivity;
        transform.Rotate(0f, _playerMouseInput.x * _sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
    }
}
