using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 8.5f;
    [SerializeField] private float rotationSpeed = 250f;
    [SerializeField] GameObject[] Crosses;
    [SerializeField] private Transform MaxX;
    [SerializeField] private Transform MinX;
    [SerializeField] private Transform MaxZ;
    [SerializeField] private Transform MinZ;
    private Vector3 pos;
    private Vector3 cameraPos;
    private Vector3 startingCamPos;

    private void Start()
    {
        startingCamPos = Camera.main.transform.position;
        Crosses = GameObject.FindGameObjectsWithTag("Cross");
        for(int i=0; i < Crosses.Length; i++)
        {
            if(Crosses[i].transform.position.x > 0)
            {
                MaxX = Crosses[i].transform;
            }
            else if (Crosses[i].transform.position.x < 0)
            {
                MinX = Crosses[i].transform;
            }
            else if (Crosses[i].transform.position.z > 0)
            {
                MaxZ = Crosses[i].transform;
            }
            else if (Crosses[i].transform.position.z < 0)
            {
                MinZ = Crosses[i].transform;
            }
        }
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        Vector3 cameraPos = Camera.main.transform.position;

        if (pos.z > MaxZ.position.z)
        {
            pos.z = MaxZ.position.z;
            cameraPos.z = MaxZ.position.z + startingCamPos.z;
        }
        else if (Input.GetKey("w")&&(pos.z < MaxZ.position.z))
        {
            if (Input.GetKey("d"))
            {
                pos.z += speed / 2 * Time.deltaTime;
                pos.x += speed / 2 * Time.deltaTime;
                cameraPos.x += speed / 2 * Time.deltaTime;
                cameraPos.z += speed / 2 * Time.deltaTime;
                Vector3 target = pos - transform.position;
                Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey("a"))
            {
                pos.z += speed / 2 * Time.deltaTime;
                pos.x -= speed / 2 * Time.deltaTime;
                cameraPos.x -= speed / 2 * Time.deltaTime;
                cameraPos.z += speed / 2 * Time.deltaTime;
                Vector3 target = pos - transform.position;
                Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
            }
            else
            {
                pos.z += speed * Time.deltaTime;
                cameraPos.z += speed * Time.deltaTime;
                Vector3 target = pos - transform.position;
                Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
            }
        }
        if(pos.z < MinZ.position.z)
        {
            pos.z = MinZ.position.z;
            cameraPos.z = MinZ.position.z + startingCamPos.z;
        }
        else if (Input.GetKey("s")&&(pos.z>MinZ.position.z))
        {
            if (Input.GetKey("d"))
            {
                pos.z -= speed / 2 * Time.deltaTime;
                pos.x += speed / 2 * Time.deltaTime;
                cameraPos.x += speed / 2 * Time.deltaTime;
                cameraPos.z -= speed / 2 * Time.deltaTime;
                Vector3 target = pos - transform.position;
                Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey("a"))
            {
                pos.z -= speed / 2 * Time.deltaTime;
                pos.x -= speed / 2 * Time.deltaTime;
                cameraPos.x -= speed / 2 * Time.deltaTime;
                cameraPos.z -= speed/2 * Time.deltaTime;
                Vector3 target = pos - transform.position;
                Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
            }
            else
            {
                pos.z -= speed * Time.deltaTime;
                cameraPos.z -= speed * Time.deltaTime;
                Vector3 target = pos - transform.position;
                Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
            }
        }
        if(pos.x > MaxX.position.x)
        {
            pos.x = MaxX.position.x;
            cameraPos.x = MaxX.position.x + startingCamPos.x;
        }
        else if (Input.GetKey("d")&&(pos.x < MaxX.position.x))
        {
            pos.x += speed * Time.deltaTime;
            cameraPos.x += speed * Time.deltaTime;
            Vector3 target = pos - transform.position;
            Quaternion toRotate = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
        if(pos.x < MinX.position.x)
        {
            pos.x = MinX.position.x;
            cameraPos.x = MinX.position.x + startingCamPos.x;
        }
        else if (Input.GetKey("a")&&(pos.x>MinX.position.x))
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
