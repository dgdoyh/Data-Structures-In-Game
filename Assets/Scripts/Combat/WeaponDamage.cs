using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will turn on/off the collider of weapon and deal damage
public class WeaponDamage : MonoBehaviour
{
    // My body collider (not weapon collider)
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        // Clear the info of colliders that this already collided with
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If it touches my collider, ignore
        if (other == myCollider) { return; }

        // If you already attacked the target, ignore
        if (alreadyCollidedWith.Contains(other)) { return; }

        // If the target gets attack at the first time, add the target to alreadyCollidedWith
        alreadyCollidedWith.Add(other);

        // Get target's health and do damage
        if (other.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.GetDamage(damage);
        }

        // If the target has ForceReceiver,
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            // Get normalized direction of the target get attack from
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            // And it gets knockback to the direction
            forceReceiver.AddForce(direction * knockback);
        }
    }

    // Set damage and knockback info
    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
