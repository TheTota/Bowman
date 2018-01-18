using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private bool shot;

    public bool Shot
    {
        get { return shot; }
        set { shot = value; }
    }


    private void Start()
    {
        shot = false;
    }

    private void Update()
    {
        if (shot)
        {
            Debug.Log("Arrow's flying !!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision avec " + collision.GetComponent<Collider>().tag);
    }

}
