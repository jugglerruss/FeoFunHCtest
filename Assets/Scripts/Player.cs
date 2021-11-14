using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private float _startSpeed;
    [SerializeField] private Transform _finishPoint;
    [Header("ground Detect")]
    [SerializeField] private Transform _groundDetect;
    [SerializeField] private float _checkRadius;
    [Header("forward Detect")]
    [SerializeField] private Transform _forwardDetect;
    [SerializeField] private LayerMask _whatIsCubes;
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private Transform _cubesParent;
    [SerializeField] private Transform _neutralCubes;

    private bool _isWaitRestucture;
    private Collider[] _collidersForward;
    private Tweener _tweenForward;
    private Transform _lastCube;
    public int CubesCount => _cubesParent.childCount;
    private void FixedUpdate()
    {
        CheckDownEnemyCollision();
        CheckForward();
    }
    public void StartMoveForward()
    {
        _tweenForward = transform.DOMoveZ(_finishPoint.position.z, _finishPoint.position.z / _startSpeed).SetEase(Ease.Linear).OnComplete(_game.Win);
    }
    private void StopMoveForward()
    {
        _tweenForward.Kill();
    }
    private void CheckDownEnemyCollision()
    {
        var isCollideEnemyDown = Physics.OverlapBox(_groundDetect.position, new Vector3(_checkRadius, _checkRadius, 0.5f), new Quaternion(), _whatIsEnemy).Length > 0;
        if (_isWaitRestucture && !isCollideEnemyDown)
        {
            Restruture();
            _isWaitRestucture = false;
        }
    }
    private void CheckForward()
    {
        CheckEnemy();
        CheckNeutralCubes();
    }
    private bool CheckOverlapSphere(LayerMask layerMask)
    {
        _collidersForward = Physics.OverlapSphere(_forwardDetect.position, _checkRadius, layerMask);
        if (_collidersForward.Length > 0)
            return true;
        return false;
    }
    private void CheckEnemy()
    {
        if (!CheckOverlapSphere(_whatIsEnemy))
            return;
        var enemy = _collidersForward[0].GetComponent<Enemy>();
        if (!enemy.IsActive)
            return;
        enemy.IsActive = false;
        _lastCube = _cubesParent.GetChild(_cubesParent.childCount - 1);
        SetCubeAsNeutral(_lastCube);
        _isWaitRestucture = true;
        CheckLose();
    }
    private void CheckLose()
    {
        if (_cubesParent.childCount != 0)
            return;
        StopMoveForward();
        _game.Lose();
    }
    private void CheckNeutralCubes()
    {
        if (!CheckOverlapSphere(_whatIsCubes))
            return;
        var item = _collidersForward[0];
        if (item.transform.parent == _cubesParent)
            return;
        SetCubeAsPlayer(item.transform);   
    }
    private void SetCubeAsPlayer(Transform cube)
    {
        cube.SetParent(_cubesParent);
        cube.GetComponent<Cube>().SetPlayerMaterial();
        Restruture();
    }
    private void SetCubeAsNeutral(Transform cube)
    {
        cube.SetParent(_neutralCubes);
        cube.GetComponent<Cube>().SetNeutralMaterial();
    }
    private void Restruture()
    {
        var i = _cubesParent.childCount;
        foreach (Transform cube in _cubesParent)
        {
            cube.localPosition = new Vector3(0, i, 0);
            i--;
        }
    }

}
