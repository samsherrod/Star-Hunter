using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOffsetManager : MonoBehaviour
{
    public Vector3 leftHandOffset = Vector3.zero;
    public Vector3 rightHandOffset = Vector3.zero;

    private GameObject leftHand;
    private GameObject rightHand;

    private void Awake()
    {
        leftHand = GameObject.Find("[LeftHand Controller] Attach");
        rightHand = GameObject.Find("[RightHand Controller] Attach");
        leftHand.transform.position += leftHandOffset;
        rightHand.transform.position += rightHandOffset;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
