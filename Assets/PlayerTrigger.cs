using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Gun gun;

    void Start()
    {
        if(gun == null)
        {
            Debug.LogError("Gotta add the gun silly");
            gun = GameObject.Find("Gun").GetComponent<Gun>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ammo Pickup"))
        {
            gun.GetAmmo();
            Destroy(other.gameObject);
        }
    }
}
