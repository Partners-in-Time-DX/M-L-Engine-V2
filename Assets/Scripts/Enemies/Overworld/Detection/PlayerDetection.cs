using System;
using Enemies.Overworld.Types;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private AbstractOverworldEnemy _enemyCtx;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyCtx = GetComponentInParent<AbstractOverworldEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _enemyCtx.PlayerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _enemyCtx.PlayerDetected = false;
        }
    }
}
