using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 8.5f;

    private void FixedUpdate()
    {
        Vector3 pos = this.transform.position;
        Vector3 cameraPos = Camera.main.transform.position;
        if (Input.GetKey("w"))
        {
            pos.z += speed * Time.deltaTime;
            cameraPos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= speed * Time.deltaTime;
            cameraPos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
            cameraPos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
            cameraPos.x -= speed * Time.deltaTime;
        }
        this.transform.position = pos;
        Camera.main.transform.position = cameraPos;
    }
}
