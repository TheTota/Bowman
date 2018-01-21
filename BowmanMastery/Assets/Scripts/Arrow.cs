using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour {
    private bool readyToBeShot;   

    public bool ReadyToBeShot
    {
        get { return readyToBeShot; }
        set { readyToBeShot = value; }
    }

    private float shotForce;

    public float ShotForce
    {
        get { return shotForce; }
        set { shotForce = value; }
    }


    private Rigidbody2D rb;
    private bool shot;
    private bool hasHit;

    private void Start()
    {
        this.readyToBeShot = false;
        this.hasHit = false;
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (readyToBeShot && !shot)
        {
            ShootArrow();
        }

        if (shot && !hasHit)
        {
            Vector2 v = this.rb.velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
    }

    private void ShootArrow()
    {
        this.shot = true;

        // Indépendance de l'arrow
        this.transform.SetParent(null);

        // Tir
        this.rb.bodyType = RigidbodyType2D.Dynamic;
        this.rb.AddForce(this.transform.up * shotForce, ForceMode2D.Impulse); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.hasHit = true;
        this.rb.velocity = Vector2.zero;
        this.rb.angularVelocity = 0f;
        this.rb.bodyType = RigidbodyType2D.Kinematic;

        if (collision.tag == "Target")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(.3f, .7f, 0f);
        }
    }    
}
