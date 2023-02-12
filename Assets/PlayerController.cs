using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public float dashSpeed;
    public float dashDistance;
    public float dashMaxCD;

    public float fallFloor; //where player can fall to before getting reset

    public int attack1Dmg;
    public int attack2Dmg;
    public int attack3Dmg;
    public int attackUpDmg;
    public int attackDownDmg;

    // public float attack1MaxCD;
    // public float attack2MaxCD;
    public float attack3MaxCD;
    public float attackUpMaxCD;
    public float attackDownMaxCD;
    public float attackChainTimerMax;

    public float bouncePercent;

    private bool dashOnCD;
    private bool attackOnCD;
    private bool myJump = false;
    private bool myDash = false;
    private bool myAttack = false;
    private float myHorizontal;
    private float attackChainCount = 0;
    private float attackChainTimerCur;
    private RaycastHit2D enemyHit;

    public int maxJumpCount;

    private int jumpCount;
    private float jumpTime;
    private bool releaseJump;

    public Vector3 boxSize;
    public Vector3 sideAttackSize;
    public Vector3 upAttackSize;
    public Vector3 downAttackSize;
    public float maxDistance;
    public LayerMask layerFloor;
    public LayerMask layerEnemy;

    private Rigidbody2D myRB;
    private SpriteRenderer mySR;

    public Animator myAnim;

    public static bool readInput;

    //Used for gizmo checking
    private float tempHor;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        jumpCount = maxJumpCount;
        readInput = true;
    }
    // Update is called once per frame

    private void Update()
    {
        //check if we are reading inputs
        if (readInput)
        {
            myHorizontal = Input.GetAxisRaw("Horizontal");

            //if user hits lmb attack
            if (Input.GetButton("Fire1") && !attackOnCD)
            {
                if (!myDash && !myAttack)
                    Attack();
            }

            //TODO: add addtional check for this
            if( Input.GetButton("Fire2")){
                if (!myDash && !myAttack)
                    fireBall();
            }

            //if user hits space jump
            if (Input.GetButtonDown("Jump"))
            {
                myJump = true;
            }
            if (Input.GetButtonUp("Jump"))
            {
                releaseJump = true;
            }

            //if users hits shift dash
            if (Input.GetButtonDown("Fire3") && !dashOnCD)
            { //Left Shift - Dashing
                if (!myDash && !myAttack)
                    StartCoroutine(Dash(myHorizontal));
            }
        }
    }
    private void FixedUpdate()
    {
        //if we are not reading input don't update anything based on input values
        if (readInput)
        {
            //check if player is grounded
            isGrounded();

            // don't use movement velocity while dashing or attacking
            if (!myDash && !myAttack)
                //TODO: Doesn't work all the time on running hitbox
                //check if against wall and stop movement (player gets stuck on it otherwise)
                if (Physics2D.BoxCast(new Vector2(transform.position.x + (.25f * myHorizontal), transform.position.y + .05f), new Vector2(.1f, .52f), 0, transform.forward, maxDistance, layerFloor))
                {
                    myRB.velocity = new Vector2(0f, myRB.velocity.y);
                }
                else
                {
                    myRB.velocity = new Vector2(myHorizontal * speed, myRB.velocity.y);
                    //flips sprite and starts running animation
                    switch (myHorizontal)
                    {
                        case 1:
                            myAnim.SetBool("Running", true);
                            mySR.flipX = false;
                            break;
                        case -1:
                            myAnim.SetBool("Running", true);
                            mySR.flipX = true;
                            break;
                        default:
                            myAnim.SetBool("Running", false);
                            break;
                    }
                }

            //if user hits jump and is not attacking or dashing then jump
            if (myJump == true && !myDash && !myAttack)
            {
                releaseJump = false;
                if (jumpCount > 0)
                {
                    jumpTime = 0;
                    myAnim.SetFloat("Jump Time", jumpTime);
                    myAnim.SetBool("Jumping", true);
                    myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
                    myJump = false;
                    --jumpCount;
                }
                else
                {
                    myJump = false;
                }
            }
            //TODO: Make this feel better
            if (releaseJump && !myAttack && myRB.velocity.y > 0 && jumpTime > .15f)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, 0);
                releaseJump = false;
            }
            else if (releaseJump && myRB.velocity.y < 0)
            {
                releaseJump = false;
            }

            if (transform.position.y < fallFloor)
            {
                //change kill later on
                //would call the player health class
                transform.position = Vector3.zero;
            }

            //check if player is in attack chain
            if (attackChainCount > 0)
            {
                attackChainTimerCur += Time.deltaTime;
                if (attackChainTimerCur >= attackChainTimerMax)
                {
                    //reset attack chain counter
                    attackChainCount = 0;
                    //reset attack chain timer
                    attackChainTimerCur = 0;
                }
            }
        }
    }

    //checks if user is grounded and resets animations and updates jumps
    private bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerFloor))
        {
            jumpTime = 0;
            myAnim.SetFloat("Jump Time", jumpTime);
            myAnim.SetBool("Jumping", false);
            //Debug.Log("On Ground");
            jumpCount = maxJumpCount;
            return true;
        }
        else
        {
            myAnim.SetFloat("Jump Time", jumpTime);
            jumpTime += Time.deltaTime;
            return false;
        }
    }

    IEnumerator Dash(float direction)
    {
        myAnim.SetBool("Dashing", true);
        //Debug.Log("Dashing");
        myDash = true;
        myRB.velocity = new Vector2(myRB.velocity.x, 0f);
        myRB.AddForce(new Vector2(dashDistance * myHorizontal, 0f), ForceMode2D.Impulse);
        float gravity = myRB.gravityScale;
        myRB.gravityScale = 0;
        yield return new WaitForSeconds(.3f);
        myDash = false;
        myRB.gravityScale = gravity;
        dashOnCD = true;
        myAnim.SetBool("Dashing", false);
        yield return new WaitForSeconds(dashMaxCD);
        dashOnCD = false;
    }

    //maybe move all attack functions to new script?
    private void Attack()
    {
        switch (Input.GetAxisRaw("Vertical"))
        {
            case 1:
                StartCoroutine(UpAttack());
                break;
            case -1:
                StartCoroutine(DownAttack());
                break;
            default:
                StartCoroutine(SideAttack(myHorizontal));
                break;

        }

    }

    private void fireBall(){
        //Do the thing
    }

    //If enemy is hit sends damage to them
    private void SendDamage(Transform enemyTransform, int dmg)
    {
        enemyTransform.SendMessage("takeDamge", dmg);
    }

    //TODO: Seperate out 3 different side attacks and dmg
    IEnumerator SideAttack(float direction)
    {
        Debug.Log("Side Attacking " + attackChainCount);
        myAnim.SetBool($"Side Attack {attackChainCount + 1}", true);
        myAttack = true;
        Vector2 prevVel = myRB.velocity;
        myRB.velocity = Vector2.zero;
        float gravity = myRB.gravityScale;
        myRB.gravityScale = 0;
        tempHor = myHorizontal;
        enemyHit = Physics2D.BoxCast(new Vector2(transform.position.x + myHorizontal, transform.position.y), sideAttackSize, 0, transform.forward, maxDistance, layerEnemy);
        if (enemyHit)
        {
            Debug.Log("hit side");
            if (attackChainCount == 2)
            {
                SendDamage(enemyHit.transform, attack1Dmg);
            }
            else
            {
                SendDamage(enemyHit.transform, attack3Dmg);
            }
        }
        yield return new WaitForSeconds(0.2f); //animation time?
        myAttack = false;
        myRB.velocity = prevVel;
        myRB.gravityScale = gravity;

        myAnim.SetBool($"Side Attack {attackChainCount + 1}", false);
        if (attackChainCount == 2)
        {
            attackOnCD = true;
            yield return new WaitForSeconds(attack3MaxCD);
            attackOnCD = false;
        }
        attackChainCount = ((attackChainCount + 1) % 3);
        attackChainTimerCur = 0;
    }

    IEnumerator UpAttack()
    {
        Debug.Log("Up Attacking");
        myAnim.SetBool("Up Attack", true);
        myAttack = true;
        Vector2 prevVel = myRB.velocity;
        myRB.velocity = Vector2.zero;
        float gravity = myRB.gravityScale;
        myRB.gravityScale = 0;
        tempHor = myHorizontal;
        enemyHit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + .9f), upAttackSize, 0, transform.forward, maxDistance, layerEnemy);
        if (enemyHit)
        {
            Debug.Log("hit up");
            SendDamage(enemyHit.transform, attackUpDmg);
        }
        yield return new WaitForSeconds(0.1f); //animation time?
        myAttack = false;
        myRB.velocity = prevVel;
        myRB.gravityScale = gravity;
        myAnim.SetBool("Up Attack", false);

        attackOnCD = true;
        yield return new WaitForSeconds(attackUpMaxCD);
        attackOnCD = false;
    }

    IEnumerator DownAttack()
    {
        if (!isGrounded())
        {
            Debug.Log("Down Attacking");
            myAnim.SetBool("Down Attack", true);
            myAttack = true;
            Vector2 prevVel = myRB.velocity;
            myRB.velocity = Vector2.zero;
            float gravity = myRB.gravityScale;
            myRB.gravityScale = 0;
            enemyHit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y - .9f), downAttackSize, 0, transform.forward, maxDistance, layerEnemy);
            if (enemyHit)
            {
                Debug.Log("hit down");
                SendDamage(enemyHit.transform, attackDownDmg);
                myRB.velocity = new Vector2(prevVel.x, jumpForce * bouncePercent);
            }
            else
            {
                myRB.velocity = prevVel;
            }
            yield return new WaitForSeconds(0.2f); //animation time?
            myAttack = false;
            myRB.gravityScale = gravity;
            myAnim.SetBool("Down Attack", false);

            attackOnCD = true;
            yield return new WaitForSeconds(attackDownMaxCD);
            attackOnCD = false;
        }
        else
        {
            yield return false;
        }

    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
        // Gizmos.DrawCube(tempVec-transform.forward*maxDistance, sideAttackSize);
        Gizmos.color = Color.red;
        // Vector3 tempVec = new Vector3(transform.position.x + tempHor, transform.position.y,0f);
        // Gizmos.DrawCube(tempVec-transform.forward*maxDistance, sideAttackSize);

        // Vector3 tempVec2 = new Vector2(transform.position.x, transform.position.y + .9f);
        // Gizmos.DrawCube(tempVec2-transform.forward*maxDistance, upAttackSize);

        // Vector3 tempVec3 = new Vector2(transform.position.x, transform.position.y - .9f);
        // Gizmos.DrawCube(tempVec3-transform.forward*maxDistance, downAttackSize);
    }
}
