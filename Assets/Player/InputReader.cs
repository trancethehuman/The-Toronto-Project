using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private Controls playerControls;
    [SerializeField] private float speed;

    private void Awake()
    {
        playerControls = new Controls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();
        GetComponent<Rigidbody2D>().AddForce(new Vector3(move.x * speed * Time.deltaTime, move.y * speed * Time.deltaTime, 0));
    }
}