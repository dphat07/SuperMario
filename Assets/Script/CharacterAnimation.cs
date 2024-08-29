using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }
    public void SetTrigger(string name)
    {
        animator.SetTrigger(name);
    }
    public void SetFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }

}
