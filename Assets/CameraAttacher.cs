using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttacher : NetworkBehaviour
{
    [SerializeField] private GameObject cameraPrefab;
    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();
        GameObject ca = Instantiate(cameraPrefab);
        if (IsOwner)
        {
            ca.tag = "MainCamera";
            ca.GetComponent<FollowerCamera>().target = transform;
        } else
        {
            ca.SetActive(false);
        }
    }
}
