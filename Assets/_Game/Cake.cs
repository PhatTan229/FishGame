using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using TMPro;

public abstract class Cake : MonoBehaviour
{
    public float Size => size;
    protected float size = 1;
    protected TextMeshPro txtSize;

    protected abstract void CheckFacing();
    public abstract void Eat(Cake whatToEat);
    public void IncreaseSize(float increase)
    {
        size += increase;
        if (size > Constants.MAX_PLAYER_SIZE) size = Constants.MAX_PLAYER_SIZE;
        transform.localScale = new Vector3(size, size);
        int sizeDisplay = (int)(size * 100);
        gameObject.GetChildComponent<TextMeshPro>("txtSize").text = sizeDisplay.ToString();
    }
}
