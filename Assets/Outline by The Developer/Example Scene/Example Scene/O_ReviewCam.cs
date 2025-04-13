using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 새로운 Input System 네임스페이스

public class O_ReviewCam : MonoBehaviour
{
    public O_CustomImageEffect customImageEffect;
    public float speed;
    public Material[] mats;
    private int currIndx = 0;

    private void Start()
    {
        ActivateNewMat(0);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            ActivateNewMat(currIndx - 1);
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            ActivateNewMat(currIndx + 1);
        }
    }

    private void ActivateNewMat(int newIndx)
    {
        if (newIndx < 0 || newIndx >= mats.Length)
            return;
        customImageEffect.imageEffect = mats[newIndx];
        currIndx = newIndx;
    }
}
