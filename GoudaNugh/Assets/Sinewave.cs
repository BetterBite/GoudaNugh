using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Sinewave : MonoBehaviour
{

    public LineRenderer lr;
    public int points;
    public float amplitude = 1;
    public float frequency = 1;
    public float speed = 1;
    [SerializeField] public Vector2 limits = new Vector2(0, 1);


    // Start is called before
    // the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }



    void Draw()
    {
        float xStart = limits[0];
        float Tau = 2 * Mathf.PI;
        float xFinish = limits[1];


        lr.positionCount = points;
        for(int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint/(points-1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((x * Tau * frequency)+(Time.timeSinceLevelLoad * speed));
            lr.SetPosition(currentPoint, new Vector3(x, y, 0));
        }



    }

    // Update is called once per frame
    void Update()
    {
        
        Draw();
    }
}
