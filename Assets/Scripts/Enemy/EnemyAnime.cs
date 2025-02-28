using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnime : MonoBehaviour
{
    private Transform _sprite;
    private float _activeSize;
    
    public float minSize;
    public float maxSize;
    public float speed;
    
    void Start()
    {
        _sprite = transform.Find("Sprite");
        _activeSize = maxSize;
    }

    void Update()
    {
        _sprite.localScale = Vector3.MoveTowards(_sprite.localScale, Vector3.one * _activeSize, speed * Time.deltaTime);

        if (_sprite.localScale.x == _activeSize)
        {
            if (_activeSize == maxSize)
                _activeSize = minSize;
            else
                _activeSize = maxSize;
        }
    }
}
