using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationTrigger : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    private void AttackOver()
    {
        if (player != null)
        {
            player.basicAttackState.DoTrigger();
        }
    }
}
