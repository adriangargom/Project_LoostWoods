using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSystem : MonoBehaviour, IObservable
{
    private readonly List<IObserver> _actualObservers = new();

    [field: SerializeField] public float Damage { get; private set; }
    [SerializeField] private string[] _excludedTags;



    // Detects target collisions and inflicts damage the the target damageSystem
    void OnTriggerEnter(Collider other)
    {
        if(_excludedTags.Contains(other.tag)) return;

        if(other.TryGetComponent(out HealthSystem healthSystem))
        {
            if(healthSystem.IsInmune) return;
            
            healthSystem.DecreaseActualHealth((int)Damage);
            healthSystem.Notify();

            HitActions(other.gameObject);
        }
    }

    private void HitActions(GameObject target)
    {
        ParticlesPoolManager particlesPoolManager = ParticlesPoolManager.Instance;

        GameObject particlesItem = particlesPoolManager.HitParticlesPool.GetItem();
        particlesItem.transform.position = target.transform.position;
        particlesItem.SetActive(true);

        ItemsPoolManager.Instance.LaunchDelayedItemStore(
            particlesPoolManager.HitParticlesPool,
            particlesItem,
            1f
        );
    }

    public void SetDamage(float value)
    {
        Damage = value;
        Notify();
    }

    public void IncreaseDamage(float increment)
    {
        Damage += increment;
        Notify();
    }

    public void DecreaseDamage(float decrement)
    {
        Damage -= decrement;
        Notify();
    }



    // IObservable
    public void Attach(IObserver observer)
    {
        _actualObservers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _actualObservers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver observer in _actualObservers)
        {
            observer.ObserverUpdate();
        }
    }
}