using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCleaner : MonoBehaviour
{
    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 0)
        {
            //Destroy(this.gameObject);
            Destroy(objs[0]); // destroy the old one
            Debug.Log("Destroyed old game manager");
        }
    }
}
