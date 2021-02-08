using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor() // Using ray casts to move about world
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray is shot from origin to point within main camera
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit); 
        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point; //calls the position of the ray cast hit and moves to it
        }
    }
}
