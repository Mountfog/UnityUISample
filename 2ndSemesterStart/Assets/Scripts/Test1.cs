using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "Hello World!";
        Debug.Log("Hello World!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
