using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    const bool left = false;
    const bool right = true;
    public Animator myanim;
    public ShootWeapon shootweapons;
    private bool direction = right,napWall;
    private bool onMoving, onMakeJump;
    public float buffSpeed = 0;

    [Range(0, 10)]
    public float moveSpeed = 5;
    [Range(0, 20)]
    public float jumpForce = 10;
 
    private Rigidbody2D objBody;
    private BoxCollider2D objCollider;

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
        this.direction= right;
        this.onMoving = true;
    } else 
        if (action == ActionType.MoveLeft) 
        {
            this.direction = left;
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

    public new BoxCollider2D collider {
    get {
        if (this.objCollider == null) this.objCollider = GetComponent<BoxCollider2D>();
            return this.objCollider;
    }
    }

    private bool CheckGround() {
    bool result = false;
    int layerMask = 1 << 8;

    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(-Vector2.up), 0.1f, layerMask);
    
    if (hit)
    {    
        result = true;
    }

    return result;
    }

    private bool CheckWall() {
    Bounds bounds = this.collider.bounds;
    Vector2 point = bounds.center;
    Vector2 size = bounds.size;
    bool result = Physics2D.OverlapBox(new Vector2(point.x,point.y+0.2f),new Vector2(size.x,size.y-0.4f), 0f, 1 << 8);
    if(!result){
        napWall = direction;
    }else{
        if(napWall != direction)
            result = false;
    }
    
    return result;
    }

 
    public Rigidbody2D body {
    get {
        if (this.objBody == null) this.objBody = GetComponent<Rigidbody2D>();
        return this.objBody;
    }
    }

    private void FixedUpdate() {
    if (this.onMoving && !CheckWall()) {  
        float xVelocity, yVelocity;

        if (this.direction) xVelocity = 1f;
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
