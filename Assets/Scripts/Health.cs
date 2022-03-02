using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;

    //[Tooltip]("Check this is a object is a object and not and enemy");
    public bool isObject = false;

    public AudioClip death;

    private AudioSource aud;

    void Start()
    {
        aud = this.gameObject.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //    Debug.Log("Magnitude" + other.relativeVelocity.magnitude);
            //    health -=(int)(other.relativeVelocity.magnitude * 0.05f);

            health -= other.gameObject.GetComponent<Bullet>().damage;
        }
        if(health <= 0)
        {
            Death();
        }
    }
    
    void Death()
    {
        if (isObject)
        {
            Destroy(this.gameObject);
            for(int i = 0; i < 4; i++)
            {
                GameObject part = GameObject.CreatePrimitive(PrimitiveType.Cube);
                part.transform.localScale = Vector3.one * Random.Range(0.5f, 0.8f);
                part.transform.position = this.transform.position;
                part.transform.Translate(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f),Random.Range (-.5f, .5f));
                part.AddComponent<Rigidbody>();
            }
        }else
        {
            this.gameObject.AddComponent<Rigidbody>();
            Destroy(this.gameObject, 1);
            aud.PlayOneShot(death);
        }
    }
}
