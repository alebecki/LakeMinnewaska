using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AndreFish : MonoBehaviour
{
    private float interestedDist;
    private float lerpTime;
    private GameObject hook;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        lerpTime = 0.0f;
        interestedDist = 2.0f;
        startPos = transform.position;
        hook = GameObject.FindGameObjectWithTag("Hook");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, hook.transform.position) < interestedDist)
        {
            transform.position = Vector3.Lerp(startPos, hook.transform.position, lerpTime / 5);
            lerpTime += Time.deltaTime;
        }
        else
        {
            startPos = transform.position;
            lerpTime = 0.0f;
        }
    }
}
