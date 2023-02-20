using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureAction : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private bool IsOn =false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        IsOn = !IsOn;
        anim.SetBool("On", IsOn);
    }
}
