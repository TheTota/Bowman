using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerShoot : MonoBehaviour
{

    public GameObject weaponMesh;

    public Arrow arrowPrefab;
    public Transform arrowHolder;

    private Animator animator;

    private Arrow arrowEquipee;

    private bool hasStartedShooting;
    private bool hasUneArrowEquipee;

    private int shotForce;

    // Use this for initialization
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.EquiperFleche();
    }

    // Update is called once per frame
    void Update()
    {
        OrientationArme();

        if (Input.GetButton("Fire1"))
        {
            // Si on vient de commencer le tir, on lance l'animation de tir
            if (!hasStartedShooting && hasUneArrowEquipee)
            {
                this.shotForce = 0;
                this.animator.Play("Shoot");
                hasStartedShooting = true;
            }

            if (hasStartedShooting)
            {
                this.shotForce++;
            }
        }

        // Relacher le btn tir -> tirer
        if (Input.GetButtonUp("Fire1"))
        {
            if (hasStartedShooting)
            {
                // On tire la flèche et stop l'animation de tir
                Tirer();
                this.animator.Play("Idle");
                hasStartedShooting = false;
            }
        }
    }

    /// <summary>
    /// Equipe une flèche en l'instanciant.
    /// Sauvegarde une référence à la flèche dans la variable flecheEquipee.
    /// </summary>
    private void EquiperFleche()
    {
        this.arrowEquipee = Instantiate<Arrow>(arrowPrefab, arrowHolder.position, arrowHolder.rotation, arrowHolder);
        this.hasUneArrowEquipee = true;
    }

    /// <summary>
    /// Tir de la flèche équipée.
    /// </summary>
    private void Tirer()
    {
        if (this.arrowEquipee)
        {
            this.arrowEquipee.ReadyToBeShot = true;
            this.arrowEquipee.ShotForce = Mathf.Clamp(this.shotForce / 2f, 1f, 20f);
            this.hasUneArrowEquipee = false;
            StartCoroutine(PreparerEquiperFleche());
        }
        else
        {
            throw new Exception("Aucune flèche à tirer.");
        }
    }

    /// <summary>
    /// Equipe la flèche aprèx X secondes.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PreparerEquiperFleche()
    {
        yield return new WaitForSeconds(1f);
        this.EquiperFleche();
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
