using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private int animationFrame;

    public float explosionLife = 0.4f;
    public float animationTime = 0.10f;
    public Sprite[] animationSprites;
    
    void Start()
    {
        InvokeRepeating(nameof(AnimationExplosion), this.animationFrame, this.animationTime);
        Destroy(this.gameObject, explosionLife);
    }

    private void AnimationExplosion()
    {
        animationFrame++;
        GetComponent<SpriteRenderer>().sprite = animationSprites[animationFrame];
    }

}
