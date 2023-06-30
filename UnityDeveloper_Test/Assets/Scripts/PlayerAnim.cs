using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("IsRunning", player.IsRunning());
        anim.SetBool("IsFalling", player.IsFalling());
    }
}
