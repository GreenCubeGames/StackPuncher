using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private bool collect;
    public bool Collect { get => collect; set => collect = value; }

    public Transform anchorPoint;
}
