using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Resources;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float healthToRestore = 0;
        [SerializeField] float respawnTime = 5;
        [SerializeField] UnityEvent onHit;

        public void OnHit()
        {
            onHit.Invoke();
        }
   
    private void OnTriggerEnter(Collider other)
        {
             if (other.gameObject.tag == "Player")
            {
                Pickup(other.gameObject);
            }
        }
        private void Pickup(GameObject subject)
        {
            if (weapon != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weapon); //only equip weapon if not null
            }
            if (healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
            {
                ShowPickup(false);
                yield return new WaitForSeconds(seconds);
                ShowPickup(true);

            }
            private void ShowPickup(bool shouldShow)
            {
                GetComponent<Collider>().enabled = shouldShow;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(shouldShow);
                }
            }
        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }

    }

