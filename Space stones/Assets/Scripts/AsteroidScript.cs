using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AsteroidScript : MonoBehaviour
{
    /*public float size = 0.5f;*/
/*    public float minSize = 0.5f;
    public float maxSize = 0.9f;*/
    public float minSpeed = 1000;
    public float maxSpeed = 7000;
    public SmallAsteroid[] smallAsteroids;

    private float speed;
    private int asteroidHp = 3;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {

        this.transform.eulerAngles = new Vector3 (0f, 0f, Random.value * 360f);
        /*this.transform.localScale = Vector3.one * size;*/

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
            asteroidHp--;
        }

        if(collision.gameObject.tag == "Bullet" && asteroidHp == 0)
        {
            asteroidHp = 3;
            Destroy(this.gameObject);
            PlayerManager.score += 200;

            transform.position = this.transform.position + new Vector3(-0.8f, 0.8f);
            Instantiate(smallAsteroids[0], transform.position, Quaternion.identity);

            transform.position = this.transform.position + new Vector3(0.8f, 0.8f);
            Instantiate(smallAsteroids[1], transform.position, Quaternion.identity);

            transform.position = this.transform.position + new Vector3(0, -1.8f);
            Instantiate(smallAsteroids[2], transform.position, Quaternion.identity);

            transform.position = this.transform.position + new Vector3(0.5f, 1f);
            Instantiate(smallAsteroids[3], transform.position, Quaternion.identity);


        }
    }

}
