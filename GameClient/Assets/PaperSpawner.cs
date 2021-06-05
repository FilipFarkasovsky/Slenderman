using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSpawner : MonoBehaviour
{
    public int paperId;
    public bool hasItem;
    public MeshRenderer paperModel;
    private Vector3 basePosition;

    public void Initialize(int _paperId, bool _hasItem)
    {
        paperId = _paperId;
        hasItem = _hasItem;
        paperModel.enabled = _hasItem;

        basePosition = transform.position;
    }

    public void PaperSpawned()
    {
        hasItem = true;
        paperModel.enabled = true;
    }

    public void PaperPickedUp()
    {
        hasItem = false;
        paperModel.enabled = false;
    }
}
