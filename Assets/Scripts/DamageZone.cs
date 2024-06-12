using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var health = collider.GetComponent<Health>();
        if (health == null)
        {
            return;
        }
        
        Debug.Log($"{collider.gameObject.name} entered Damage Zone!");

        health.Hurt(_damage);
    }
}
