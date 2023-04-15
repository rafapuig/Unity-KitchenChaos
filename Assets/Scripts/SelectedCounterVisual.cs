using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
   
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start() {
        Player.Instance.SelectedCounterChanged += Player_SelectedCounterChanged;
    }

    //Cada counter mira si es el mismo el que ha sido seleccionado para mostrarse o esconder el counter resaltado
    private void Player_SelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e) {
        if(e.selectedCounter == baseCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        foreach (var visualGameObject in visualGameObjectArray) {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide() {
        foreach (var visualGameObject in visualGameObjectArray) {
            visualGameObject.SetActive(false);
        }

    }
}
