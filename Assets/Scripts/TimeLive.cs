using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLive : MonoBehaviour
{
    public float timeLive;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeLive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
