using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.GetComponent<Animator>().Play("SDance");
        }
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    this.GetComponent<Animator>().Play("Y");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    this.GetComponent<Animator>().Play("Z");
        //}
    }

    //private void OnMouseDownX()
    //{
    //    this.GetComponent<Animator>().Play("X");
    //}
    //private void OnMouseDownY()
    //{
    //    this.GetComponent<Animator>().Play("Y");
    //}
    //private void OnMouseDownZ()
    //{
    //    this.GetComponent<Animator>().Play("Z");
    //}
}
