using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMoving : DanMonoBehaviour
{
    [SerializeField] protected List<Point> points = new();
    public List<Point> Points => points;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoint();
    }

    protected virtual void LoadPoint()
    {
        if(this.points.Count > 0) return;
        foreach (Transform child in transform)
        {
            Point point = child.GetComponent<Point>();
            if(point == null) continue;
            this.points.Add(point);
        }
    }

    public virtual Point GetPoint(int pointIndex)
    {
        return this.points[pointIndex];
    }
    
}
