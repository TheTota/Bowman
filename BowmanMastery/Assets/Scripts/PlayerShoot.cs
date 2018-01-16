using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject weaponMesh;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        OrientationArme();
    }

    /// <summary>
    /// Rotation de l'arme vers le curseur de la souris.
    /// </summary>
    private void OrientationArme()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weaponMesh.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        weaponMesh.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 94f);
    }
}
