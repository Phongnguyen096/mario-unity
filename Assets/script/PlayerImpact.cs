using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerImpact : PNGMonoBehaviour
{
    [SerializeField] protected  Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody { get => this._rigidbody; }
    [SerializeField] protected BoxCollider2D Collider2D;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2D();
        this.LoadCollider();
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = transform.GetComponent<Rigidbody2D>();
    }
    protected virtual void LoadCollider()
    {
        if (this.Collider2D != null) return;
        this.Collider2D = transform.GetComponent<BoxCollider2D>();
    }

    

}
