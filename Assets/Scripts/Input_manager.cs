using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_manager : MonoBehaviour
{
    public InputData input_data;

    private void Update()
    {
        WriteInputData(); 
    }

    void WriteInputData()
    {
        input_data.is_pressed = Input.GetMouseButtonDown(0);
        input_data.is_held = Input.GetMouseButton(0);
        input_data.is_released = Input.GetMouseButtonUp(0);
    }
}
