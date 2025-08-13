using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static Vector2 Movement;
    private InputAction moveAction;
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

   
    void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();
    }
}
