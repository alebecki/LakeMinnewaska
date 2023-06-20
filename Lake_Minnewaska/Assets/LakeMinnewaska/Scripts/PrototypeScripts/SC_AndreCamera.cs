using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class SC_AndreCamera : MonoBehaviour
{
    float curRotX;
    float curRotY;
    // Start is called before the first frame update
    void Start()
    {
        curRotX = transform.rotation.x;
        curRotY = transform.rotation.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 1000;
        float mouseX = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 1000;

        curRotX -= mouseX;
        curRotY += mouseY;
        curRotX = Mathf.Clamp(curRotX, -90.0f, 90.0f);

        transform.rotation = Quaternion.Euler(curRotX, curRotY, 0);
    }
}
