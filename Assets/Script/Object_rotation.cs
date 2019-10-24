using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 100*Time.deltaTime, 0, Space.Self);
        if (GetComponentInParent<search_check>().r_check == false)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
