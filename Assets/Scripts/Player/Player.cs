using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;
    public static Player Instance; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("Engine/EngineEffect").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        playerMovement.Move(); // Panggil method Move 
    }
    private void LateUpdate()
    {
        bool isMoving = playerMovement.IsMoving();
        animator.SetBool("IsMoving", isMoving);
        Debug.Log("Animator IsMoving set to: " + isMoving); 
    }
}