using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SC_AndreCast : MonoBehaviour
{
    private float maxAngle;
    private float curAngle;
    private float curPower;
    private float maxPower;
    private bool hasCast;
    private bool inFishingMode;

    private List<GameObject> fishies;


    public GameObject hook;
    // Start is called before the first frame update
    void Start()
    {
        maxAngle = 75.0f;
        maxPower = 200.0f;
        hasCast = false;
        curAngle = 0.0f;
        curPower = 0.0f;
        hook = GameObject.FindGameObjectWithTag("Hook");
        inFishingMode = false;
        fishies = GameObject.FindGameObjectsWithTag("Fish").ToList();

    }

    // Update is called once per frame
    void Update()
    {
        if (!inFishingMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hasCast)
                {
                    bool shouldReturn = true;
                    foreach (GameObject fish in fishies)
                    {
                        if (Vector3.Distance(fish.transform.position, hook.transform.position) < 1)
                        {
                            shouldReturn = false;
                        }
                    }

                    if (shouldReturn)
                    {
                        curAngle = 0.0f;
                        curPower = 0.0f;
                        hasCast = false;
                        hook.transform.position = new Vector3(0.0f, 0.6f, 0.0f);
                        hook.transform.rotation = Quaternion.identity;
                        hook.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                }
            }

            if (Input.GetMouseButton(0))
            {
                curAngle += Time.deltaTime / 4;
                curPower += Time.deltaTime * 4;
                if (curAngle > maxAngle)
                {
                    curAngle = maxAngle;
                }

                if (curPower > maxPower)
                {
                    curPower = maxPower;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                //cast
                //hook.transform.eulerAngles = new Vector3(curAngle, transform.eulerAngles.x, 0.0f);
                hook.GetComponent<Rigidbody>().velocity =
                    (Vector3.Scale(transform.forward, new Vector3(1, 0, 1)) + new Vector3(0, curAngle, 0)) * curPower;
                curAngle = 0.0f;
                hasCast = true;
            }
        }
    }

    public void startFishing()
    {
        inFishingMode = true;
    }

    public void stopFishing()
    {
        inFishingMode = false;
        hasCast = false;
    }
}

