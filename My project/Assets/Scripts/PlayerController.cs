using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 15f;   
    public float turnSpeed = 100f;  

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");  
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        MovePlayer();
        TurnPlayer();
    }

    void MovePlayer()
    {
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void TurnPlayer()
    {
        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}