using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {

    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        //cuttingCounter.PlayerGrabbedObject += ContainerCounter_PlayerGrabbedObject;
        cuttingCounter.OnCut.AddListener(() => { animator.SetTrigger(CUT); });
    }

    private void CuttingCounter_OnCut() {
        animator.SetTrigger(CUT);
    }
}
