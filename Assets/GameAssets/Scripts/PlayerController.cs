using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Character2DController cController;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int jumpdamage;
    [SerializeField]
    float hitGodModeDuration;
    public int AttackDamage;
    [HideInInspector]
    public bool canMove= true; //controla cuando puede moverse por si quiero bloquear el personaje
  //  [SerializeField] 
   // GameObject attackPoint;
    [HideInInspector]
    public bool hasHit=false;
    float horizontal;
    bool isJumping;
    
    Animator anim;
    Rigidbody2D rigi;
    bool godMode;
   public bool canDoubleJump;
    bool canAttack = true;
     public bool doubleJumpLocked=true;
    

    // Start is called before the first frame update
    void Start()
    {
       // attackPoint.SetActive(false);
        cController = GetComponent<Character2DController>();
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
        horizontal = Input.GetAxisRaw("Horizontal");
        if (canMove) {anim.SetFloat("Speed", Mathf.Abs(horizontal));
        }
        else { anim.SetFloat("Speed", Mathf.Abs(0)); }
        anim.SetFloat("VelY", rigi.velocity.y);
        anim.SetBool("IsGrounded", cController.GetIsGround());

        // si esta parado o corriendo , puede atacar
      /*  if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            canAttack = true;
        }*/


        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canMove)
            {
                Attack();
            }
        }


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.UpArrow) && canMove)
        {
            
            {

                if (cController.GetIsGround())
                {
                    isJumping = true;
                    canDoubleJump = true;
                  //  GameManager.instance.GetComponent<SoundManager>().SoundJump();
                }
                else if (canDoubleJump)
                {
                    if (!doubleJumpLocked)
                    {
                        canDoubleJump = false;
                        cController.Jump();
                    }
                }
            }
        }

        if (godMode)
        {
            Blink();
        }
        else
        {
            ChangePlayerAlpha(1);
        }
        

    }

    private void FixedUpdate()
    {//SI PUEDE, SE MUEVE
        if (canMove)
        {
            cController.Move(horizontal * Time.deltaTime * moveSpeed, false, isJumping);
        }
        else
        {
            cController.Move(0, false, isJumping);
        }
    }

    public void OnGround()
    {
        isJumping = false;
        canDoubleJump = true;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
       /* if (col.collider.tag == "Enemy")
        {
            if (JmpAttack(col.transform.position))
            {
                cController.Jump();
                col.collider.GetComponent<Health>().LoseHealth(jumpdamage);
            }
            else
            {
                
                GetDamage(col.collider.GetComponent<Enemy>().GetDamage());
            }
        }*/
    }

    bool JmpAttack(Vector2 posiEnemy)
    {
        if (posiEnemy.y < transform.position.y && rigi.velocity.y < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActiveGodMode(float duration)
    {
        if (!godMode)
        {
            StartCoroutine(GodModeCorr(duration));
        }
    }

    IEnumerator GodModeCorr(float time)
    {
        godMode = true;
        yield return new WaitForSeconds(time);
        godMode = false;
    }

    void Blink()
    {
        ChangePlayerAlpha(Mathf.PingPong(Time.time * 5, 1) + 0.3f);
    }

    void ChangePlayerAlpha(float value)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color myNewColor = sr.color;
        myNewColor.a = value;
        sr.color = myNewColor;
    }

    public void GetDamage(int damage)
    {
      /*  if (!godMode)
        {
            GameManager.instance.GetComponent<SoundManager>().SoundPlayerGetDamage();
            GetComponent<Health>().LoseHealth(damage);
            ActiveGodMode(hitGodModeDuration);
        }*/
    }


    void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
           // GameManager.instance.GetComponent<SoundManager>().SoundPlayerAttack();
            StartCoroutine(ReloadAttack());
        }
    }

    IEnumerator ReloadAttack()
    {
         anim.SetTrigger("Attack");
       
         yield return new WaitForSeconds(0.3f);
        canAttack = true;
        hasHit = false;
        
        
    }

    public void UnlockDoubleJump() { doubleJumpLocked = false; }

}
