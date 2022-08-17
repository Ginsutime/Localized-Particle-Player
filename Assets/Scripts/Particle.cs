using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Particle
{
    public string label = "Enter Label Here";

    [Space(10)]
    public ParticleSystem particleSystem;
    [Space(7)]
    public bool playOnAwake;

    public enum StopAction { None, Disable, Destroy, Callback };
    [Space(7)]
    public StopAction actions;
}
