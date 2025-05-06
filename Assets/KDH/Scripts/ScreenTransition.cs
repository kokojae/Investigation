using System;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] private RoomChange roomChange;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    

    public void OnStartEnd()
    {
        roomChange.MoveCamera();
        animator.SetTrigger("End");
    }
    
    public void OnEndEnd()
    {
        gameObject.SetActive(false);
    }
}
