using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currLocationText;
    [SerializeField] Map map;


    private void Start()
    {
        map.OnCurrLocationUpdated += UpdateCurrLocationText;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    private void OnDisable()
    {
        map.OnCurrLocationUpdated -= UpdateCurrLocationText;
    }

    private void UpdateCurrLocationText(string currLocation)
    {
        currLocationText.text = "Current Location: " + currLocation;
    }

    private void ToggleMap()
    {
        map.gameObject.SetActive(!map.gameObject.activeSelf);
    }
}
