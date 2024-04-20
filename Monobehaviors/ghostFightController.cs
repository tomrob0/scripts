using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostFightController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator theAnimator;

    void Start()
    {
        this.theAnimator = this.gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            this.theAnimator.SetTrigger("attack");

        }
    }
}
