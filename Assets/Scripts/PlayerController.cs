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
    public BoxCollider2D BoxColl;
    public CircleCollider2D CirColl;
    public LayerMask Ground;
    private bool IsTouchingBarrier = false;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnimator();
    }

    void Movement()
    {
        float HorizontalMove = Input.GetAxis("Horizontal");
        float FaceDircetion = Input.GetAxisRaw("Horizontal");
        float VerticalMove = Input.GetAxis("Y");

        //移动
        if (HorizontalMove != 0)                      //HorizontalMove !=0 则有移动
        {
            //默认以右移动，具体看HorizontalMove的值为正为负；
            RB2D.velocity = new Vector2(HorizontalMove * Speed * Time.deltaTime, RB2D.velocity.y);
            //rigidbody.velocity 速度向量

            /*（移动替代方法）
            * 移动，方向 * 上一帧花费的时间 * 玩家按键
            * transform.Translate(transform.right * Time.deltaTime * HorizontalMove * Speed);
            **/

            Animator.SetBool("idle", false);
            Animator.SetBool("run", true);
        }
        if (HorizontalMove == 0)
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
        if (Input.GetButtonDown("Jump") && Coll.IsTouchingLayers(Ground))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
            Animator.SetBool("jump", true);
        }

        //Crouch and Climb
        if (VerticalMove != 0)
        {
            if (VerticalMove < 0)
            {
                Animator.SetBool("crouch", true);
            }
            if (VerticalMove > 0)
            {
                Animator.SetBool("climb", true);
            }
        }
        if (VerticalMove == 0 && !IsTouchingBarrier)
        {
            Animator.SetBool("crouch", false);
        }

    }

    void SwitchAnimator()
    {
        //jump and fall
        if (Animator.GetBool("jump"))
        {
            if (RB2D.velocity.y < 0)            //juimp后fall
            {
                Animator.SetBool("jump", false);
                Animator.SetBool("fall", true);
            }
        }
        else
        {
            if (RB2D.velocity.y < 0)            //从平台上fall
            {
                Animator.SetBool("fall", true);
            }
            if (Coll.IsTouchingLayers(Ground))             //回归站立
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

        //crouch
        if (Animator.GetBool("crouch"))
        {
            if (IsTouchingBarrier)
            {
                Animator.SetBool("crouch", true);
                BoxColl.isTrigger = true;
            }
        }
        else
        {
            BoxColl.isTrigger  = false;
        }

        //climb
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collection")
        {
            UIController.Instance.CherryNum += 1;
            Destroy(collision.gameObject);
            //AudioManager.Instance.Play();
        }
        /*
        if(collision.gameObject.tag == "Collection1")
        {
            UIController.Instance.DiamondNum += 1;
            Destroy(collision.gameObject);


        }
        */

    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            IsTouchingBarrier = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            IsTouchingBarrier = true;
        }
    }
}