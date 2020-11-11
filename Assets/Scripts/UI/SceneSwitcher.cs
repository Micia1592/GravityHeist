using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;

    private LayerMask UILayer;

    public float controlDelay = 0.5f;

    private PlayerControls playerControls;
    private GameObject gravgun;
    private bool gravgunLevel = false;

    private void Start() {
      //Deactivate player controls while level is loading
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      playerControls = player.GetComponent<PlayerControls>();
      playerControls.enabled =false;

      //Store the gravgun, and whether it should be active on level load
      gravgun = GameObject.FindGameObjectWithTag("GravGun");
      if (gravgun.activeSelf==true){
        gravgunLevel = true;
        gravgun.SetActive(false);
      }
    }

    public void SceneChange()
    {
        
      StartCoroutine((LoadLevel(SceneManager.GetActiveScene().buildIndex + 1))); //next level
       
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex); //next level

    }

    private void Update() {

      //Reactivate the player after a certain delay
      if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime>controlDelay)
      {
            playerControls.enabled = true;

            if(gravgunLevel){
              gravgun.SetActive(true);
            }
      }
      
    }
}
