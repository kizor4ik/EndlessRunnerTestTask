using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MovementParameters", menuName = "ScriptableObjects/MovementParameters", order = 1)]

public class MovementParameters : ScriptableObject
{
    public float InitialSpeed = 15;
    public float MaxSpeed = 200f;
    public float SlowDownRate=3f;
    public float JumpHeight=3f;
    public float Duration=0.5f;
}
