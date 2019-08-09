using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    //Limites de pantalla
    public float xMin, xMax;
}

public class PlayerController : MonoBehaviour
{
    public bool moveWithCel;
    public float speed, distanceY;
    public Boundary boundary;
    Rigidbody2D rbd;
    public GameObject shot;
    public Transform shotSpawn;

    public float fireRate;
    float minFireRate = 0.2f;
    private float nextFire;

    float moveHorizontal;


    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        fireRate = 0.5f;
    }

    void Update()
    {

        // Disparo y recarga
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.identity);
            GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate()
    {
        // movimiento nave
        if (moveWithCel)
        {
            moveHorizontal = Input.acceleration.x;
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal");
        }

        Vector2 movement = new Vector2(moveHorizontal, distanceY);
        rbd.velocity = new Vector2 (movement.x * speed, distanceY);

        // Limites donde se puede mover
        rbd.position = new Vector2(Mathf.Clamp(rbd.position.x, boundary.xMin, boundary.xMax), distanceY);
    }

    public void AddRate()
    {
        if (fireRate <= minFireRate)
        {
            fireRate = minFireRate;
        }else
        {
            fireRate -= 0.05f;
        }
    }
}