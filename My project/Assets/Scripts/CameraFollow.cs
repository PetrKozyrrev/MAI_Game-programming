using UnityEngine;

public class CameraRearFollow : MonoBehaviour
{
    public Transform target;     

    [Header("Position Offsets (Local)")]
    public float distance = 11f;   
    public float height = 7f;       
    public float lookAtBias = 0.8f; 

    [Header("Smoothing")]
    public float smoothSpeed = 10f; 

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 worldPosOffset = (target.forward * -distance) + (target.up * height);
        
        Vector3 desiredPosition = target.position + worldPosOffset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);

        Quaternion lookRotation = target.rotation;

        Vector3 directionToLook = target.position - transform.position;
        if (directionToLook != Vector3.zero)
        {
             Quaternion centerRotation = Quaternion.LookRotation(directionToLook);
             lookRotation = Quaternion.Slerp(target.rotation, centerRotation, lookAtBias);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, smoothSpeed * Time.fixedDeltaTime);
    }
}