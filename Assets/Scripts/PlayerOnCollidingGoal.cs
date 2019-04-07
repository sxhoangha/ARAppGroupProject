using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnCollidingGoal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.ToString().ToLower().Contains("goal"))
        {
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("kkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
        }      
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
