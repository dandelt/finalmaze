using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbstract : DanMonoBehaviour
{
    [SerializeField] protected EnemyCtrl ctrl;
    public EnemyCtrl Ctrl => ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponentInParent<EnemyCtrl>();
        Debug.Log(transform.name + "LoadCtrl", gameObject);
    }
}
