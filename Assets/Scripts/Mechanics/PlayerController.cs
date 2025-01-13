using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 20;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        public Collider2D collider2d;
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        // Variabile pentru efectul de blitz
        public Color blitzColor = Color.white;
        public float blitzDuration = 0.5f;
        private bool isBlitzActive = false;
        private Color originalColor;

        // Variabilă pentru starea scutului
        public bool isShieldActive = false;

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            // Salvează culoarea originală a sprite-ului
            originalColor = spriteRenderer.color;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x));

            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
                animator.SetTrigger("jump"); // Activează animația de săritură
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y *= 0.5f;
                }
            }

            // Aplicație de gravitație suplimentară la cădere
            if (velocity.y < 0)
            {
                velocity.y += Physics2D.gravity.y * 2 * Time.deltaTime;
            }

            // Controlează direcția sprite-ului în funcție de direcția de mers
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            // Actualizează parametrii animației
            animator.SetBool("grounded", IsGrounded);                        // Setează dacă este pe sol
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed); // Setează viteza orizontală

            targetVelocity = move * maxSpeed;
        }



        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Slime"))
            {
                // Verifică dacă scutul este activ. Dacă da, nu activa efectul de blitz
                if (isShieldActive)
                {
                    Debug.Log("Shield is active. Blitz effect will not trigger.");
                    return;
                }

                // Activează efectul de blitz dacă blitz-ul nu este deja activ
                if (!isBlitzActive)
                {
                    StartCoroutine(ActivateBlitzEffect());
                    Debug.Log("Player has triggered with a Slime!");
                }
            }
        }





        /// <summary>
        /// Corutină pentru a activa un efect vizual de blitz.
        /// </summary>
        IEnumerator ActivateBlitzEffect()
        {
            isBlitzActive = true;

            // Schimbă culoarea sprite-ului în blitzColor
            spriteRenderer.color = blitzColor;

            // Așteaptă durata efectului de blitz
            yield return new WaitForSeconds(blitzDuration);

            // Resetează culoarea sprite-ului la culoarea originală
            spriteRenderer.color = originalColor;

            isBlitzActive = false;
        }

        /// <summary>
        /// Activează scutul pentru o anumită durată.
        /// </summary>
        public void ActivateShield(float duration)
        {
            StartCoroutine(ShieldCoroutine(duration));
        }

        private IEnumerator ShieldCoroutine(float duration)
        {
            isShieldActive = true;
            Debug.Log("Shield activated.");

            yield return new WaitForSeconds(duration);

            isShieldActive = false;
            Debug.Log("Shield deactivated.");
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}
