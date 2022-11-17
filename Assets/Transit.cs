using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transit : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> Checkpoints { get; private set; }
    [field: SerializeField] public float CheckpointWaitTime { get; private set; }
    [field: SerializeField] public float CurrentCheckpointWaitTime { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public GameObject VehicleModel { get; private set; }
    [field: SerializeField] public bool CanBoard { get; private set; }
    [field: SerializeField] public bool CanStart { get; private set; }
    [field: SerializeField] public GameObject CurrentCheckpoint { get; private set; }


    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        if (CurrentCheckpoint == null)
        {
            CurrentCheckpoint = Checkpoints[0];
        }

        CurrentCheckpointWaitTime = CheckpointWaitTime;

    }

    // Update is called once per frame
    void Update()
    {
        ActivateTransit();
    }

    private GameObject TravelToNextCheckpoint(bool canStart, List<GameObject> checkpoints, GameObject currentCheckpoint, GameObject VehicleModel)
    {
        GameObject nextCheckpoint = GetNextCheckpoint(currentCheckpoint, checkpoints);

        float distance = Vector2.Distance(currentCheckpoint.transform.position, nextCheckpoint.transform.position);

        // Move the Vehicle GameObject based on its speed, deltaTime, and distance between checkpoint

        // when arrive at a checkpoint

        // wait and emit Unity Event
        BroadcastAllAboard(nextCheckpoint);

        // flip boolean CanBoard so player can board

        return nextCheckpoint;

    }

    private GameObject GetNextCheckpoint(GameObject current, List<GameObject> checkpoints)
    {
        int currentCheckpointIndex = checkpoints.IndexOf(current);
        if (currentCheckpointIndex != checkpoints.Count - 1)
        {
            return checkpoints[0];
        }
        return checkpoints[checkpoints.IndexOf(current) + 1];
    }

    private void ActivateTransit()
    {
        if (CanBoard)
        {
            if (CurrentCheckpointWaitTime > 0)
            {
                CurrentCheckpointWaitTime -= Time.deltaTime;
                Debug.Log(CurrentCheckpointWaitTime);
            }

            else
            {
                TravelToNextCheckpoint(CanStart, Checkpoints, CurrentCheckpoint, VehicleModel);
                CurrentCheckpointWaitTime = CheckpointWaitTime;
                CanBoard = false;
            }
        }
    }

    private GameObject BroadcastAllAboard(GameObject checkpoint)
    {
        return null;
    }
}
