using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collector : MonoBehaviour
{
    public Component Doorcollider;
    public static int Keys;

    public int rotateSpeed;
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }



    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Doorcollider.GetComponent<BoxCollider>().enabled = true;
            col.GetComponent<PlayerCollector>().Keys++;
            Destroy(gameObject);

        }
    }
}
