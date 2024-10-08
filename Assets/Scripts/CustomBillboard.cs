using UnityEngine;

public abstract class CustomBillboard : MonoBehaviour
{
    // Sprites
    protected float _moveAngle;
    protected float _displayAngle;
    protected string _facing = "_down";
    protected Transform _cam;
    private Transform _spriteTransform;
    protected SpriteRenderer _sprite;
    protected Animator _animator;
    protected int _offset = 0;
    
    protected void Init(GameObject child)
    {
        _spriteTransform = child.transform;
        _animator = child.GetComponent<Animator>();
        _sprite = child.GetComponent<SpriteRenderer>();
        _cam = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    protected void LateUpdate()
    {
        _facing = SetFacing(_cam.eulerAngles.y - transform.eulerAngles.y);
        
        SetAnimation();

        Quaternion rot = _spriteTransform.rotation;
        _spriteTransform.transform.rotation = _cam.rotation;

        _spriteTransform.rotation = Quaternion.Euler(rot.eulerAngles.x, _spriteTransform.rotation.eulerAngles.y, rot.eulerAngles.z);
    }

    private string SetFacing(float moveAngle)
    {
        while (moveAngle < 0)
        {
            moveAngle += 360;
        }
        
        if (moveAngle < 22.5 || moveAngle > 337.5)  return "_u";
        if (moveAngle < 67.5)   return "_ul";
        if (moveAngle < 112.5)  return "_l";
        if (moveAngle < 157.5)  return "_dl";
        if (moveAngle < 202.5)  return "_d";
        if (moveAngle < 247.5)  return "_dr";
        if (moveAngle < 292.5)  return "_r";
        if (moveAngle <= 337.5) return "_ur";

        return null;
    }

    protected abstract void SetAnimation();
}
