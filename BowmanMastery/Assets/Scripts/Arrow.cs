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

    private Rigidbody2D rb;
    private bool shot;

    private void Start()
    {
        readyToBeShot = false;
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (readyToBeShot && !shot)
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        this.shot = true;

        // Indépendance de l'arrow
        this.transform.SetParent(null);

        // Tir
        this.rb.bodyType = RigidbodyType2D.Dynamic;
        this.rb.AddForce(this.transform.up * 13f, ForceMode2D.Impulse); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.rb.velocity = Vector3.zero;
        this.rb.bodyType = RigidbodyType2D.Kinematic;

        if (collision.tag == "Target")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(.3f, .7f, 0f);
        }
    }

}
