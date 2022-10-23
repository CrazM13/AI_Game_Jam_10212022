using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 8.5f;
    [SerializeField] private float rotationSpeed = 205f;

    private void Update()
    {
        Vector3 pos = this.transform.position;
        Vector3 cameraPos = Camera.main.transform.position;
        Quaternion toRotate = Quaternion.LookRotation(pos, Vector3.up);

        if (Input.GetKey("w"))
        {
            pos.z += speed * Time.deltaTime;
            cameraPos.z += speed * Time.deltaTime;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, toRotate, rotationSpeed * 10 * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            pos.z -= speed * Time.deltaTime;
            cameraPos.z -= speed * Time.deltaTime;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, toRotate, rotationSpeed * 10 * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
            cameraPos.x += speed * Time.deltaTime;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, toRotate, rotationSpeed * 10 * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
            cameraPos.x -= speed * Time.deltaTime;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, toRotate, rotationSpeed *10* Time.deltaTime);
        }
        this.transform.position = pos;
        Camera.main.transform.position = cameraPos;
    }
}
