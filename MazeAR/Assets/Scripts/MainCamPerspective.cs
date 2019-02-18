using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamPerspective : MonoBehaviour
{
    public Transform gameObject;
    // Start is called before the first frame update
    void Start()
    {
        // Rotate camera to look at the maze
        transform.LookAt(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
