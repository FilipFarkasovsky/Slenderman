using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public static Dictionary<int, Paper> papers = new Dictionary<int, Paper>();
    private static int nextPaperId = 1;

    public int id;
    public int pickedByPlayer;

    private void Start()
    {
        id = nextPaperId;
        nextPaperId++;
    }

    private void PaperFound()
    {
        //ServerSend.PaperFound(this);

        papers.Remove(id);
        Destroy(gameObject);
    }
}
