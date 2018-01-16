using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerShoot : MonoBehaviour {

    public GameObject weaponMesh;
    
    private Animator animator;

	// Use this for initialization
	void Start () {
        this.animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!Input.GetButtonDown("Fire1"))
        {
            OrientationArme();
        }
        else
        {
            animator.SetFloat("Force", Mathf.Clamp01(BanderArc()));
        }

        // Relache le btn tir -> tirer
        if (Input.GetButtonUp("Fire1"))
        {
            Tirer();
            animator.SetFloat("Force", 0f);
        }
    }

    private float BanderArc()
    {
        // TODO EVENTUALLY: Calculer le swipe pour bander progressivement l'arc

        return 1f;
    }

    private void Tirer()
    {
        // TODO: Shoot the arrow!

        Debug.Log("Shooting!");
    }

    /// <summary>
    /// Rotation de l'arme vers le curseur de la souris.
    /// </summary>
    private void OrientationArme()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.weaponMesh.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        this.weaponMesh.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 94f);
    }
}
