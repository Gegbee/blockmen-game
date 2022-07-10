using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttacher : NetworkBehaviour
{
    [SerializeField] private GameObject cameraPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject ca = Instantiate(cameraPrefab);
        ca.GetComponent<FollowerCamera>().target = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
