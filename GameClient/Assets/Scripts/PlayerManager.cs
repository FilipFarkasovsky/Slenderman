using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public bool isSlender;
    public static int lastSlender = -1;
    public float health;
    public float maxHealth = 100f;
    public int itemCount = 0;
    public MeshRenderer model;

    public GameObject citizenModel;
    public GameObject slenderModel;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;
    }

    public void SetHealth(float _health)
    {
        health = _health;

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        model.enabled = false;
    }

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
    }

    public void MakeSlender(int _id)
    {
        if(lastSlender != -1)
        {
            PlayerManager lastSlenderPlayer = null;
            GameManager.players.TryGetValue(lastSlender, out lastSlenderPlayer);
            if (default(PlayerManager) != lastSlenderPlayer)
            {
                lastSlenderPlayer.isSlender = false;
                lastSlenderPlayer.citizenModel.SetActive(true);
                lastSlenderPlayer.slenderModel.SetActive(false);
            }
        }
        citizenModel.SetActive(false);
        slenderModel.SetActive(true);
        lastSlender = id;
        isSlender = true;
    }
}
