using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private GameObject m_weaponPivot;

    //[HideInInspector] public Weapon currentWeapon = null;
    [HideInInspector] public CharacterStats stats;
    [SerializeField] private CameraShake m_camShake;

    [SerializeField] private ParticleSystem m_particles;
    [SerializeField] private Animator m_amimations;

    private void Start()
    {
        //currentWeapon = m_weaponPivot.transform.GetChild(0).GetComponent<Weapon>();
        stats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        if (GameManager.Instance.state == GameManager.State.RUNNING)
        {
            // Get mouse position In world space.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get input and move player
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 movement = input.normalized * m_speed * Time.deltaTime;
            transform.Translate(movement);
            m_amimations.SetBool("isRunning", (input != Vector2.zero));

            /*if (input != Vector2.zero)
            {
                m_particles.Play();
            }
            else
            {
                m_particles.Pause();
            } */

            // Change sprite and weapon direction
            FlipSpriteOnMouseX(mousePos);
            SetGunDirection();

            // Player Attack
            if (Input.GetMouseButtonDown(0))
            {
                if (stats.weapon != null)
                {
                    Vector2 dir = ((Vector2)mousePos - (Vector2)m_weaponPivot.transform.position).normalized;
                    Attack(stats.weapon, dir);
                }
            }

            // Temporary death stuff
            if (stats.health == 0)
            {
                GameManager.Instance.state = GameManager.State.GAMEOVER;
                Destroy(gameObject);
            }
        }
    }

    private void FlipSpriteOnMouseX(Vector2 mousePos)
    {
        Vector2 characterScale = transform.localScale;
        if (mousePos.x > transform.position.x)
        {
            characterScale.x = 1;
        }
        else
        {
            characterScale.x = -1;
        }
        transform.localScale = characterScale;
    }

    private void SetGunDirection()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 direction = (mousePos - (Vector2)Camera.main.WorldToScreenPoint(transform.position)).normalized;
        float angleDegree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Flip angle to the other side when crossing axis with mouse.
        if (angleDegree >= 90 || angleDegree <= -90)
        {
            angleDegree -= 180;
        }
        
        // Rotate weapons pivot object.
        m_weaponPivot.transform.rotation = Quaternion.AngleAxis(angleDegree, Vector3.forward);
    }

    private void Attack(Weapon weapon, Vector2 directionToAttack)
    {
        weapon.Fire(directionToAttack);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //EnemyController enemyCont = collision.GetComponent<EnemyController>();
            CharacterStats enemyStats = collision.GetComponent<CharacterStats>();
            stats.TakeDamage(enemyStats.GetDamage());
        }
    }
}
