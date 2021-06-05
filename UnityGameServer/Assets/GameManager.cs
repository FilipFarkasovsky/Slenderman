using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playersNeeded = 1;
    bool someoneIsSlender = false;

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
                    ServerSend.MakePlayerSlender(randomPlayerID);
                    Debug.Log($"{randomPlayerID} is Slender");
                }
            }
        }
    }
}
