﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootExplodeDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }


}
