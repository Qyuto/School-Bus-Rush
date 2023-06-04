using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class AdjustWheel : EditorWindow
    {
        private WheelCollider[] _wheelColliders;
        private Rigidbody _rigidbody;

        private int _naturalFrequency = 10;
        private float _dampingRatio = 0.8f;
        private float _forceShift = 0.03f;
        private bool _setSuspensionDistance = true;

        [MenuItem("Vehicles/Adjust the wheels of the car")]
        private static void ShowWindow()
        {
            var window = GetWindow<AdjustWheel>();
            window.titleContent = new GUIContent("Wheel");
            window.Show();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Get wheels"))
            {
                _wheelColliders = null;
                _rigidbody = null;
                if (FindComponents())
                {
                    Debug.Log($"Find wheels");
                    Debug.Log($"Find RigidBody: {_rigidbody.transform.name}");
                }
            }

            _naturalFrequency =
                EditorGUILayout.IntSlider(new GUIContent("Natural Frequency"), _naturalFrequency, 0, 20);
            _dampingRatio = EditorGUILayout.Slider(new GUIContent("Damping Ratio"), _dampingRatio, 0f, 3f);
            _forceShift = EditorGUILayout.Slider(new GUIContent("Force Shift"), _forceShift, -1f, 1f);

            if (GUILayout.Button("Configuration"))
                WheelConfiguration();
        }

        private bool FindComponents()
        {
            foreach (GameObject gObject in Selection.gameObjects)
            {
                WheelCollider[] colliders = gObject.GetComponentsInChildren<WheelCollider>();

                if (colliders != null)
                {
                    _rigidbody = colliders[0].GetComponentInParent<Rigidbody>();
                    _wheelColliders = colliders;
                    return _rigidbody != null && _wheelColliders != null;
                }
            }

            return false;
        }

        private void WheelConfiguration()
        {
            foreach (WheelCollider wc in _wheelColliders)
            {
                JointSpring spring = wc.suspensionSpring;

                spring.spring = Mathf.Pow(Mathf.Sqrt(wc.sprungMass) * _naturalFrequency, 2);
                spring.damper = 2 * _dampingRatio * Mathf.Sqrt(spring.spring * wc.sprungMass);

                wc.suspensionSpring = spring;

                Vector3 wheelRelativeBody = _rigidbody.transform.InverseTransformPoint(wc.transform.position);
                float distance = _rigidbody.centerOfMass.y - wheelRelativeBody.y + wc.radius;

                wc.forceAppPointDistance = distance - _forceShift;

                // the following line makes sure the spring force at maximum droop is exactly zero
                if (spring.targetPosition > 0 && _setSuspensionDistance)
                    wc.suspensionDistance =
                        wc.sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);
            }
        }
    }
}