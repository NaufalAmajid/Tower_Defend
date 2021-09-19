using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour

{
    private Tower _placedTower;
 
    //METHOD THAT IS CALLED ONCE WHEN A RIGIDBODY OBJECT TOUCHES THE COLLIDER AREA
    private void OnTriggerEnter2D (Collider2D collision)
    {

        if(_placedTower != null)
        {

            return;

        }

        Tower tower = collision.GetComponent<Tower> ();

        if (tower != null)
        {

            tower.SetPlacePosition (transform.position);

            _placedTower = tower;

        }

    }

    //CONCRARY OF OnTriggerEnter2D, THIS METHOD IS CALLED ONCE WHEN THE OBJECT LEAVES THE COLLIDER AREA
    private void OnTriggerExit2D(Collider2D collision)
    {

        if(_placedTower == null)
        {

            return;

        }

        _placedTower.SetPlacePosition (null);

        _placedTower = null;

    }

}
