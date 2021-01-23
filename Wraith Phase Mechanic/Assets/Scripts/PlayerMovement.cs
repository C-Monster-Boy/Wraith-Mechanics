using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum MoveType
    {
        Idle, Walk, Run
    };
    public float rotationSpeed;
    public float forwardSpeed;
    public float runSpeed;
    public float inputThreshold;

    public AudioSource audioSource;
    public AudioClip[] clips;
    public float walkGap;
    public float runGap;
    private float currTimeSinceLastPlayEnd;

    private float inputX;
    private float inputZ;
    private Vector3 desiredMoveDirection;
    MoveType m;

    private Animator anim;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        m = MoveType.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        SetMoveDirection();
        PlayAudio();

        if (inputZ >= inputThreshold )
        {
            LookAtDesiredDirection();
            Vector3 moveDir = new Vector3(0, 0, inputZ);//transform.forward * inputZ;
            

            if(Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(moveDir * runSpeed * Time.deltaTime, Space.Self);
                anim.SetFloat("Move_Input", 2);
                m = MoveType.Run;
            }
            else
            {
                transform.Translate(moveDir * forwardSpeed * Time.deltaTime, Space.Self);
                anim.SetFloat("Move_Input", Mathf.Abs(inputZ));
                m = MoveType.Walk;
            }
        }
        else if(inputZ <= -inputThreshold)
        {
            LookAtDesiredDirection();
            Vector3 moveDir = new Vector3(0, 0, -inputZ);//transform.forward * inputZ;
            transform.Translate(moveDir * forwardSpeed * Time.deltaTime, Space.Self);
            anim.SetFloat("Move_Input", Mathf.Abs(inputZ));
        }
        else if (inputX >= inputThreshold)
        {
            LookAtDesiredDirection();
            Vector3 moveDir = new Vector3(0, 0, inputX);//transform.forward * inputZ;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(moveDir * runSpeed * Time.deltaTime, Space.Self);
                anim.SetFloat("Move_Input", 2);
                m = MoveType.Run;
            }
            else
            {
                transform.Translate(moveDir * forwardSpeed * Time.deltaTime, Space.Self);
                anim.SetFloat("Move_Input", Mathf.Abs(inputX));
                m = MoveType.Walk;
            }
        }
        else if (inputX <= -inputThreshold)
        {
            LookAtDesiredDirection();
            Vector3 moveDir = new Vector3(0, 0, -inputX);//transform.forward * inputZ;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(moveDir * runSpeed * Time.deltaTime, Space.Self);
                anim.SetFloat("Move_Input", 2);
                m = MoveType.Run;
            }
            else
            {
                transform.Translate(moveDir * forwardSpeed * Time.deltaTime, Space.Self);
                anim.SetFloat("Move_Input", Mathf.Abs(inputX));
                m = MoveType.Walk;
            }
        }
        else
        {
            anim.SetFloat("Move_Input", 0);
            m = MoveType.Idle;
        }
    }

    void SetMoveDirection()
    {
        var cam = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * inputZ + right * inputX;
    }

    void PlayAudio()
    {
        if(m == MoveType.Idle)
        {
            currTimeSinceLastPlayEnd = -10;
        }
        else if(m == MoveType.Walk)
        {
            if (currTimeSinceLastPlayEnd <= 0)
            {
                currTimeSinceLastPlayEnd = walkGap;
                int clipToPlay = Random.Range(0, clips.Length - 1);
                if(clips[clipToPlay] == audioSource.clip)
                {
                    clipToPlay = (clipToPlay + 1) % clips.Length;
                }
                audioSource.clip = clips[clipToPlay];
                audioSource.Play();
            }
        }
        else
        {
            if (currTimeSinceLastPlayEnd <= 0)
            {
                currTimeSinceLastPlayEnd =runGap;
                int clipToPlay = Random.Range(0, clips.Length - 1);
                if (clips[clipToPlay] == audioSource.clip)
                {
                    clipToPlay = (clipToPlay + 1) % clips.Length;
                }
                audioSource.clip = clips[clipToPlay];
                audioSource.Play();
            }
        }

        currTimeSinceLastPlayEnd -= Time.deltaTime;
       
    }

    void LookAtDesiredDirection()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed);
    }
}