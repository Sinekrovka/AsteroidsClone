using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveSystem : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject shootPrefab;
    
    private Vector3 moveDirection;
    private Vector3 previeDirection;
    private float speedPreview;
    private Vector3 point;
    private Transform _player;
    private PlayerInput _input;
    private Camera cam;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Fire.performed += context => Shoot();
        _player = transform;
        speedPreview = speed;
        cam = Camera.main;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Shoot()
    {
        GameObject shootOnly = Instantiate(shootPrefab, _player.GetChild(0).position, _player.rotation, null);
        Destroy(shootOnly, 3f);
        
    }

    private void Update()
    {
       moveDirection = _input.Player.Move.ReadValue<Vector2>();
       point = cam.ScreenToWorldPoint(_input.Player.Look.ReadValue<Vector2>());
       LookAtPoint();
       if (moveDirection.Equals(Vector2.zero) & speed > 0)
       {
           speed = speed - speedPreview / 100;
           moveDirection = previeDirection;
       }
       else
       {
           speed = speedPreview;
       }
       MovePosition();
    }

    private void MovePosition()
    {
        _player.position += moveDirection * Time.deltaTime * speed;
        previeDirection = moveDirection;
    }

    private void LookAtPoint()
    {
        _player.LookAt(point, Vector3.forward);
        _player.rotation = Quaternion.Euler(0,0,_player.eulerAngles.z*-1);
    }
}
