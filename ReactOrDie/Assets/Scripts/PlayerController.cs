using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private GameObject m_weaponPivot;
    
    private enum FacingDirection { Up, Down, Left, Right}
    private FacingDirection m_direction;

    [HideInInspector] public Weapon currentWeapon = null;
    [HideInInspector] public CharacterStats stats;

    private void Start()
    {
        m_direction = FacingDirection.Right;
        currentWeapon = m_weaponPivot.transform.GetChild(0).GetComponent<Weapon>();
        stats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        // Get mouse position In world space.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Get input and move player
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 movement = input.normalized * m_speed * Time.deltaTime;
        transform.Translate(movement);

        // Change sprite and weapon direction
        FlipSpriteOnMouseX(mousePos);
        SetGunDirection();

        // Player Attack
        if (Input.GetMouseButtonDown(0))
        {
            if (currentWeapon != null)
            {
                Vector2 dir = ((Vector2)mousePos - (Vector2)m_weaponPivot.transform.position).normalized;
                Attack(currentWeapon, dir);
            }
        }

        // Temporary death stuff
        if (stats.health == 0)
        {
            SceneManager.LoadScene(0);
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

    private Vector2 GetDirectionVector()
    {
        Vector2 directionToAttack = Vector2.left;
        switch (m_direction)
        {
            case FacingDirection.Up:
                directionToAttack = Vector2.up;
                break;
            case FacingDirection.Down:
                directionToAttack = Vector2.down;
                break;
            case FacingDirection.Right:
                directionToAttack = Vector2.right;
                break;
            case FacingDirection.Left:
                directionToAttack = Vector2.left;
                break;
        } 

        return directionToAttack;
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
            EnemyController enemyCont = collision.GetComponent<EnemyController>();
            CharacterStats enemyStats = enemyCont.stats;
            stats.TakeDamage(enemyStats.GetDamage(enemyCont.currentWeapon));
            Debug.Log("Player Health: " + stats.health);
        }
    }
}
