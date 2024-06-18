using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public Transform[] checkpoints;
    private static GameObject[] hidingSpots;

    public GameObject[] HidingSpots {  get { return hidingSpots; } }

    public static AreaManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }
}
