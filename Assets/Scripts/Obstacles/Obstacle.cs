using System;
using Bus;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour, IInteractable
    {
        [SerializeField] protected bool useVFX;
        [SerializeField] protected ParticleSystem vfxExplosion;
        public Action<IBusInteractor> onInteractSelect;

        public virtual void Select(IBusInteractor interactor)
        {
            BusLevelCompletion levelCompletion = interactor.GetBusLevelCompletion();
            if (levelCompletion == null) return;
            if (useVFX) Instantiate(vfxExplosion, transform.position, Quaternion.identity).Play();
            levelCompletion.onBusLevelFailComplete?.Invoke();
            onInteractSelect?.Invoke(interactor);
            Destroy(this);
        }
    }
}