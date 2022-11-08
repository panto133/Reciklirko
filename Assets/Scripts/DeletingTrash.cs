using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletingTrash : MonoBehaviour
{
    public Transform trash;

    public void Delete()
    {
        foreach (Transform t in trash)
        {
            Destroy(t.gameObject);
        }
    }
}
