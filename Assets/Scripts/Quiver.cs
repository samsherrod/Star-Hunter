using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Bulk of XR code taken from https://learn.unity.com/tutorial/creating-bow-and-arrow-gameplay-in-vr-from-vr-with-andrew?projectId=5fbc5135edbc2a0139266a9a

public class Quiver : XRSocketInteractor
{
    public GameObject arrowPrefab = null;
    private Vector3 attachOffset = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
        CreateAndSelectArrow();
        SetAttachOffset();
    }

    protected override void OnSelectExit(XRBaseInteractable interactable)
    {
        base.OnSelectExit(interactable);
        CreateAndSelectArrow();
    }

    private void CreateAndSelectArrow()
    {
        Arrow arrow = CreateArrow();
        SelectArrow(arrow);
    }

    private Arrow CreateArrow()
    {
        // Subtract offset to make sure arrow appears in the correct position
        GameObject arrowObject = Instantiate(arrowPrefab, transform.position - attachOffset, transform.rotation);
        //arrowObject.transform.parent = gameObject.transform;
        return arrowObject.GetComponent<Arrow>();
    }

    private void SelectArrow(Arrow arrow)
    {
        OnSelectEnter(arrow);
        arrow.OnSelectEnter(this);
    }

    private void SetAttachOffset()
    {
        if(selectTarget is XRGrabInteractable interactable)
        {
            attachOffset = interactable.attachTransform.localPosition;
        }
    }
}
