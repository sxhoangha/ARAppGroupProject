using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOnCollidingGoal : MonoBehaviour
{
    public GameObject PanelGameOver;

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
    // Start is called before the first frame update
    void Start()
    {

        PanelGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
