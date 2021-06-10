using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playersNeeded = 1;
    public int slenderID = -1;
    bool someoneIsSlender = false;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        StartSlender();
    }

    void StartSlender()
    {
        if (Server.clients.Count >= playersNeeded && ( !someoneIsSlender || slenderID == -1))
        {
            int randomPlayerID = Random.Range(1, Server.clients.Keys.Count);

            MakePlayerSlender(randomPlayerID);
        }
    }

    public void MakePlayerSlender(int _id)
    {
        Client client;
        Server.clients.TryGetValue(_id, out client);
        if (client != null)
        {
            Player slender = client.player;
            if (slender != null)
            {
                if(someoneIsSlender || slenderID != - 1)
                {
                    MakePlayerCitizen(slenderID);
                }
                slender.isSlender = true;
                someoneIsSlender = true;
                slenderID = _id;
                ServerSend.MakePlayerSlender(_id);
            }
        }
    }

    public void MakePlayerCitizen (int _id)
    {
        Client oldClient;
        Server.clients.TryGetValue(_id, out oldClient);
        if (oldClient != null)
        {
            Player oldSlender = oldClient.player;
            oldSlender.isSlender = false;
            slenderID = -1;
            someoneIsSlender = false;
        }
    }
}
