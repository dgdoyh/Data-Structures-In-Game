using System;
using UnityEngine;

// This will Invoke OnDestroyed when this target gets destroyed 
public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
