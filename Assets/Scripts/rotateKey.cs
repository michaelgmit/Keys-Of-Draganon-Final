using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateKey : MonoBehaviour
{

    public int rotateSpeed;
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);

    }

}