using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void Message(string message)
    {
        _text.text = message;
    }
}
