using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Demon_Controller : MonoBehaviour
{
    [Header("World Variables")]
    GameObject toTod;
    GameObject player;
    Area2_Manager a2Manager;

    [Header("Visual Variables")]
    public Slider hpSlider;
    public ParticleSystem lifeEssenceEffect;
    public ParticleSystem deathParticleSystem;
    bool isDying = false;
    Animator demonAnimator;

    [Header("Attack Variables")]
    public float distance2Attack;
    GameObject object2Attack;

    public int attackDamage;
    public int frames2Attack;
    int attackFrameCounter;

    [Header("AI Variables")]
    public NavMeshAgent demonAgent;

    Demon demonStatistics;
    DemonGame_Controller dgController;

    // Start is called before the first frame update
    void Start()
    {       
        WeakDemon weakFactory = new WeakDemon();
        demonStatistics = weakFactory.buildDemon("Base");

        toTod = GameObject.FindGameObjectWithTag("ThoughtOfTod");
        player = GameObject.FindGameObjectWithTag("Player");
        a2Manager = GameObject.FindGameObjectWithTag("CameraController").GetComponent<Area2_Manager>();

        demonAnimator = GetComponent<Animator>();

        demonAgent.speed = demonStatistics.MovementSpeed;
        lifeEssenceEffect.Pause();

        object2Attack = toTod;

        dgController = GameObject.FindGameObjectWithTag("DemonGameManager").GetComponent<DemonGame_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        demonBehaviour();
        demonDeath();
    }

    void demonBehaviour()
    {
        if (destinationDistance(object2Attack) < distance2Attack)
        {
            demonAgent.SetDestination(transform.position);

            demonAnimator.SetTrigger("Attack");
            lifeEssenceEffect.Play();

            demonAttack();
        }
        else
        {
            demonAnimator.SetTrigger("Float");  
            demonAgent.SetDestination(object2Attack.transform.position);
        }
    }

    void demonAttack()
    {
        if (attackFrameCounter < frames2Attack)
        {
            attackFrameCounter++;
        }
        else
        {
            a2Manager.todHealth -= attackDamage;
            attackFrameCounter = 0;
        }
    }

    void demonDeath()
    {
        if (isDying == true)
        {
            if (deathParticleSystem.IsAlive() == false)
            {
                dgController.demonsAlive--;
                Destroy(gameObject);
            }
        }

        if (demonStatistics.HP <= 0)
        {
            if (deathParticleSystem.IsAlive() == false)
            {
                deathParticleSystem.Play();
            }

            isDying = true;
        }
    }

    float destinationDistance(GameObject destination)
    {
        return Vector3.Distance(transform.position, destination.transform.position);     
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            demonStatistics.HP = demonStatistics.HP - 20;
            hpSlider.value = demonStatistics.HP;

            //Destroy(collision.gameObject);
            Destroy(collision.transform.parent.gameObject);
        }
        else if (collision.gameObject.tag == "PlayerFists")
        {
            demonStatistics.HP = demonStatistics.HP - 10;
            hpSlider.value = demonStatistics.HP;
        }
    }
}
