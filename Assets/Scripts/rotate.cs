using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public int rotateSpeed;
    void Update()
    {
        // Rotate the object around its local y axis at 1 degree per second
        transform.Rotate(Vector3.up * Time.deltaTime);
    }
}
