using System;
using System.ComponentModel;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {

    [SerializeField] private CuttingCounter cuttingCounter;

    private const string CUT = "Cut";
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Start(){
        cuttingCounter.OnCut += ContainerCounter_OnCut;
    }

    private void ContainerCounter_OnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}

