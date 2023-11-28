using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialItem : MonoBehaviour
{
    public Tutorial tutorial;
    private void OnEnable()
    {
        tutorial = FindObjectOfType<Tutorial>();    
    }
    void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            tutorial.StartChat();
            this.gameObject.SetActive(false);
        }
    }
}
