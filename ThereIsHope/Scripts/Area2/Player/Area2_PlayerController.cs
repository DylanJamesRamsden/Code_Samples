using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area2_PlayerController : MonoBehaviour
{

    [Header("Movement Stats")]
    public float walkSpeed;
    public float runSpeed;

    public Animator playerAnimator;

    [Header("Global State Variables")]
    public bool IsPlayerAlive = true;

    [Header("Diary:")]
    public GameObject Diary;
    public Area2_DiaryController pDiary;
    //public Area2_Manager a2manager;

    [Header("Attack Variables")]
    public GameObject[] Projectiles;
    public GameObject[] SpawnObjectPositions;
    public GameObject[] AimPositions;
    public SphereCollider playerFists;

    bool canChangeAnimation = true;
    int bulletsShot = 0;
    public ParticleSystem shootParticleSystem;
    bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        playerFists.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pDiary.isOpen == false)
        {
            if (isAttacking == false)
            {
                MovementControls();
            }

            AttackController();

            if (canChangeAnimation == true)
            {
                AnimationControls();
            }
        }

        diaryController();
    }

    void MovementControls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.localRotation = Quaternion.Euler(0, 45, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.localRotation = Quaternion.Euler(0, 315, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.localRotation = Quaternion.Euler(0, 135, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.localRotation = Quaternion.Euler(0, 225, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(0, 270, 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
        }
    }

    void AnimationControls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetTrigger("Run");
            }
            else
            {
                playerAnimator.SetTrigger("Walk");
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetTrigger("Run");
            }
            else
            {
                playerAnimator.SetTrigger("Walk");
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetTrigger("Run");
            }
            else
            {
                playerAnimator.SetTrigger("Walk");
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetTrigger("Run");
            }
            else
            {
                playerAnimator.SetTrigger("Walk");
            }
        }
        else
        {
            playerAnimator.SetTrigger("Idle");
        }
    }

    void AttackController()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerAnimator.SetTrigger("Punch");

            playerFists.enabled = true;

            canChangeAnimation = false;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            playerAnimator.SetTrigger("Shoot");

            canChangeAnimation = false;

            isAttacking = true;
        }
    }

    void diaryController()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Diary.activeSelf == true)
            {
                if (pDiary.diaryType == "")
                {
                    pDiary.hideObjectiveUI();
                    Diary.SetActive(false);
                    pDiary.isOpen = false;
                }
            }
            else
            {
                Diary.SetActive(true);
                pDiary.isOpen = true;
                playerAnimator.SetTrigger("Idle");
            }
        }
    }

    public void finishedPunch()
    {
        canChangeAnimation = true;

        playerFists.enabled = false;
    }

    public void finishedShoot()
    {
        canChangeAnimation = true;

        shootParticleSystem.Play();

        if (bulletsShot < 3)
        {
            GameObject bullet;

            int bullet2Shoot = Random.Range(0, 4);

            switch (bullet2Shoot)
            {
                case 0:
                    bullet = Instantiate(Projectiles[0], SpawnObjectPositions[0].transform.position, Quaternion.identity);
                    bullet.transform.rotation = Quaternion.LookRotation(AimPositions[0].transform.position - bullet.transform.position);
                    break;
                case 1:
                    bullet = Instantiate(Projectiles[1], SpawnObjectPositions[0].transform.position, Quaternion.identity);
                    bullet.transform.rotation = Quaternion.LookRotation(AimPositions[0].transform.position - bullet.transform.position);
                    break;
                case 2:
                    bullet = Instantiate(Projectiles[2], SpawnObjectPositions[0].transform.position, Quaternion.identity);
                    bullet.transform.rotation = Quaternion.LookRotation(AimPositions[0].transform.position - bullet.transform.position);
                    break;
                case 3:
                   bullet = Instantiate(Projectiles[3], SpawnObjectPositions[0].transform.position, Quaternion.identity);
                   bullet.transform.rotation = Quaternion.LookRotation(AimPositions[0].transform.position - bullet.transform.position);
                    break;
            }

            bulletsShot++;
        }
        else
        {
            GameObject bullet;

            for (int i = 0; i < 3; i++)
            {
                int bullet2Shoot = Random.Range(0, 4);

                bullet = Instantiate(Projectiles[bullet2Shoot], SpawnObjectPositions[i].transform.position, Quaternion.identity);
                bullet.transform.rotation = Quaternion.LookRotation(AimPositions[i].transform.position - bullet.transform.position);
            }

            bulletsShot = 0;
        }

        isAttacking = false;

    }
}
