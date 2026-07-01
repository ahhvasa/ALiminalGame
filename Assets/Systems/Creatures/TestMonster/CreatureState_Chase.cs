using System;
using UnityEngine;
using System.Collections;
using UnityEngine;

public class CreatureState_Chase : ICreatureState
{
    public Creature creature;
    public CreatureMovement creatureMovement;

    public Player player;


    public CreatureState_Chase(Creature creature)
    {
        this.creature = creature;
        creatureMovement = creature.GetComponent<CreatureMovement>();
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        creatureMovement.FollowTarget(player.transform);
    }
    public void OnExit()
    {
        creatureMovement.ClearFollowTarget();
    }
    public void FixedUpdate()
    {

    }
    public void Update()
    {

    }
}


