using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SC_AndreFish : MonoBehaviour
{
    private float interestedDist;
    private float lerpTime;
    private float stamina;
    public bool theGuy;
    private bool inFishingMode;
    private GameObject hook;
    private GameObject player;
    private float switchTimer;
    private bool isRight;
    private bool isTired;

    private List<GameObject> fishies;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        lerpTime = 0.0f;
        interestedDist = 2.0f;
        startPos = transform.position;
        hook = GameObject.FindGameObjectWithTag("Hook");
        player = GameObject.FindGameObjectWithTag("Player");
        fishies = GameObject.FindGameObjectsWithTag("Fish").ToList();
        theGuy = true;
        inFishingMode = false;
        switchTimer = 1.0f;
        isRight = true;
        isTired = false;
        stamina = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inFishingMode && Input.GetMouseButtonDown(0))
        {
            foreach (GameObject fish in fishies)
            {
                fish.GetComponent<SC_AndreFish>().theGuy = true;
            }
        }
        if(Vector3.Distance(transform.position, hook.transform.position) < interestedDist && !inFishingMode)
        {
            foreach (GameObject fish in fishies)
            {
                if (Vector3.Distance(fish.transform.position, hook.transform.position) < Vector3.Distance(transform.position, hook.transform.position))
                {
                    theGuy = false;
                }
            }

            if (theGuy)
            {
                transform.position = Vector3.Lerp(startPos, hook.transform.position, lerpTime / 5);
                lerpTime += Time.deltaTime;
            }
        }
        else if (!inFishingMode)
        {
            startPos = transform.position;
            lerpTime = 0.0f;
            theGuy = true;
        }

        if (Input.GetMouseButtonDown(0) && Vector3.Distance(transform.position, hook.transform.position) < 1)
        {
            inFishingMode = true;
            player.GetComponent<SC_AndreCast>().startFishing();
            player.GetComponent<SC_AndreCamera>().startFishing();
        }
        

        if (inFishingMode && theGuy)
        {
            hook.transform.position = transform.position;
            switchTimer -= Time.deltaTime;
            if (switchTimer < 0)
            {
                switchTimer = 2;
                isRight = (Random.value >= 0.5);
            }

            if (Input.GetMouseButton(0))
            {
                if (isTired)
                {
                    transform.position += (player.transform.position - transform.position).normalized * Time.deltaTime;
                }
                else
                {
                    if ((Input.mousePosition.x > Screen.width / 2f && !isRight) ||
                        (Input.mousePosition.x < Screen.width / 2f && isRight))
                    {
                        stamina -= Time.deltaTime;
                        if (stamina < 0)
                        {
                            isTired = true;
                        }
                    }
                    else if (isRight)
                    {
                        transform.position += (Vector3.back * Time.deltaTime);
                    }
                    else
                    {
                        transform.position += (Vector3.forward * Time.deltaTime);
                    }
                }
            }
            else if (isRight)
            {
                transform.position += (Vector3.back * Time.deltaTime);
            }
            else
            {
                transform.position += (Vector3.forward * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, player.transform.position) < 1)
            {
                inFishingMode = false;
                player.GetComponent<SC_AndreCast>().stopFishing();
                player.GetComponent<SC_AndreCamera>().stopFishing();
                foreach (GameObject fish in fishies)
                {
                    if (fish != this.gameObject)
                    {
                        fish.GetComponent<SC_AndreFish>().theGuy = true;
                    }
                }
                hook.transform.position = player.transform.position;
                this.gameObject.SetActive(false);
            }
        }
    }
}
