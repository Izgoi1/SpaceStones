using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroid : MonoBehaviour
{
    public float minSpeed = 6000f;
    public float maxSpeed = 9000f;

    private float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = Random.Range(this.minSpeed, this.maxSpeed);

        rb.AddForce(transform.position * this.speed);

    }

    private void Update()
    {
        if (transform.position.x < -9)
        {
            transform.position = new Vector3(transform.position.x + 17f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 9)
        {
            transform.position = new Vector3(transform.position.x - 17f, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 12f, transform.position.z);
        }
        else if (transform.position.y > 6)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 12, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
            PlayerManager.score += 50;
        }
    }

}
