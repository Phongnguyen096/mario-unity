using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SideCrollingCamera : PNGMonoBehaviour
{

    [SerializeField] protected Transform player;
   
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerComponent();
    }
    private void LateUpdate()
    {
        this.GetPositionTranform();
    }
    protected virtual void LoadPlayerComponent()
    {
        if (player != null) return;
        player = GameObject.FindWithTag("Player").transform;

    }
    protected virtual void GetPositionTranform()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.x = Mathf.Max(cameraPos.x, player.position.x);
        transform.position = cameraPos;
    }
}
