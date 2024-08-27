using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterPanel : MonoBehaviour
{
    private Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Filter(string type)
    {
        if (type == "AtoZ")
        {
            manager.AtoZ = true;
            manager.UpdateFilter();
        }
    }
}
