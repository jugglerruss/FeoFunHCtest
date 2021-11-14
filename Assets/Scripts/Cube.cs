using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _playerMaterial;

    private MeshRenderer _mr;

    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
    }
    public void SetPlayerMaterial()
    {
        _mr.material = _playerMaterial;
    }
    public void SetNeutralMaterial()
    {
        _mr.material = _defaultMaterial;
    }
}
