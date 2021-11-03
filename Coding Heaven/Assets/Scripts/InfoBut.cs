using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBut : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void OpenPanel()
    {
        if (animator != null)
        { 
            bool isOpen = animator.GetBool("open");
            animator.SetBool("open", !isOpen);
        }
    }
}
