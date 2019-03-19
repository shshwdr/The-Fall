using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderConnection : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        line.SetPositions(new Vector3[] { p1.position, p2.position });
    }
}
