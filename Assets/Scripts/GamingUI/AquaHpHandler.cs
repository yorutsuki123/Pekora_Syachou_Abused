using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AquaHpHandler : MonoBehaviour
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (target == null)
            target = GameObject.Find("Aqua");
        else
        {
            gameObject.GetComponent<Image>().fillAmount = (float)target.GetComponent<AquaController>().hp / target.GetComponent<AquaController>().maxHp;
        }
    }
}
