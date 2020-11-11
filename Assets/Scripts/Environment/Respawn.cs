using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public GameObject spawn;
    public GameObject BoxSpawn;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Player")
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
        //if (collision.tag == "Player")
        //{
        //    collision.transform.position = spawn.transform.position;
        //}

        //if (collision.tag == "GravityItem")
        //{
        //    collision.transform.position = BoxSpawn.transform.position;
        //}
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
