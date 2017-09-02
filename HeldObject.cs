using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This script is added to everything you want to be holdable

[RequireComponent(typeof(Rigidbody))]
public class HeldObject : MonoBehaviour {

    
    [HideInInspector]
    public Controller parent; // Tells the held object which input device it is being held by (ie which controller)

    public bool dropOnRelease; // Variable that lets you choose whether you drop the object when you release the pickup button or drop it by pressing another button

    public UnityEvent pickUp; // Adds events in the inspector which will enable us to add objects and call functions on those objects

    public UnityEvent drop; // Adds events in the inspector which will enable us to add objects and call functions on those objects

    public void PickUp()
    {
        pickUp.Invoke();
        if (pickUp.GetPersistentEventCount() == 0) // Checks how many listeners you've added -- if zero, runs the default
        {
            DefaultPickUp();
        }
    }

    public void Drop()
    {
        drop.Invoke();
        if (drop.GetPersistentEventCount() == 0) // Checks how many listeners you've added -- if zero, runs the default
        {
            DefaultDrop();
        }
        parent = null;
    }

    public void DefaultDrop()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = parent.GetComponent<Hand>().simulator.velocity; // Give the object the velocity it had before it was released
        parent = null; // Allows the object to be picked up again
    }

    public void DefaultPickUp()
    {
        transform.parent = parent.transform; // Sets the object's position to the controller - allows you to move it around
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
