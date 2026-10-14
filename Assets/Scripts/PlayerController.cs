using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float leftBoundPadding;
    [SerializeField] float rightBoundPadding;
    ShootController playerShootController;
    InputAction moveAction;
    InputAction shootAction;
    Vector3 moveVector;
    Vector2 minBounds;
    Vector2 maxBounds;

    void Start()
    {
        playerShootController = GetComponent<ShootController>();
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Fire");

        InitBounds();
    }

    void Update()
    {
        MovePlayer();
        PlayerShoot();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void MovePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        Vector3 newPos = transform.position + moveVector * moveSpeed * Time.deltaTime;

        newPos.x = Mathf.Clamp(newPos.x, minBounds.x + leftBoundPadding, maxBounds.x - rightBoundPadding);

        transform.position = newPos;
    }

    void PlayerShoot()
    {
        playerShootController.isShooting = shootAction.IsPressed();
    }
}
