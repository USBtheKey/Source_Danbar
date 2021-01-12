
using UnityEngine;

public class LineRenderer_FX : MonoBehaviour
{
    private LineRenderer lRend = null;

    [SerializeField] private Transform trans = null;
    private Vector3 p2 = Vector3.zero;

    [SerializeField] float step = 0f;

    private bool reached = false;
    void Start()
    {
        lRend = GetComponent<LineRenderer>();

        p2 = trans.position;

        lRend.SetPosition(0, transform.position);
        lRend.SetPosition(1, transform.position);


        Destroy(trans.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (reached)
            return;

        lRend.SetPosition(1, Vector3.MoveTowards(lRend.GetPosition(1), p2, step));

        if ((lRend.GetPosition(1) - p2).magnitude <= step)
        {
            reached = true;
            Debug.Log("Reached");
        }
            
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;

        Debug.DrawLine(transform.position, trans.position, Color.white);
    }
}
