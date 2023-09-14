using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    public AudioSource shotSound;
    public AudioSource explosionSound;
    public GameObject ExplosionPrefab;
    public float bulletLife = 5f;
    public float speed = 15f;
    public float animationTime = 0.05f;
    public Sprite[] animationSprites;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        shotSound.Play();
        InvokeRepeating(nameof(AnimationSpriteShoot), this.animationTime, this.animationTime);
    }

    public void Project(Vector2 direction)
    {
        rb.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.bulletLife);
    }

    public void AnimationSpriteShoot()
    {
        animationFrame++;
        GetComponent<SpriteRenderer>().sprite = this.animationSprites[animationFrame];
    }

    private void createExplosion()
    {
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        explosionSound.Play();
        spriteRenderer.enabled = false;
        Destroy(this.gameObject, 2f);
        createExplosion();  
    }
}
