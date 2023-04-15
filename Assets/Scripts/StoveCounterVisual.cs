using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOn;
    [SerializeField] private GameObject particles;

    private void Start() {
        stoveCounter.OnStateChanged.AddListener(stoveCounter_OnStateChanged);
    }

    private void stoveCounter_OnStateChanged(StoveCounter.State state) {
        bool showVisual = state == StoveCounter.State.Frying || state == StoveCounter.State.Fried;
        stoveOn.SetActive(showVisual);
        particles.SetActive(showVisual);
    }
}
