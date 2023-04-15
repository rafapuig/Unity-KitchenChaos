using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    public void PlatesCounter_OnPlateRemoved() {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    public void PlatesCounter_OnPlateSpawned() {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f;

        plateVisualTransform.localPosition = new Vector3 (0, plateOffsetY * plateVisualGameObjectList.Count , 0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

}
