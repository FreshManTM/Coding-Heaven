using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    int money_click, passive_click;

    [SerializeField]Animator animator;

    [SerializeField] Text textPanel;

    private void Start()
    {
        money_click = PlayerPrefs.GetInt("money_click");
        passive_click = PlayerPrefs.GetInt("passive_click");
    }
    void Update()
    {
        textPanel.text = "Money for click: " + money_click +"$" + '\n' + '\n' + "Passive earnings: " + passive_click + "$"; 
    }

    public void OpenPanel()
    {
        if (animator != null)
        {
            bool isOpen = animator.GetBool("open");
            animator.SetBool("open", !isOpen);
        }
    }
}
