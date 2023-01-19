using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/******************************************
 * DON"T TOUCH ANYTHING UNTIL A BACKUP IS MADE!!!!!!!!!!!!!!!!!
 * ***************************************/




// Bulk of XR code taken from https://learn.unity.com/tutorial/creating-bow-and-arrow-gameplay-in-vr-from-vr-with-andrew?projectId=5fbc5135edbc2a0139266a9a

public class Arrow : XRGrabInteractable
{
    public float speed = 2000.0f;
    public Transform tip = null;

    private bool inAir = false;
    private Vector3 lastPosition = Vector3.zero;

    private Rigidbody rigidBody = null;

    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            CheckForCollision();
            lastPosition = tip.position;
            //DestroyArrow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star") && other.GetComponent<StarManager>() != null)
        {
            other.GetComponent<StarManager>().ChangeMaterial();
        }
    }

    private void CheckForCollision()
    {
        if(Physics.Linecast(lastPosition, tip.position))
        {
            Stop();
        }
    }

    private void Stop()
    {
        inAir = false;
        SetPhysics(false);
    }

    public void Release(float pullValue)
    {
        inAir = true;
        SetPhysics(true);

        MaskAndFire(pullValue);
        StartCoroutine(RotateWithVelocity());

        // Turn light after arrow is released
        GetComponent<Light>().enabled = true;

        lastPosition = tip.position;
    }

    private void SetPhysics(bool usePhysics)
    {
        rigidBody.isKinematic = !usePhysics;
        rigidBody.useGravity = usePhysics;
    }

    private void MaskAndFire(float power)
    {
        colliders[0].enabled = false;
        interactionLayerMask = 1 << LayerMask.NameToLayer("Ignore");

        Vector3 force = transform.forward * (power * speed);
        rigidBody.AddForce(force);
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();

        while (inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void DestroyArrow()
    {
        Destroy(this.gameObject, 3.0f);
    }

    public new void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
    }

    public new void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
    }
}
