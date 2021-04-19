﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] UnityEvent onHit;

    public void OnHit()
    {
        onHit.Invoke();
    }
}
