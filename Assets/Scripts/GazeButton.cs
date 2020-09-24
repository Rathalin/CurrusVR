using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeButton : MonoBehaviour
{

    public void Red()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }


    public void Green()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }


    public void Blue()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
    

    public void White()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
