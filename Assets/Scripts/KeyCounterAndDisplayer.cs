using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCounterAndDisplayer : MonoBehaviour
{
    [SerializeField] private Image first;
    [SerializeField] private Image second;
    [SerializeField] private Image third;

    public List<string> keys { get; private set; }

    private void Awake()
    {
        keys = new List<string>();
    }
    public void TakeAKey(string color)
    {
        keys.Add(color);
        first.gameObject.SetActive(true);
    }
}
