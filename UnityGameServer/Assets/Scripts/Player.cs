using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region fields and properties
    [Header("Players informations")]
    public int id;
    public string username;
    public float health;
    public float maxHealth = 100f;

    private bool[] inputs;

    [Header("Assignables")]
    public Transform shootOrigin;
    public PlayerMovement playerMovement;

    [Header("Grenades informations")]
    public int itemAmount = 0;
    public int maxItemAmount = 3;
    public float throwForce = 1200f;
    #endregion

    #region start and update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;

        inputs = new bool[6];
    }

    /// <summary>Updates the player input with newly received input.</summary>
    /// <param name="_inputs">The new key inputs.</param>
    /// <param name="_rotation">The new rotation.</param>
    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        transform.rotation = _rotation;
    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        if (health <= 0f)
        {
            return;
        }


        playerMovement.x = 0;
        playerMovement.y = 0;

        if (inputs[0])
        {
            playerMovement.y = 1;
        }
        if (inputs[1])
        {
            playerMovement.y = -1;
        }
        if (inputs[2])
        {
            playerMovement.x = -1;
        }
        if (inputs[3])
        {
            playerMovement.x = +1;
        }

        playerMovement.jumping = inputs[4];


        //Crouching
        if (inputs[5])
        {
            if (!playerMovement.crouching)
            {
                playerMovement.StartCrouch();
                ServerSend.PlayerScale(this);
            }
        }
        else
        {
            if (playerMovement.crouching)
            {
                playerMovement.StopCrouch();
                ServerSend.PlayerScale(this);
            }
        }

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
        playerMovement.WallChecker();
        playerMovement.Movement();
    }
    #endregion

    #region multiplayer methods
    public void Shoot(Vector3 _viewDirection)
    {
        if (health <= 0f)
        {
            return;
        }

        if (Physics.Raycast(shootOrigin.position, _viewDirection, out RaycastHit _hit, 25f))
        {
            if (_hit.collider.CompareTag("Player"))
            {
                _hit.collider.GetComponent<Player>().TakeDamage(50f);
            }
        }
    }

    public void ThrowItem(Vector3 _viewDirection)
    {
        if (health <= 0f)
        {
            return;
        }

        if (itemAmount > 0)
        {
            itemAmount--;
            NetworkManager.instance.InstantiateProjectile(shootOrigin).Initialize(_viewDirection, throwForce, id);
        }
    }

    public void TakeDamage(float _damage)
    {
        if (health <= 0f)
        {
            return;
        }

        health -= _damage;
        if (health <= 0f)
        {
            health = 0f;
            transform.position = new Vector3(0f, 25f, 0f);
            ServerSend.PlayerPosition(this);
            StartCoroutine(Respawn());
        }

        ServerSend.PlayerHealth(this);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);

        health = maxHealth;
        ServerSend.PlayerRespawned(this);
    }

    public bool AttemptPickupItem()
    {
        if (itemAmount >= maxItemAmount)
        {
            return false;
        }

        itemAmount++;
        return true;
    }
    #endregion
}
