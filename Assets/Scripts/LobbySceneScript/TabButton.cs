using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabButton : MonoBehaviour
{
    public void Selected()
    {
        this.GetComponent<Button>().interactable = false;
    }

    public void DeSeleted()
    {
        this.GetComponent<Button>().interactable = true;
    }
}
