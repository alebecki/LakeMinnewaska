using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public enum TriggerState
    {
        None,
        Rod,
        Storage,
        Driving
    }

    public TriggerState triggerState;
    public CinemachineVirtualCamera playerCam, rodCam, storageCam, drivingCam;
    public bool inFPS = true;

    private void TransitionCam(CinemachineVirtualCamera from, CinemachineVirtualCamera to)
    {
        from.Priority = 0;
        to.Priority = 1;
        inFPS = !inFPS;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (triggerState)
            {
                case TriggerState.None:
                    break;
                case TriggerState.Rod:
                    if (inFPS)
                        TransitionCam(playerCam, rodCam);
                    else
                        TransitionCam(rodCam, playerCam);
                    break;
                case TriggerState.Storage:
                    if (inFPS)
                        TransitionCam(playerCam, storageCam);
                    else
                        TransitionCam(storageCam, playerCam);
                    break;
                case TriggerState.Driving:
                    if (inFPS)
                        TransitionCam(playerCam, drivingCam);
                    else
                        TransitionCam(drivingCam, playerCam);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rod"))
        {
            triggerState = TriggerState.Rod;
        }
        if (other.CompareTag("Storage"))
        {
            triggerState = TriggerState.Storage;
        }
        if (other.CompareTag("Driving"))
        {
            triggerState = TriggerState.Driving;
        }
    }
}
