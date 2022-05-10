using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpeedTest : MonoBehaviour
{
    [SerializeField] Rigidbody linkedRigidbody;

    public string outputString = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        outputString += Time.time.ToString("F6") + System.Environment.NewLine;
    }
}
