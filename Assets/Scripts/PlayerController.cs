using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float JumpForce; 
     private Rigidbody2D RB2D;          //[SerializeField]:  可将 成员变量 在Inspector中显示，并且定义Serialize关系;
    private Animator Animator;
    public Collider2D Coll;
    public LayerMask Ground;
    


    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        SwitchAnimator();
    }

    void Movement()
    {
        float HorizontalMove = Input.GetAxis("Horizontal");
        float FaceDircetion = Input.GetAxisRaw("Horizontal");

        //移动
        if (HorizontalMove != 0)                      //HorizontalMove !=0 则有移动
        {
            //默认以右移动，具体看HorizontalMove的值为正为负；
            RB2D.velocity = new Vector2(HorizontalMove * Speed * Time.deltaTime, RB2D.velocity.y);       //rigidbody.velocity 速度向量

            /**（移动替代方法）
            * 移动，方向 * 上一帧花费的时间 * 玩家按键
            * transform.Translate(transform.right * Time.deltaTime * HorizontalMove * speed);
            **/

            Animator.SetBool("idle", false);
            Animator.SetBool("run", true);
        }
        if(HorizontalMove == 0)
        {
            Animator.SetBool("run", false);
            Animator.SetBool("idle", true);
        }
        //转向
        if (FaceDircetion != 0)
        {
            transform.localScale = new Vector3(FaceDircetion, 1, 1);
        }

        //jump
        if (Input.GetButtonDown("Jump") && Coll.IsTouchingLayers(Ground) )
        {
            RB2D.velocity = new Vector2(RB2D.velocity.x, JumpForce * Time.deltaTime);
            Animator.SetBool("jump", true);
        }
        
        
        


    }
    void SwitchAnimator()
    {
        if (Animator.GetBool("jump"))
        {
            if (RB2D.velocity.y < 0)
            {
                Animator.SetBool("jump", false);
                Animator.SetBool("fall", true);
            }
        }
        else
        {   if (RB2D.velocity.y < 0)            //从平台上下落
            {
                Animator.SetBool("fall", true);
            }
            if (Coll.IsTouchingLayers(Ground) )
            {
                Animator.SetBool("fall", false);
                Animator.SetBool("idle", true);
            }
        }
        /**（这样也可以跳出fall）
        if( RB2D.velocity.y ==0 )
        {
            Animator.SetBool("fall", false);
            Animator.SetBool("idle", true);

        }
        */
    }







}