using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    
    public bool debug = false;

    [Range(10,100)]
    public float bulletSpeed = 50;

    public Transform bulletSpawn;

    public int totalAmmo = 30;
    public int clipSize = 10;
    public int clip = 0;

   public void Reload()
        {
        if(clip == clipSize)
        {
            if (debug) Debug.Log("Clip is already full");
            return;
        }

        if(totalAmmo + clip >= clipSize)
        {
            totalAmmo -= (clipSize - clip);
            clip = clipSize;
        }
        else
        {
            clip = (totalAmmo + clip);
            totalAmmo = 0;
        }
    }

   public void Fire()
    {

        if (clip > 0)
        {
            if (debug) Debug.Log("POW!");

            clip -= 1;
            Rigidbody bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            bullet.transform.Translate(0, 0, 1);

            bullet.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        } else
        {
            if (debug) Debug.Log("Out of Ammo!");
        }
    }
}
