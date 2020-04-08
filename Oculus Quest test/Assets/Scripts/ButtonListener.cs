using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;

public class ButtonListener : MonoBehaviour
{

    public UnityEvent proximityevent;
    public UnityEvent contactevent;
    public UnityEvent actionevent;
    public UnityEvent defaultevent;

    public OVRHand lefthand;
    public OVRHand righthand;

    public Material cur;
    public Material[] materials;


    bool eventtrigger;
    bool boxtrigger;

    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitialEvent);
        cur = materials[0];
        eventtrigger = true;
    }

    void InitialEvent(InteractableStateArgs state)
    {
        //print(state.Interactable);
        if (state.NewInteractableState == InteractableState.ProximityState)
        {
            //GetComponent<MeshRenderer>().material = cur;
            boxtrigger = true;
        } else if (state.NewInteractableState == InteractableState.ContactState)
        {
            //GetComponent<MeshRenderer>().material = cur;
            boxtrigger = true;
            /*if (righthand.GetFingerIsPinching(OVRHand.HandFinger.Index) && righthand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.8)
            {
                GetComponent<Transform>().position = righthand.GetComponent<Transform>().position;
            }
            if (lefthand.GetFingerIsPinching(OVRHand.HandFinger.Index) && lefthand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.8)
            {
                GetComponent<Transform>().position = lefthand.GetComponent<Transform>().position;
            }*/

        } else if (state.NewInteractableState == InteractableState.ActionState)
        {
            actionevent.Invoke();
        } else
        {
            defaultevent.Invoke();
            boxtrigger = false;
            cur = materials[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isIndexFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool isMiddleFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        bool isRingFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool isPinkyFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);

        bool isRIndexFingerPinching = righthand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool isRMiddleFingerPinching = righthand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        bool isRRingFingerPinching = righthand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool isRPinkyFingerPinching = righthand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);

        if ( (isIndexFingerPinching || isRIndexFingerPinching) && eventtrigger)
        {
            cur = materials[1];
            eventtrigger = false;
        }
        else if ( (isMiddleFingerPinching || isRMiddleFingerPinching) && eventtrigger)
        {
            cur = materials[2];
            eventtrigger = false;
        }
        else if ( (isRingFingerPinching || isRRingFingerPinching) && eventtrigger)
        {
            cur = materials[3];
            eventtrigger = false;
        }
        else if ( (isPinkyFingerPinching || isRPinkyFingerPinching) && eventtrigger)
        {
            cur = materials[4];
            eventtrigger = false;
        }
        if (!eventtrigger && !isIndexFingerPinching && !isMiddleFingerPinching && !isRingFingerPinching && !isPinkyFingerPinching &&
            !isRIndexFingerPinching && !isRMiddleFingerPinching && !isRRingFingerPinching && !isRPinkyFingerPinching)
        {
            eventtrigger = true;
        }

        if (boxtrigger)
        {
            GetComponent<MeshRenderer>().material = cur;
        }
    }
}
