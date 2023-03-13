using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {

    [SerializeField] private ClearCounter clearCounter;

    private void Start() {
        Player.Instance.SelectedCounterChanged += Player_SelectedCounterChanged;
    }

    private void Player_SelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e) {
        clearCounter.Interact();
    }
}
