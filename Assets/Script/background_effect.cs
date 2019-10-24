using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_effect : MonoBehaviour
{
    bool turn = true;
    float loop = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turn ==true)
        {
            loop += Time.deltaTime; 
            gameObject.transform.Translate(new Vector3(0.001f, 0, 0));

            if (loop>3)
            {
                loop = 0;
                turn = false;
            }
        }
        else
        {
            loop += Time.deltaTime;
            gameObject.transform.Translate(new Vector3(-0.001f, 0, 0));

            if (loop > 3)
            {
                loop = 0;
                turn = true;
            }
        }



    }
}
