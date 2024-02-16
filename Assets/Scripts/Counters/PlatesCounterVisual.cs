using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {

    [SerializeField] private PlatesCounter platesCounter;

    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    public void Start(){
        platesCounter.OnPlatesSpawned += PlatesCounter_OnPlatesSpawned;
        platesCounter.OnPlatesRemoved += PlatesCounter_OnPlatesRemoved;
    }

    private void PlatesCounter_OnPlatesRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count -1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlatesSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform =  Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;

        plateVisualTransform.localPosition = new Vector3(0f, plateVisualGameObjectList.Count * plateOffsetY, 0f);
         plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}