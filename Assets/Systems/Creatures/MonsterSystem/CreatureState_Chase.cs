using System;
using UnityEngine;
using System.Collections;

public class CreatureState_Chase : ICreatureState
{
    public Creature creature;
    public CreatureMovement creatureMovement;
    public Player player;


    public CreatureState_Chase(Creature creature)
    {
        this.creature = creature;
        creatureMovement = creature.movement;
    }

    public CreatureState_Chase(Creature creature, Player player) : this(creature)
    {
        this.player = player;
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


