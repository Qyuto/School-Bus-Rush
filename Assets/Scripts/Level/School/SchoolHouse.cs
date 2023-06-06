using Bus;
using UnityEngine;

namespace Level.School
{
    public class SchoolHouse : MonoBehaviour
    {
        [SerializeField] private TransformSizeScaler sizeScaler;
        [SerializeField] private int minPassengerCount;
        [SerializeField] private BusTrigger busTrigger;
        [SerializeField] private LayerMask overlappedLayerMask;
        [SerializeField] private Vector3 destroyRayPosition;
        [SerializeField] private float destroyRadius;

        private int _currentPassengerInSchool;
        private int _currentPassengerRateInSchool;
        private int _beforeUnloadingPassengerCount;
        private SchoolHouseUI _schoolHouseUI;
        private BusLevelCompletion _levelCompletion;
        private BusRatePassenger _ratePassenger;
        private PassengerCount _passengerCount;

        private readonly Collider[] _overlappedCollider = new Collider[5];

        private void Awake()
        {
            _schoolHouseUI = GetComponent<SchoolHouseUI>();
            busTrigger.onTriggerEnter.AddListener(GetComponents);
        }

        private void LateUpdate()
        {
            if (_levelCompletion == null || !_levelCompletion.isArrived) return;
            FindPassenger();
        }

        private void FindPassenger()
        {
            int size = GetOverlappedColliders();
            if (size == 0) return;
            ProcessOverlappedColliders(size);
            ComparePassengers();
        }

        private int GetOverlappedColliders()
        {
            int size = Physics.OverlapSphereNonAlloc(transform.position + destroyRayPosition, destroyRadius,
                _overlappedCollider,
                overlappedLayerMask);
            return size;
        }

        private void ProcessOverlappedColliders(int size)
        {
            for (int i = 0; i < size; i++)
            {
                ProcessPassenger();
                DestroyOverlappedCollider(i);
            }
        }

        private void ComparePassengers()
        {
            if (_beforeUnloadingPassengerCount != _currentPassengerInSchool) return;

            if (_currentPassengerInSchool >= minPassengerCount) _levelCompletion.onBusLevelComplete?.Invoke();
            else _levelCompletion.onBusLevelFailComplete?.Invoke();
        }

        private void ProcessPassenger()
        {
            _currentPassengerInSchool++;
            _ratePassenger.TryAddRate(_currentPassengerInSchool);

            int passengerRate = 1 * _ratePassenger.Rate;
            _currentPassengerRateInSchool += passengerRate;
            _passengerCount.AddTotalPassenger(passengerRate);

            _schoolHouseUI.UpdatePassengerText(_currentPassengerRateInSchool);
            _schoolHouseUI.UpdateRateSlider(_currentPassengerInSchool);
            sizeScaler.IncreaseScale((Vector3)Vector2.one / 500f, true);
        }

        private void DestroyOverlappedCollider(int index)
        {
            Destroy(_overlappedCollider[index].gameObject);
        }

        private void GetComponents(GameObject bus)
        {
            IBusInteractor busInteractor = bus.GetComponentInParent<IBusInteractor>();
            if (busInteractor == null) return;

            _passengerCount = busInteractor.GetPassengerCountComponent();
            _levelCompletion = busInteractor.GetBusLevelCompletion();
            _ratePassenger = _levelCompletion.GetComponent<BusRatePassenger>();
            AgentCoordinator agentCoordinator = _passengerCount.GetComponent<AgentCoordinator>();

            _schoolHouseUI.Init(_ratePassenger);
            agentCoordinator.SetAgentDestination(transform.position + destroyRayPosition);
            _beforeUnloadingPassengerCount = _passengerCount.CurrentPassenger;
            _levelCompletion.onBusArrivedAtEnd?.Invoke();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + destroyRayPosition, destroyRadius);
        }
    }
}