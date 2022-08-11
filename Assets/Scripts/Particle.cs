using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Particle
{
    public string label = "Enter Label Here";

    [Space(10)]
    public ParticleSystem particleSystem;
    public bool playOnAwake;
}
