using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] Particle[] particleList = new Particle[1];

#if UNITY_EDITOR
    void OnValidate()
    {
        for (int i = 0; i < particleList.Length; ++i)
        {
            Particle particle = particleList[i];

            if (particle != null)
            {
                if (particle.label.Length == 0)
                    particle.label = i + ". Enter Label Here";
                else if (particle.label.Substring(0, 1) != i.ToString())
                    particle.label = i + ". Enter Label Here";

                if (particle.particleSystem)
                {
                    var main = particle.particleSystem.main;
                    main.playOnAwake = particle.playOnAwake;
                }
            }
        }
    }
#endif

    void Awake()
    {
        foreach (Particle particle in particleList)
        {
            if (particle.particleSystem != null)
            {
                if (particle.particleSystem.isPlaying && !particle.playOnAwake)
                    particle.particleSystem.Stop();
            }
        }
    }

    void Start()
    {
        ValidateHasParent();
        ValidateListNotEmpty();
    }

    public void Play(int indexToPlay)
    {
        if (!IsListElementMissingParticleSystem(indexToPlay)) return;

        particleList[indexToPlay].particleSystem.Play();
    }

    void ValidateHasParent()
    {
        if (transform.parent == null)
            Debug.LogWarning(name + " is missing a parent.");
    }

    void ValidateListNotEmpty()
    {
        if (particleList.Length == 0)
            Debug.LogWarning(name + " has no particles to play because the list is empty.");
    }

    bool IsListElementMissingParticleSystem(int i)
    {
        if (i >= particleList.Length)
        {
            Debug.LogWarning(name + " attempted to play particle system in nonexistant (out of range) element " + i + ".");
            return false;
        }

        if (particleList[i].particleSystem == null)
        {
            Debug.LogWarning(name + " attempted to play nonexistant particle system in element " + i + ".");
            return false;
        }
        else
            return true;
    }
}
