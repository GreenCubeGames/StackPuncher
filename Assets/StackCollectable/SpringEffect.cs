using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stackables
{
    public class SpringEffect : MonoBehaviour
    {
        [SerializeField] private Transform springTarget;
        [SerializeField] private Transform springObj;

        [Space(12)]
        [SerializeField] private float drag = 2.5f;
        [SerializeField] private float springForce = 80.0f;
        [SerializeField] private float TargetDistanceY = 15;

        [Space(12)]
        [SerializeField] private Transform GeoParent;

        Rigidbody SpringRB;
        private Vector3 LocalDistance;
        private Vector3 LocalVelocity;

        void Start()
        {
            SpringInitializer();
        }

        private void SpringInitializer()
        {
            SpringRB = springObj.GetComponent<Rigidbody>();
            springObj.gameObject.SetActive(true);
            springObj.GetComponent<TrailRenderer>().enabled = false;
            springTarget.localPosition = new Vector3(0, TargetDistanceY, 0);
            springObj.transform.parent = null;
        }

        void FixedUpdate()
        {
            SyncRotation();
            CalculateTwoPointsDistance();
            CalculateSpringPointVelocity();
        }

        private void CalculateSpringPointVelocity()
        {
            LocalVelocity = (springObj.InverseTransformDirection(SpringRB.velocity));
            SpringRB.AddRelativeForce((-LocalVelocity) * drag);
        }

        private void CalculateTwoPointsDistance()
        {
            LocalDistance = springTarget.InverseTransformDirection(springTarget.position - springObj.position);
            SpringRB.AddRelativeForce((LocalDistance) * springForce);
        }

        private void SyncRotation()
        {
            SpringRB.transform.rotation = transform.rotation;
        }

        private void Update()
        {
            AimGeoTarget();
        }

        private void AimGeoTarget()
        {
            GeoParent.transform.LookAt(springObj.position, new Vector3(0, 0, 1));
        }
    }
}