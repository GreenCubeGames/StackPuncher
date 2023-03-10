using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stackables {
    public class Collector : MonoBehaviour
    {
        [SerializeField] private Transform _anchorPoint;
        [SerializeField] private List<Transform> _collectables = new List<Transform>();
        [SerializeField] private float distanceStackables = .8f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Collectable>() != null)
            {
                if (_collectables.Count == 0)
                {
                    other.transform.SetParent(_anchorPoint);
                    other.transform.localPosition = new Vector3(0, distanceStackables, 0);
                }
                else if (_collectables.Count >= 1)
                {
                    other.transform.SetParent(_collectables[_collectables.Count - 1].GetComponent<Collectable>().anchorPoint, true);
                    other.transform.localPosition = new Vector3(0, 0, distanceStackables);
                    other.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
                }
                Destroy(other.GetComponent<Collider>());
                other.GetComponent<Collectable>().enabled = false;
                other.GetComponent<SpringEffect>().enabled = true;
                _collectables.Add(other.transform);
            }
        }
    }
}