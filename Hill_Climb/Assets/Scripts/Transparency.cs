using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Transparency : NetworkBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material _material;
    
    void Start()
    {
        if (!IsOwner)
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _spriteRenderer.material = _material;
    }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
