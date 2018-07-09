using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public string Name { get; private set; }
    public float Health { get; set; }
    public float Damage { get; private set; }
    private bool facingRight = true;
    private float cooldown;
    private float damage = 20;
  
    public void ApplyDamage(int damageAmount)
    {
        Health -= damageAmount;
    }

    public bool GroundDetection(Vector3 groundDetection, float distance)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection, Vector2.down, distance);
        return groundInfo;
    }

    public void Flip(Transform trnsfrm)
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = trnsfrm.localScale;
        theScale.x *= -1;
        trnsfrm.localScale = theScale;
    }

    public void Move(Transform trnsfrm, float speed)
    {
        if(facingRight)
        trnsfrm.Translate(Vector2.right * speed * Time.deltaTime);
        else
            trnsfrm.Translate(Vector2.left * speed * Time.deltaTime);

    }

    public bool Cooldown(float seconds)
    {

        if (Time.time >= cooldown)
        {
            cooldown = Time.time + seconds;
            return true;
        }
        return false;
    }

    public void Death(GameObject obj, float health)
    {
        if (health <= 0)
        {
            Destroy(obj);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlatformerCharacter2D>().health.Reduce(damage);
            CombatTextManager.Instance.CreateText(collision.gameObject.transform.position, new Vector3(0, 1, 0), 2, 0, 2, "-" + damage.ToString(), Color.red, CombatText.TextType.FeedbackText);
        }

    }
}
