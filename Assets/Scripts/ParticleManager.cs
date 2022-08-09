using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] Particle[] particleList;

    void Start()
    {
        ValidateHasParent();
        ValidateListNotEmpty();
    }

    void ValidateHasParent()
    {
        if (transform.parent == null)
            Debug.LogWarning(name + " is missing a parent.");
    }

    void ValidateListNotEmpty()
    {
        if (particleList.Length == 0)
            Debug.LogWarning(name + " has no particles to play.");
    }

    bool IsListElementNotEmpty(int i)
    {
        if (particleList[i] == null || i > particleList.Length)
        {
            Debug.LogWarning(name + " attempted to play nonexistant particles in element " + i + ".");
            return false;
        }
        else
            return true;
    }
}
