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
        if(Server.clients.Count >= playersNeeded && !someoneIsSlender)
        {
            int randomPlayerID = Random.Range(0, Server.clients.Keys.Count);

            Client client;
            Server.clients.TryGetValue(randomPlayerID, out client);
            if(client != null)
            {
                Player slender = client.player;
                if(slender != null)
                {
                    slender.isSlender = true;
                    someoneIsSlender = true;
                    slenderID = randomPlayerID;
                    ServerSend.MakePlayerSlender(randomPlayerID);
                    Debug.Log($"{randomPlayerID} is Slender");
                }
            }
        }
    }
}
