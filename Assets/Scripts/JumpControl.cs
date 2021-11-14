using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpControl : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _minJumpHeight;
    [SerializeField] private float _maxJumpHeight;
    [Header("ground Detect")]
    [SerializeField] private Transform _groundDetect;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;

    private Tweener _tweenJump;
    private Tweener _tweenFall;

    private bool _isGrounded;
    private bool _isJumping;
    private bool _isFalling;

    private float _tweenJumpLastPosition;

    private void Update()
    {
        if(_game.IsPlay)
            WaitJump();
    }
    private void WaitJump()
    {
        _isGrounded = Physics.OverlapSphere(_groundDetect.position, _checkRadius, _whatIsGround).Length > 0;
        if (!_isJumping && _isGrounded && Input.GetMouseButtonDown(0))
        {
            DoJump();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isJumping = false;
        }
        if (_isJumping && Input.GetMouseButton(0) && _tweenJumpLastPosition != _maxJumpHeight)
        {
            DoLongJump();
        }
        if (!_isJumping && !_isGrounded && !_isFalling || transform.position.y == _maxJumpHeight)
        {
            DoFall();
        }
    }
    private void DoJump()
    {
        _tweenJump = transform.DOMoveY(_minJumpHeight, _jumpTime).SetAutoKill(false).SetRelative().SetEase(Ease.Linear);
        _tweenJumpLastPosition = _minJumpHeight;
        _isJumping = true;
    }
    private void DoLongJump()
    {
        _tweenJump.ChangeEndValue(new Vector3(0, _maxJumpHeight), true).Restart();
        _tweenJumpLastPosition = _maxJumpHeight;
    }
    private void DoFall()
    {
        _tweenJump.Kill();
        _tweenFall = transform.DOMoveY(- transform.localPosition.y, _jumpTime / 2).SetEase(Ease.Linear).SetRelative().OnComplete(StopFalling);
        _isFalling = true;

    }
    private void StopFalling()
    {
        _isFalling = false;
    }
}
