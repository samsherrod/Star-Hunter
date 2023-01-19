using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Bulk of XR code taken from https://learn.unity.com/tutorial/creating-bow-and-arrow-gameplay-in-vr-from-vr-with-andrew?projectId=5fbc5135edbc2a0139266a9a

public class Notch : XRSocketInteractor
{
    private Puller puller = null;
    private Arrow currentArrow = null;

    protected override void Awake()
    {
        base.Awake();
        puller = GetComponent<Puller>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        puller.onSelectExit.AddListener(TryToReleaseArrow);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        puller.onSelectExit.RemoveListener(TryToReleaseArrow);
    }

    protected override void OnSelectEnter(XRBaseInteractable interactable)
    {
        base.OnSelectEnter(interactable);
        StoreArrow(interactable);
    }

    private void StoreArrow(XRBaseInteractable interactable)
    {
        if(interactable is Arrow arrow)
        {
            currentArrow = arrow;
        }
    }

    private void TryToReleaseArrow(XRBaseInteractor interactor)
    {
        if (currentArrow)
        {
            ForceDeselect();
            ReleaseArrow();
        }
    }

    private void ForceDeselect()
    {
        base.OnSelectExit(currentArrow);
        currentArrow.OnSelectExit(this);
    }

    private void ReleaseArrow()
    {
        currentArrow.Release(puller.PullAmount);
        currentArrow = null;
    }

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride
    {
        get { return XRBaseInteractable.MovementType.Instantaneous; }
    }
}
