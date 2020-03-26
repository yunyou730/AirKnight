using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimTest : MonoBehaviour
{
    Animator animator = null;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(gameObject.name);
    }
}
