using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    public AnimationCurve curve;
    public float resetInterval = 3, hangTime = 2, resetTimer = 5;

    public bool randomize = true;

    Rigidbody rb;
    Vector3 startPosition;
    Quaternion startRotation;
    bool platformIsActice = false;

    // Start is called before the first frame update
    void Start()
    {
        if(randomize)
        {
            this.transform.Translate(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);
        }

        rb = this.GetComponent<Rigidbody>();
        startPosition = this.transform.position;
        startRotation = this.transform.rotation;

        Randomize();

    }

    void Randomize()
    {
        if (randomize)
        {
            resetInterval += Random.Range(-resetInterval / 3, resetInterval / 3);
            hangTime += Random.Range(-hangTime / 3, hangTime / 3);
            resetTimer += Random.Range(-resetTimer / 3, resetTimer / 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " has ran into us");
        if(other.gameObject.CompareTag("Player"))
        StartCoroutine(WaitToFall()); 

    }

    IEnumerator WaitToFall()
    {
        if (!platformIsActice)
        {
            platformIsActice = true;
            yield return new WaitForSeconds(hangTime);
            rb.isKinematic = false;
            StartCoroutine(ResetPositon());
        }
    }

    IEnumerator ResetPositon()
    {
        yield return new WaitForSeconds(resetTimer);
        rb.isKinematic = true;

        Vector3 pointB = startPosition;
        Vector3 pointA = this.transform.position;


        Quaternion rotA = this.transform.rotation;
        Quaternion rotB = startRotation;

        float timer = 0;

        while(timer < 1)
        {
            this.transform.position = Vector3.Lerp(pointA, pointB, curve.Evaluate(timer));
            this.transform.rotation = Quaternion.Lerp(rotA, rotB, curve.Evaluate(timer));
            timer += Time.deltaTime / resetInterval;
            yield return null;
        }

        this.transform.position = startPosition;
        this.transform.rotation = startRotation;

        platformIsActice = false;

        Randomize();
    }
}
