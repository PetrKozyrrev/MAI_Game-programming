using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public Transform firePoint;    
    public GameObject bulletPrefab; 
    public float bulletForce = 20f; 

    [Header("Heat Mechanics")]
    public float heatValue = 0f;       
    public float heatPerShot = 10f;    
    public float coolingRate = 20f;    
    public float maxHeat = 100f;      
    private bool isOverheated = false; 

    public Slider heatSlider;

    void Update()
    {
        RotateGunToMouse();

        if (heatValue > 0) 
            heatValue -= coolingRate * Time.deltaTime;
        else 
            heatValue = 0;

        if (isOverheated && heatValue <= 0) 
            isOverheated = false;

        if (Input.GetButtonDown("Fire1") && !isOverheated) 
        {
            Shoot();
        }

        if (heatSlider != null){
            heatSlider.value = heatValue;
            if (isOverheated)
            {
                heatSlider.fillRect.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                heatSlider.fillRect.GetComponent<Image>().color = Color.red;
            }
        }
    }

    void RotateGunToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); 
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 lookPoint = ray.GetPoint(rayLength);
            
            Vector3 direction = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
            transform.LookAt(direction);
        }
    }

    void Shoot()
    {
        if (heatValue >= maxHeat){
            isOverheated = true;
            return;
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        
        Destroy(bullet, 3f);

        heatValue += heatPerShot;
    }
}