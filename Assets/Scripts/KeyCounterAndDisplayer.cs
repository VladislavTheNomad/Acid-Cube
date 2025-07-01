using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AcidCube
{
    public class KeyCounterAndDisplayer : MonoBehaviour
    {
        [SerializeField] private Image first; // red
        [SerializeField] private Image second; // yellow
        [SerializeField] private Image third; // blue

        public List<string> keys { get; private set; }

        private void Awake()
        {
            keys = new List<string>();
        }
        public void TakeAKey(string color)
        {
            keys.Add(color);

            switch (color)
            {
                case "red":
                    first.gameObject.SetActive(true);
                    break;
                case "yellow":
                    second.gameObject.SetActive(true);
                    break;
                case "blue":
                    third.gameObject.SetActive(true);
                    break;

                default:    
                    break;
            }
        }
    }
}
