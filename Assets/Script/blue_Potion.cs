using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_Potion : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0, Space.Self);
        if (GetComponentInParent<search_check>().b_check == false)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
