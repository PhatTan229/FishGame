using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public abstract class Cake : MonoBehaviour
{
    public float Size => size;
    protected float size = 1;
    
    protected abstract void CheckFacing();
    public abstract void Eat(Cake whatToEat);
}
