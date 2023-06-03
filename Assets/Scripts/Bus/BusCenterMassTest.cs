using UnityEngine;

namespace Bus
{
    public class BusCenterMassTest : MonoBehaviour
    {
        [SerializeField] private Rigidbody busRb;
        [SerializeField] private Vector3 centerOfMass;

        private void Start()
        {
            busRb.centerOfMass = centerOfMass;
        }
    }
}