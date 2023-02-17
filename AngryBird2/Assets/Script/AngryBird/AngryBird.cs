using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class AngryBird : MonoBehaviour
{

    public StateAngryBird stateAngryBird;
    public GameObject targetPrefabs;
    public Vector3 target;
    public float _speed;
    [Header("Animation")]
    public Animator animator;
    [Header("Seek Location Start")]
    Vector3 locStart;// dang mac dinh san trong Game -> sau la vi tri giua cua cai sung 

    [Header("Audio")]
    public AudioSource auSourceFly;
    public AudioSource auSourceBlock;
    private void Start()
    {
        GameController.instance.stateAngryBird = Enums.StateAngryBird.Idle;
        locStart = Slingshoot.instance.Mid_locationAngryBirdStart;//! khi nao co controller thi dieu khien sau 
        transform.position = locStart;
        stateAngryBird = StateAngryBird.Idle;
    }

    public Vector3 mousePosition;
    // Khi chuot nhan giu vao  AngryBird 
    private void OnMouseDrag()
    {

        if (GameController.instance.stateGame == Enums.StateGame.Start && stateAngryBird == StateAngryBird.Idle)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePosition.x < -4.5f)
            {
                transform.position = new Vector3(mousePosition.x, mousePosition.y);
            }
            // Slingshoot 
            GameController.instance.stateSlingshoot = Enums.StateSlingshot.BeforeShoot;
        }
    }

    private void OnMouseUp()
    {
        Vector3 dis = locStart - transform.position;
        Vector3 target = transform.position + 4 * dis;
        Vector3 velocity = target - transform.position;

        // Angry Bird chi ban khi ma vi tri cua Angry Bird va vi tri (mid) cua sung > 1f nguoc lai thi se khong bay
        if (Vector3.Distance(locStart, transform.position) > 1f && Vector3.Distance(locStart, transform.position) < 2)
        {
            stateAngryBird = StateAngryBird.Fly;
            GameController.instance.stateSlingshoot = Enums.StateSlingshot.Shoot;

            FlyAngryBird(velocity);
            StartCoroutine(OnDestroyAngryBird());
        }
        else
        {
            if (stateAngryBird == StateAngryBird.Idle)
            {
                transform.position = locStart;
                GameController.instance.stateSlingshoot = Enums.StateSlingshot.BeforeShoot;
            }
            // if(stateAngryBird )

        }
    }

    public void FlyAngryBird(Vector3 _velocity)
    {
        auSourceFly.Play();
        GetComponent<Rigidbody2D>().AddForce((_velocity) * _speed);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    IEnumerator OnDestroyAngryBird()
    {
        yield return new WaitForSeconds(4);
        GameController.instance.stateSlingshoot = Enums.StateSlingshot.AfterShoot;
        GameController.instance.stateAngryBird = Enums.StateAngryBird.Died;
        Destroy(gameObject);
    }

    private void Update()
    {
        AniAngryBird();
        auSourceBlock.volume = GameManager.instance.data._volume;
        auSourceFly.volume = GameManager.instance.data._volume;
    }
    public void AniAngryBird()
    {
        if (stateAngryBird == StateAngryBird.Idle)
        {
            animator.SetBool("BirdFly", false);
        }
        if (stateAngryBird == StateAngryBird.Fly)
        {
            animator.SetBool("BirdFly", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("block"))
        {
            auSourceBlock.Play();
        }
    }
}
