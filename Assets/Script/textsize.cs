using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textsize : MonoBehaviour
{
    public TextMeshPro text;
    public float max;
    public float min;
    public float speed;
    bool tick = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text = GetComponent<TextMeshPro>();
        if (tick == true)
        {
            if (text.fontSize < max)
                text.fontSize += speed*Time.deltaTime;
            else tick = false;
        }
        if (tick ==false)
        {
            if (text.fontSize > min)
                text.fontSize -= speed*Time.deltaTime;
            else tick = true;
        }



    }
}
