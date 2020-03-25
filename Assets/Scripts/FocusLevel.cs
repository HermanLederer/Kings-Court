using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusLevel : MonoBehaviour
{
    public float HalfXBounds = 40f;
    public float HalfYBounds = 25f;
    public float HalfZBounds = 25f;

    public Bounds FocusBounds;
    // Start is called before the first frame update
    private void Start()
    {
        Vector3 position = gameObject.transform.position;
        Bounds bounds = new Bounds();
        bounds.Encapsulate(new Vector3(position.x - HalfXBounds, position.y - HalfYBounds, position.z - HalfZBounds));
        bounds.Encapsulate(new Vector3(position.x + HalfXBounds, position.y + HalfYBounds, position.z + HalfZBounds));
        FocusBounds = bounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
