using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator myanim;
    public ShootWeapon shootweapons;
    private bool directionRight = true,napWall;
    private bool onMoving, onMakeJump;
    public float buffSpeed = 0;

    private void Awake() {
        myanim = GetComponent<Animator>();   
    }

    public void OnDoAction(int actionId) {
    OnDoAction((ActionType)actionId);
    }
 
    public void OnDoAction(ActionType action) {
   // print("Выполнить действие: " + action);
    if (action == ActionType.MoveRight) 
    {
        this.onMoving = this.directionRight = true;
    } else 
        if (action == ActionType.MoveLeft) 
        {
            this.directionRight = false;
            this.onMoving = true;
        }
    else if (action == ActionType.None) 
        {
            this.onMoving = false;
        }
    else if (action == ActionType.Jump) 
        {
            if (CheckGround()) this.onMakeJump = true;
        }
    }

    private BoxCollider2D objCollider;
 
    public new BoxCollider2D collider {
    get {
        if (this.objCollider == null) this.objCollider = GetComponent<BoxCollider2D>();
            return this.objCollider;
    }
    }

    private bool CheckGround() {
    Bounds bounds = this.collider.bounds;
    Vector2 point = bounds.min;
    Vector2 size = bounds.size/1000;
    bool result = Physics2D.OverlapBox(point,new Vector2(0.01f,0.01f), 0.2f, 1 << 8);
    
    return result;
    }

    private bool CheckWall() {
    Bounds bounds = this.collider.bounds;
    Vector2 point = bounds.center;
    Vector2 size = bounds.size;
    bool result = Physics2D.OverlapBox(point,new Vector2(size.x+0.15f,size.y-0.1f), 0.2f, 1 << 8);
    if(!result){
        napWall = directionRight;
    }else{
        if(napWall != directionRight)
            result = false;
    }
    
    return result;
    }

    [Range(0, 10)]
    public float moveSpeed = 5;
    [Range(0, 20)]
    public float jumpForce = 10;
 
    private Rigidbody2D objBody;

 
    public Rigidbody2D body {
    get {
        if (this.objBody == null) this.objBody = GetComponent<Rigidbody2D>();
        return this.objBody;
    }
    }

    private void FixedUpdate() {
    if (this.onMoving && !CheckWall()) {  
        float xVelocity, yVelocity;

        if (this.directionRight) xVelocity = 1f;
            else xVelocity = -1f;

        transform.localScale = new Vector2(transform.localScale.y*-xVelocity,transform.localScale.y);
        float speed = this.moveSpeed * Time.fixedDeltaTime * 100f;
        xVelocity = xVelocity * (speed + buffSpeed);
        yVelocity = this.body.velocity.y;

        this.body.velocity = new Vector2(xVelocity, yVelocity);

        if(!this.onMakeJump)
            this.myanim.SetFloat("Horisontal",(moveSpeed+buffSpeed)/2);

    }else{
        this.myanim.SetFloat("Horisontal",0);
    }

    if (this.onMakeJump) 
    {
    float xVelocity = this.body.velocity.x/10;
    float yVelocity = this.jumpForce;
 
    Vector2 force = new Vector2(xVelocity, yVelocity);
    this.body.AddForce(force, ForceMode2D.Impulse);
 
    this.onMakeJump = false;
    }

    if(!CheckGround()){
        myanim.SetBool("Jump",true);
    }else
    {
        myanim.SetBool("Jump",false);
    }
    }

    
}
