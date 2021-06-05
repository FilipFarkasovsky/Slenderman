using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperManager : MonoBehaviour
{
    public int id;
    public GameObject paperPrefab;

    public void Initialize(int _id)
    {
        id = _id;
    }

    public void PickedUp(Vector3 _position)
    {
        GameManager.projectiles.Remove(id);
        Destroy(gameObject);
    }
}
