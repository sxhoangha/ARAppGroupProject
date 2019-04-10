using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOnCollidingGoal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.ToString().ToLower().Contains("goal"))
        {
            Destroy(collision.gameObject);

            int goalCount = PlayerPrefs.GetInt("GoalCount");
            PlayerPrefs.SetInt("GoalCount", ++goalCount);
        }
        else
        {
            Debug.Log("No Collision");
        }      
    }
}
