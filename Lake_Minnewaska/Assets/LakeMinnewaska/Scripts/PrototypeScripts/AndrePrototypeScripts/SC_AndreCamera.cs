using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class SC_AndreCamera : MonoBehaviour
{
    float curRotX;
    float curRotY;

    private bool inFishingMode;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        curRotX = transform.rotation.x;
        curRotY = transform.rotation.y;
        //Cursor.lockState = CursorLockMode.Locked;
        inFishingMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inFishingMode)
        {
            transform.rotation = Quaternion.Euler(30, 90, 0);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            float mouseY = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 500;
            float mouseX = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 500;

            curRotX -= mouseX;
            curRotY += mouseY;
            curRotX = Mathf.Clamp(curRotX, -90.0f, 90.0f);

            transform.rotation = Quaternion.Euler(curRotX, curRotY, 0);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void startFishing()
    {
        inFishingMode = true;
    }

    public void stopFishing()
    {
        inFishingMode = false;
    }
}
