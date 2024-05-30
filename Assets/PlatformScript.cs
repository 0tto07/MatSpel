using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow2D enemyComponent = other.GetComponent<EnemyFollow2D>();
            if (enemyComponent != null)
            {
                enemyComponent.OnPlatform = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow2D enemyComponent = other.GetComponent<EnemyFollow2D>();
            if (enemyComponent != null)
            {
                enemyComponent.OnPlatform = false;
                StartCoroutine(DestroyEnemyAfterDelay(enemyComponent.gameObject));
            }
        }
    }

    private IEnumerator DestroyEnemyAfterDelay(GameObject enemy)
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay as needed

        EnemyFollow2D enemyComponent = enemy.GetComponent<EnemyFollow2D>();
        if (enemyComponent != null && !enemyComponent.OnPlatform)
        {
            Destroy(enemy);
        }
    }
}


