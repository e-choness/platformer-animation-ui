using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int _cherries = 0;
    [SerializeField] private TextMeshProUGUI text;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            _cherries++;
            text.text = $"Cherries Count: {_cherries.ToString()}";
#if UNITY_EDITOR
            Debug.Log($"Cherries: {_cherries.ToString()}");
#endif
        }
    }
}
