using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transit : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> Checkpoints { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject broadcastCurrentCheckpoint(List<GameObject> checkpoints)
    {
        return null;
    }
}
