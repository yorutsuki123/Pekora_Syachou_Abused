using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{

    SkinnedMeshRenderer smRenderer;

    // Start is called before the first frame update
    void Start()
    {
        smRenderer = GetComponent<SkinnedMeshRenderer>();
        smRenderer.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
