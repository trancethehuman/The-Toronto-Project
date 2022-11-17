using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transit : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> Checkpoints { get; private set; }
    [field: SerializeField] public float CheckpointWaitTime { get; private set; }
    [field: SerializeField] public float CurrentCheckpointWaitTime { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public GameObject Vehicle { get; private set; }
    [field: SerializeField] public bool Boarding { get; private set; }
    [field: SerializeField] public bool CanStart { get; private set; }
    [field: SerializeField] public GameObject CurrentCheckpoint { get; private set; }
    [field: SerializeField] public GameObject NextCheckpoint { get; private set; }
    [field: SerializeField] public float DistanceFromNextCheckpoint { get; private set; }

    void Start()
    {

    }

    void Awake()
    {
        if (CurrentCheckpoint == null)
        {
            CurrentCheckpoint = Checkpoints[0];
            Boarding = true;
            Vehicle.transform.position = CurrentCheckpoint.transform.position;
        }

        if (CurrentCheckpointWaitTime == 0)
        {
            CurrentCheckpointWaitTime = CheckpointWaitTime;
        }
    }

    void Update()
    {
        ActivateTransit();
    }

    private float GetDistanceFromCheckpoint(GameObject checkpoint, GameObject Vehicle)
    {
        float distance = Vector2.Distance(Vehicle.transform.position, checkpoint.transform.position);
        BroadcastAllAboard(checkpoint);
        return distance;

    }

    private GameObject GetNextCheckpoint(GameObject current, List<GameObject> checkpoints)
    {
        int currentCheckpointIndex = checkpoints.IndexOf(current);
        if (currentCheckpointIndex == checkpoints.Count - 1)
        {
            return checkpoints[0];
        }
        return checkpoints[checkpoints.IndexOf(current) + 1];
    }

    private void ActivateTransit()
    {
        if (Boarding)

        {
            Vehicle.transform.position = CurrentCheckpoint.transform.position;
            if (CurrentCheckpointWaitTime > 0)
            {
                CurrentCheckpointWaitTime -= Time.deltaTime;
                Debug.Log("Currently at a checkpoint for: " + CurrentCheckpointWaitTime);
            }

            if (CurrentCheckpointWaitTime < 0)
            {
                NextCheckpoint = GetNextCheckpoint(CurrentCheckpoint, Checkpoints);
                DistanceFromNextCheckpoint = GetDistanceFromCheckpoint(NextCheckpoint, Vehicle);
                CurrentCheckpointWaitTime = CheckpointWaitTime;
                Boarding = false;
            }

        }
        if (!Boarding)
        {
            if (DistanceFromNextCheckpoint > 0)
            {
                DistanceFromNextCheckpoint -= Time.deltaTime;
                Debug.Log("Remaining distance from next checkpoint: " + DistanceFromNextCheckpoint);
            }

            if (DistanceFromNextCheckpoint < 0)
            {
                CurrentCheckpoint = NextCheckpoint;
                Boarding = true;
            }

        }
    }

    private GameObject BroadcastAllAboard(GameObject checkpoint)
    {
        return null;
    }
}
