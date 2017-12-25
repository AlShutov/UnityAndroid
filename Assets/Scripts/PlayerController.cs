using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float Xmin, Xmax, Zmin, Zmax;
}
public class PlayerController : MonoBehaviour {

    public float speed;
    public Boundary boundary;
    public GameObject shot;
    public GameObject poweredShot;
    public GameObject poweredShot2;
    public TouchPad touchPad;

    public Transform ShotSpawn;
    public Transform PoweredSpawn;
    public Transform PoweredSpawn2;
    public Transform PoweredSpawn3;
    
    public float fireRate;
    public float poweredFireRate;
    public float poweredFireRate2;
    public int powerUpped;
    

    private float nextFire;
    private Quaternion calibrationQuaternion;

     void Start()
    {
        powerUpped = 0;
        CalibrateAccelerometer();  
    }
    void Update()
    {
        if (Time.time > nextFire)
        {
            if (powerUpped >= 2)
            {
                nextFire = Time.time + poweredFireRate2;
                Instantiate(poweredShot, PoweredSpawn.position, ShotSpawn.rotation);
                nextFire = Time.time + poweredFireRate2;
                Instantiate(poweredShot2, PoweredSpawn2.position, ShotSpawn.rotation);
                nextFire = Time.time + poweredFireRate2;
                Instantiate(poweredShot2, PoweredSpawn3.position, ShotSpawn.rotation);
            }
            if (powerUpped == 1)
            {
                nextFire = Time.time + poweredFireRate;
                Instantiate(poweredShot, PoweredSpawn.position, ShotSpawn.rotation);
            }
            if (powerUpped == 0)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, ShotSpawn.position, ShotSpawn.rotation);
            }
        }
    }
    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 accelerationRaw = Input.acceleration;
        Vector3 acceleration = FixAcceleration(accelerationRaw);
        //Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);
        GetComponent<Rigidbody>().velocity = movement * speed;


        GetComponent<Rigidbody>().position = new Vector3
            (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.Xmin, boundary.Xmax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.Zmin, boundary.Zmax)
            );
    }
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;     
    }
}
