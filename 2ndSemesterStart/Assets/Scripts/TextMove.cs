using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y =  Input.GetAxisRaw("Vertical");
        Vector3 inputvec = new Vector3(x, y, 0);
        transform.Translate(inputvec * playerSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.eulerAngles += new Vector3(0, 0, -5);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.eulerAngles += new Vector3(0, 0, 5);
        }

        Vector2 wheelInput2 = Input.mouseScrollDelta;
        if (wheelInput2.y > 0)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        else if (wheelInput2.y < 0)
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }

    }
}
