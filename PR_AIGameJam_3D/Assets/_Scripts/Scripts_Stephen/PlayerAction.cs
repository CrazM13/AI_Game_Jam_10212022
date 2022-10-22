using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Transform cam;
    public float playerActivateDistance;
    bool active = false;
    

      
       public  void Update()
        {
            RaycastHit hit;
            active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance);

            if (Input.GetKeyDown(KeyCode.F) && active == true)
            {
                if (hit.transform.GetComponentInChildren<Animator>() != null) 
                {
                    hit.transform.GetComponentInChildren<Animator>().SetTrigger("Activate");
                }

            }
                
        }

}

