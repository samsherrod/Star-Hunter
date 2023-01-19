using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

// Bulk of XR code taken from https://learn.unity.com/tutorial/creating-bow-and-arrow-gameplay-in-vr-from-vr-with-andrew?projectId=5fbc5135edbc2a0139266a9a

public class Bow : XRGrabInteractable
{
    private Animator animator = null;
    private Puller puller = null;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        puller = GetComponentInChildren<Puller>();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                AnimateBow(puller.PullAmount);
            }
        }
    }

    private void AnimateBow(float value)
    {
        animator.SetFloat("Blend", value);
    }
}
