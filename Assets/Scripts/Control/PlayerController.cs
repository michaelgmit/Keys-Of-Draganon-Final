using System;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay()); //array of ray cast hits
            foreach (RaycastHit hit in hits) //adds each hit to array 
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>(); //
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit) //hasHit is when the ray cast hits something it can hit and sends location to player controller
            {
                if (Input.GetMouseButton(0)) //listens for left mouse click
                {
                    GetComponent<Mover>().StartMoveAction(hit.point); //starts the movement of player calling to mover scritp
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay() //sends camera to position of each ray cast
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}