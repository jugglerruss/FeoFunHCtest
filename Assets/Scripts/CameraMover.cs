using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Player _player;
    [SerializeField] private float _duration;
    [SerializeField] private Transform _position;
    private int _lastCount;
    private Vector3 _targetLastPosition;
    private Tweener _tweenFollow;
    private void Start()
    {
        _tweenFollow = transform.DOMove(_position.position, _duration).SetAutoKill(false);
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (_targetLastPosition != _position.position)
        {
            _tweenFollow.ChangeEndValue(_position.position, true).Restart();
            _targetLastPosition = _position.position;
        }
        if (!_game.IsPlay || _lastCount == _player.CubesCount)
            return;
        float count = _player.CubesCount - _lastCount;
        _position.localPosition += new Vector3(0, count / 2, -count / 2);
        _lastCount = _player.CubesCount;     
    }
}
