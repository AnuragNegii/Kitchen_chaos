using System;
using System.ComponentModel;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {

    [SerializeField] ContainerCounter containerCounter;

    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Start(){
        containerCounter.OnPlayerGrabObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}

