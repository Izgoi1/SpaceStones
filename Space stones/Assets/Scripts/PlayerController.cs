using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject engine;
    [SerializeField] public GameObject bulletPosition;

    public Vector2 movement;
    public Rigidbody2D rb;
    public float speed = 4;
    public float rotSpeed = 180f;
    public float animationTime = 15f;
    public Sprite[] animationSprites;
    public BulletScript bulletPrefab;

    private int animationFrame;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        InvokeRepeating(nameof(AnimationSpriteMove), this.animationTime, this.animationTime);
    }

    void Update()
    {


        Quaternion rot = transform.rotation;
        float z = rot.eulerAngles.z;
        z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        rot = Quaternion.Euler(0, 0, z);
        transform.rotation = rot;

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(this.transform.up * speed);
            AnimationSpriteMove();
        }
        else
        {
            engine.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }


        if (transform.position.x < -8)
        {
            transform.position = new Vector3(transform.position.x + 16f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 8)
        {
            transform.position = new Vector3(transform.position.x - 16f, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
        }
        else if (transform.position.y > 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);
        }
    }
    

    private void Shoot()
    {
        BulletScript bullet = Instantiate(this.bulletPrefab, bulletPosition.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
        bullet.AnimationSpriteShoot();
    }

    private void AnimationSpriteMove()
    {
        engine.gameObject.SetActive(true);
        animationFrame++;

        if (animationFrame >= this.animationSprites.Length)
        {
            animationFrame = 0;
        }

        engine.GetComponent<SpriteRenderer>().sprite = this.animationSprites[animationFrame];
    }


}
