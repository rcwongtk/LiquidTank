using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepWithinScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // x1 = highest y-Value (12.5)
        // x2 = lowest y-Value (-1.25)
        
        if(transform.localPosition.y > 12.5)
        {
            gameObject.transform.localPosition = new Vector3(transform.localPosition.x, 12.5f, transform.localPosition.z);
        }
        else if (transform.localPosition.y < -1.25)
        {
            gameObject.transform.localPosition = new Vector3(transform.localPosition.x, -1.25f, transform.localPosition.z);
        }

    }
}
