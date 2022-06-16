using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    private EnemyAbility enemyAbility;
    [SerializeField] private GameObject Item;
    void Start()
    {
        enemyAbility = GetComponentInParent<EnemyAbility>();
    }

    void DropItem()
    {
        var newItem = Instantiate(Item,this.transform.position,Quaternion.identity);
        newItem.GetComponent<Rigidbody2D>().AddForce(this.transform.up*2,ForceMode2D.Impulse);
    }
    void Update()
    {
        if (enemyAbility.currentHealth <= 0)
        {
            DropItem();
            Destroy(this.gameObject);
        }
    }
}
