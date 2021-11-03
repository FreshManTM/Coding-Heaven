using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effects : MonoBehaviour
{
    [SerializeField]Text text;
    [SerializeField]CanvasGroup group;
    
    void Update()
    {
        group.alpha = Mathf.MoveTowards(group.alpha, 0, Time.deltaTime * 2);
        transform.position += Vector3.up * Time.deltaTime * 60;

        if (group.alpha < 0.01f)
            Destroy(gameObject);
    }

    public void SetPosition()
    {
        transform.position = new Vector3(0,38,50);
    }

    public void SetValue(int value)
    {
        text.text = "+" + value + "$";
    }

    public void SetPassiveColor()
    {
        text.color = Color.blue;
    }

}
