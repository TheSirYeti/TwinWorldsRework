using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("HorizontalK"), 0, Input.GetAxis("VerticalK")) * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventManager.Trigger("SeeObject", Vector3.zero, 3f);
        }
    }
}
