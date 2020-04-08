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

    public Material cur;
    public Material[] materials;

    bool eventtrigger;

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
        if (state.NewInteractableState == InteractableState.ProximityState)
        {
            GetComponent<MeshRenderer>().material = cur;
        } else if (state.NewInteractableState == InteractableState.ContactState)
        {
            GetComponent<MeshRenderer>().material = cur;
        } else if (state.NewInteractableState == InteractableState.ActionState)
        {
            actionevent.Invoke();
        } else
        {
            defaultevent.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isIndexFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool isMiddleFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        bool isRingFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool isPinkyFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);

        if (isIndexFingerPinching && eventtrigger)
        {
            cur = materials[1];
            eventtrigger = false;
        }
        else if (isMiddleFingerPinching && eventtrigger)
        {
            cur = materials[2];
            eventtrigger = false;
        }
        else if (isRingFingerPinching && eventtrigger)
        {
            cur = materials[3];
            eventtrigger = false;
        }
        else if (isPinkyFingerPinching && eventtrigger)
        {
            cur = materials[4];
            eventtrigger = false;
        }
        if (!eventtrigger && !isIndexFingerPinching && !isMiddleFingerPinching && !isRingFingerPinching && !isPinkyFingerPinching)
        {
            eventtrigger = true;
        }
    }
}
