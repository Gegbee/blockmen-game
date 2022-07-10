using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{

    [SyncVar]
    public string username;

    [SyncVar]
    public bool isReady;

    private Vector2 aimingVelocity = new Vector2(0f, 0f);
    private float aimTime;

    private float maxAxisVelocity = 4.5f;
    private float maxAimTime = 1.5f;
    private float velocityPerSecond;

    private float timeDownX = 0f;
    private float timeDownY = 0f;
    
    void Update()
    {
        //if (IsOwner)
        //{
        //GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<FollowerCamera>().target = transform;
        //} else
        //{
        //    GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<FollowerCamera>().target = null;
        //}

        if (!IsOwner) return;
        // velocityPerSecond = (maxAxisVelocity - 1f) / maxAimTime * Time.deltaTime;
        // Vector3 frameAim = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            timeDownX += Time.deltaTime;
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
        {
            timeDownY += Time.deltaTime;
        }

        aimingVelocity.x += Input.GetAxisRaw("Horizontal") * Time.deltaTime * maxAxisVelocity;
        aimingVelocity.y += Input.GetAxisRaw("Vertical") * Time.deltaTime * maxAxisVelocity;

        if (timeDownX >= maxAimTime || (timeDownX > 0d && Mathf.Abs(Input.GetAxisRaw("Horizontal")) <= 0))
        {
            timeDownX = 0f;
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(aimingVelocity.x + 0.4f * Mathf.Sign(aimingVelocity.x), 0), ForceMode2D.Impulse);
            aimingVelocity.x = 0f;
        }
        if (timeDownY >= maxAimTime || (timeDownY > 0d && Mathf.Abs(Input.GetAxisRaw("Vertical")) <= 0))
        {
            timeDownY = 0f;
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, aimingVelocity.y + 0.4f * Mathf.Sign(aimingVelocity.y)), ForceMode2D.Impulse);
            aimingVelocity.y = 0f;
        }
        //Debug.Log(aimingVelocity.x.ToString() + ", " + aimingVelocity.y.ToString());
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
    }
}
