using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transit : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> Checkpoints { get; private set; }
    [field: SerializeField] public float BoardingTimeLimit { get; private set; }
    [field: SerializeField] public float BoardingTimeLeft { get; private set; }
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
        InitializeTransit();
    }

    void Update()
    {
        ActivateTransit();
    }

    private void InitializeTransit()
    {
        if (CurrentCheckpoint == null)
        {
            CurrentCheckpoint = Checkpoints[0];
            Boarding = true;
            MoveVehicle(Vehicle, CurrentCheckpoint);
        }

        if (BoardingTimeLeft == 0)
        {
            BoardingTimeLeft = BoardingTimeLimit;
        }
    }

    private void ActivateTransit()
    {
        if (CanStart)
        {
            if (Boarding)
            {
                SetWaiting();
            }

            if (!Boarding)
            {
                SetTraveling();
            }
        }
    }

    private void SetTraveling()
    {
        {
            if (DistanceFromNextCheckpoint > 0)
            {
                DistanceFromNextCheckpoint -= Time.deltaTime;
            }

            if (DistanceFromNextCheckpoint < 0)
            {
                CurrentCheckpoint = NextCheckpoint;
                Boarding = true;
            }
        }
    }

    private void SetWaiting()
    {
        MoveVehicle(Vehicle, CurrentCheckpoint); // To-do: Animate and remove from waiting method

        if (BoardingTimeLeft > 0)
        {
            BoardingTimeLeft -= Time.deltaTime;
        }

        if (BoardingTimeLeft < 0)
        {
            NextCheckpoint = GetNextCheckpoint(CurrentCheckpoint, Checkpoints);
            DistanceFromNextCheckpoint = GetDistanceFromCheckpoint(NextCheckpoint, Vehicle);
            BoardingTimeLeft = BoardingTimeLimit;
            Boarding = false;
        }
    }

    private float GetDistanceFromCheckpoint(GameObject checkpoint, GameObject Vehicle)
    {
        return Vector2.Distance(Vehicle.transform.position, checkpoint.transform.position);
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

    private void MoveVehicle(GameObject vehicle, GameObject destination)
    {
        vehicle.transform.position = destination.transform.position;
    }

    private GameObject BroadcastAllAboard(GameObject checkpoint)
    {
        return null;
    }
}
