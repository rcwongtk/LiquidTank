using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

public class LockInPlace : MonoBehaviour
{
    public GameObject tankSystem;
    public GameObject variableMeasurement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LockPosition()
    {
        // First lock the position of the system in place my removing the
        // Interaction capabilities.
        tankSystem.GetComponent<NearInteractionGrabbable>().enabled = false;
        tankSystem.GetComponent<ObjectManipulator>().enabled = false;
        tankSystem.GetComponent<BoundingBox>().enabled = false;
        tankSystem.GetComponent<BoxCollider>().enabled = false;

        // Now Enable the Variable Measurement interaction
        variableMeasurement.GetComponent<NearInteractionGrabbable>().enabled = true;
        variableMeasurement.GetComponent<ObjectManipulator>().enabled = true;
        variableMeasurement.GetComponent<BoundingBox>().enabled = true;
    }

    public void UnlockPosition()
    {
        // Reverse actions of LockPosition()
        tankSystem.GetComponent<NearInteractionGrabbable>().enabled = true;
        tankSystem.GetComponent<ObjectManipulator>().enabled = true;
        tankSystem.GetComponent<BoundingBox>().enabled = true;
        tankSystem.GetComponent<BoxCollider>().enabled = true;

        variableMeasurement.GetComponent<NearInteractionGrabbable>().enabled = false;
        variableMeasurement.GetComponent<ObjectManipulator>().enabled = false;
        variableMeasurement.GetComponent<BoundingBox>().enabled = false;

    }

}
