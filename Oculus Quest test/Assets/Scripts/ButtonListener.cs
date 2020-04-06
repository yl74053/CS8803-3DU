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

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitialEvent);
    }

    void InitialEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ProximityState)
        {
            proximityevent.Invoke();
        } else if (state.NewInteractableState == InteractableState.ContactState)
        {
            contactevent.Invoke();
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
        
    }
}
