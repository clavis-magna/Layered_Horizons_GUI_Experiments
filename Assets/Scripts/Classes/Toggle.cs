using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public bool active;
    public string toggleName;

    void ToggleOn() {
        active = true;
    }

    void ToggleOff()
    {
        active = false;
    }

    //Toggle(bool _active, string _toggleName) {
    //    this.active = _active;
    //    this.toggleName = _toggleName;
    //}
}
