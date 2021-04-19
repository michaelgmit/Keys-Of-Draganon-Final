using System;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using UnityEngine;
using RPG.Resources;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] CursorMapping[] cursorMappings = null;


        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                SceneManager.LoadScene("gameover");

                return;

            }

            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;

            SetCursor(CursorType.None);

        }


        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted(); //array of ray cast hits
            foreach (RaycastHit hit in hits) //adds each hit to array 
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this)) //raycast handeler
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay()); //recives all hits by the raycast
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++) //discovers the distance of all hits and sorts them
            {
                distances[i] = hits[i].distance; //returns the ray with correct distance
            }
            Array.Sort(distances, hits); //sorts the hits
            return hits;
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
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }
        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay() //sends camera to position of each ray cast
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}