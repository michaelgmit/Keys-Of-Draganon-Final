using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using RPG.Core;
using UnityEngine;
using RPG.Resources;

public class HealthAdd : MonoBehaviour
    {
        public static int HealthA;
        int add = 20;

        public int rotateSpeed;
        void Update()
        {
            transform.Rotate(0, rotateSpeed, 0, Space.World);
        }

    /*    private void OnTriggerEnter(Collider coll)
        {
            if (coll.CompareTag("Player"))
            {
                coll.GetComponent<Health>().healthPoints += add;
                Destroy(gameObject);
                Health.singleton.addHealth(add);
            }
        }*/
}
