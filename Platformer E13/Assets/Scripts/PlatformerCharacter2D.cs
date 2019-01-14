using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
    public GameObject deathPoint, corpse, camera2D;
    public PostProcessingProfile profile;
    public Sprite spirit;

    [SerializeField]
    public Stat health;

    [SerializeField]
    public Stat energy;

    [SerializeField]
    public Stat ammo;

    [SerializeField]
    public Stat momentum;

    [SerializeField ]private Transform playerCollider;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded, space = false;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool died = false;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.    
    public AudioClip jumpSound;

    private void Awake()
    {
        // Setting up references.
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        health.Initialize();
        energy.Initialize();
        ammo.Initialize();
    }


    private void FixedUpdate()
    {
        m_Grounded = false;
        DeathCheck();
        energy.Regen(1, 1);
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, m_WhatIsGround);
        if(hit.collider != null)
        { m_Grounded = true; }
        m_Anim.SetBool("Ground", m_Grounded);
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }


    public void Move(float move, float vmove, bool crouch, bool roll, bool jump, bool run)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            m_Anim.SetFloat("Speed", Mathf.Abs(move));
            m_Anim.SetFloat("vSpeed", Mathf.Abs(vmove));
            m_Anim.SetBool("Run", run);
            // Move the character
            if (space)
            {
                if (run && m_Grounded)
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed * 1.5f, vmove * m_MaxSpeed);
                else
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, vmove * m_MaxSpeed);
            }
            else
            {
                
                if (run && m_Grounded)
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed * 1.5f, m_Rigidbody2D.velocity.y);
                else
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
            }
            //If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump )
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            //SoundManager.instance.PlaySingle(jumpSound);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void DeathCheck()
    {
        if ((deathPoint != null && transform.position.y <= deathPoint.transform.position.y || health.CurrentVal <= 0) && !died)
        {
            //var body = Instantiate(corpse, transform.position, Quaternion.identity);
            //GetComponent<SpriteRenderer>().sprite = spirit;
            //camera2D.GetComponent<PostProcessingBehaviour>().profile = profile;
            //died = true;
            ManagerForScenes.Instance.ResetLevel();
        }
    }
}

