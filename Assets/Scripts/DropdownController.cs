using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public static DropdownController instance;
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ResetDropDown()
    {
        dropdown1.value = 0;
        dropdown2.value = 0;
        dropdown3.value = 0;
    }
}
