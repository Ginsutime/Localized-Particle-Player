using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Particle
{
    [SerializeField] string description = "Enter Description Here";

    [Space(10)]
    public ParticleSystem particles;
    public bool playOnAwake;
}
