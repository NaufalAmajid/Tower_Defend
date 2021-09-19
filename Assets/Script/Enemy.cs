using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //ENEMY PROPERTY
    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private SpriteRenderer _healthBar;
    [SerializeField] private SpriteRenderer _healthFill;

    private int _currentHealth;

    //FOLLOWING PATH
    public Vector3 TargetPosition{ get; private set; }
    public int CurrentPathIndex{ get; private set; }

    //THIS METHOD IS CALLED ONCE EVERY TIME YOU TURN ON A GAME OBJECT THAT HAS THIS SCRIPT
    private void OnEnable()
    {

        _currentHealth = _maxHealth;

        _healthFill.size = _healthBar.size;

    }

    //FOLLOWING PATH
    public void MoveToTarget()
    {

        transform.position = Vector3.MoveTowards (transform.position, TargetPosition, _moveSpeed * Time.deltaTime);

    }

    public void SetTargetPosition(Vector3 targetPosition)
    {

        TargetPosition = targetPosition;

        _healthBar.transform.parent = null;

        //CHANGE ROTATION OF ENEMY
        Vector3 distance = TargetPosition - transform.position;

        if(Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
        {
            //FACING UP
            if(distance.y > 0)
            {

                transform.rotation = Quaternion.Euler (new Vector3(0f, 0f, 90f));

            }
            //FACING DOWN
            else
            {

                transform.rotation = Quaternion.Euler (new Vector3(0f, 0f, -90f));

            }

        }
        else
        {

            //FACING RIGHT (DEFAULT)
            if(distance.x > 0)
            {

                transform.rotation = Quaternion.Euler (new Vector3(0f, 0f, 0f));

            }
            //FACING LEFT
            else
            {

                transform.rotation = Quaternion.Euler (new Vector3(0f, 0f, 180f));

            }

        }

        _healthBar.transform.parent = transform;

    }

    //MARK THE LAST INDEX ON THE PATCH
    public void SetCurrentPathIndex(int currentIndex)
    {

        CurrentPathIndex = currentIndex;

    }

    //DESTROY ENEMY
    public void ReduceEnemyHealth(int damage)
    {

        _currentHealth -= damage;

        AudioPlayer.Instance.PlaySFX ("hit-enemy");

        if(_currentHealth <= 0)
        {

            gameObject.SetActive (false);

            AudioPlayer.Instance.PlaySFX ("enemy-die");

        }

        float healthPercentage = (float) _currentHealth / _maxHealth;

        _healthFill.size = new Vector2 (healthPercentage * _healthBar.size.x, _healthBar.size.y);

    }

}
