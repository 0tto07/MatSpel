
using UnityEngine;

public class TestSize : MonoBehaviour
{
    public StatApplier applier;
    public Rigidbody2D rb;
    bool Started;


    // Start is called before the first frame update
    void Start()
    {
        Started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Started == true)
        {
            transform.localScale = new Vector2(transform.localScale.x / applier.scaleMod, transform.localScale.y / applier.scaleMod);
            Started = false;
        }
   
    }
}
