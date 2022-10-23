using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 8.5f;
    [SerializeField] private float rotationSpeed = 250f;

    private void Update()
    {
        Vector3 pos = transform.position;
        Vector3 cameraPos = Camera.main.transform.position;

        if (Input.GetKey("w"))
        {
            pos.z += speed * Time.deltaTime;
            cameraPos.z += speed * Time.deltaTime;
            Vector3 target = pos - transform.position;
            Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            pos.z -= speed * Time.deltaTime;
            cameraPos.z -= speed * Time.deltaTime;
            Vector3 target = pos - transform.position;
            Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
            cameraPos.x += speed * Time.deltaTime;
            Vector3 target = pos - transform.position;
            Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
            cameraPos.x -= speed * Time.deltaTime;
            Vector3 target = pos - transform.position;
            Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
        transform.position = pos;
        Camera.main.transform.position = cameraPos;
    }
}
