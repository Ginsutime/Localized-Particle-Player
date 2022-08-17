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

                    if (particle.actions == Particle.StopAction.None)
                        main.stopAction = ParticleSystemStopAction.None;
                    else if (particle.actions == Particle.StopAction.Disable)
                        main.stopAction = ParticleSystemStopAction.Disable;
                    else if (particle.actions == Particle.StopAction.Destroy)
                        main.stopAction = ParticleSystemStopAction.Destroy;
                    else if (particle.actions == Particle.StopAction.Callback)
                        main.stopAction = ParticleSystemStopAction.Callback;
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

    public void DetachToPlayThenDestroy(int indexToPlay)
    {
        if (!IsListElementMissingProperStopAction(indexToPlay)) return;

        var main = particleList[indexToPlay].particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Destroy;

        transform.parent = null;

        Play(indexToPlay);

        StopAllCoroutines();
        StartCoroutine(DestroyWhenParticleSystemDestroyed(indexToPlay));
    }

    IEnumerator DestroyWhenParticleSystemDestroyed(int indexToPlay)
    {
        while (particleList[indexToPlay].particleSystem != null)
            yield return null;
        
        Destroy(gameObject);
    }

    #region Validation/Checks
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

    bool IsListElementMissingProperStopAction(int i)
    {
        if (particleList[i].actions != Particle.StopAction.Destroy)
        {
            Debug.LogWarning(name + " needs to have StopAction set to Destroy in " +
                "order to run this function for element " + i + ".");
            return false;
        }
        else
            return true;
    }
    #endregion
}
