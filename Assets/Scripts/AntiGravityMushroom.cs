using Platformer.Mechanics;
using UnityEngine;

public class AntiGravityMushroom : MonoBehaviour
{
    // TODO: If IsActive is set to true, have to retrigger collisions (overlap a box and do the same thing as in OnCollisionEnter2D).
    public bool IsActive { get; private set; } = false;

    public void Activate()
    {
        IsActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("OnTriggerEnter2D");
        var controller = collider.GetComponent<PlayerController>();
        if (controller == null)
        {
            return;
        }

        if (IsActive)
        {
            controller.InvertGravity = true;
            controller.StopJump();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        var controller = collider.GetComponent<PlayerController>();
        if (controller == null)
        {
            return;
        }

        if (IsActive)
        {
            controller.InvertGravity = false;
        }
    }
}
