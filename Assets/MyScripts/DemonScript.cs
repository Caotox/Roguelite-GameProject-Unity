using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{
    public GameObject demonCharacter;
    public bool isDead = false;
    bool hasCreated = false;
    bool hasGenerated = false;
    public int randomNumber;
    public float currenthP;
    public float xposition;
    public float yposition;
    public float maxHP = 100.0f;
    public GameObject enemy_fireball;
    public float timer = 2f;
    public GameObject mainCharacter;
    public string fireDirection;
    public string fireTempo;
    public float temp_scale;
    public Rigidbody2D demonRigid;
    public Transform mainCharTransform;
    public float timerStop = 3f;
    public float timerStop2 = 1.5f;
    public GameObject upgrade;
    public Animator demonAnimator;
    // Start is called before the first frame update
    void Start()
    {
        currenthP = maxHP;
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        mainCharTransform = mainCharacter.GetComponent<Transform>();
        demonCharacter.GetComponent<Rigidbody2D>().gravityScale = 20;
        FlipCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        FlipCharacter();
        GetPositions();
        Deplacement();
        Shoot();
    }
    void FixedUpdate(){
        StopRegu();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MCSpell")
        {
            demonAnimator.SetTrigger("isHurt");
            float spellDamage = mainCharacter.GetComponent<MainCharacterScript>().spellDamage;
            currenthP -= spellDamage;
            //Debug.Log("Hit FireBall : " + currenthP);
        }
        if (collision.gameObject.tag == "MCFleche")
        {
            demonAnimator.SetTrigger("isHurt");
            float arrowDamage = mainCharacter.GetComponent<MainCharacterScript>().arrowDamage;
            currenthP -= arrowDamage;
            //Debug.Log("Hit Fleche : " + currenthP);
        }
        if (collision.gameObject.layer == 3)
        {
            demonCharacter.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
    void FlipCharacter()
    {
        if (mainCharacter.transform.position.x < transform.position.x)
        {
            temp_scale = 1;
            fireDirection = "left";
        }
        else if (mainCharacter.transform.position.x > transform.position.x)
        {
            temp_scale = -1;
            fireDirection = "right";
        }
        transform.localScale = new Vector3(temp_scale*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    void GetPositions()
    {
        xposition = mainCharTransform.position.x;
        yposition = mainCharTransform.position.y;
    }
    void Deplacement()
    {
        if (UpgradesScript.isUpgrading == false && isDead == false){
        if (fireDirection == "right"){
            if (xposition - transform.position.x > 8){ // avancer
                demonRigid.velocity = Vector2.right * 3;
            } else if (xposition - transform.position.x < 5){ // reculer
                demonRigid.velocity = Vector2.left * 3;
            } else { // ne rien faire
                demonRigid.velocity = Vector2.zero;
            }


        } else if (fireDirection == "left"){
            if (transform.position.x - xposition > 8){ // avancer
                demonRigid.velocity = Vector2.left * 3;
            } else if (transform.position.x - xposition < 5){ // reculer
                demonRigid.velocity = Vector2.right * 3;
            } else { // ne rien faire
                demonRigid.velocity = Vector2.zero;
            }

        }
        }
    }
    void StopRegu(){
        if (timerStop <=0){
            demonRigid.velocity = Vector2.zero;
            if (timerStop2 <=0){
                timerStop = 3f;
                timerStop2 = 1.5f;
            } else {
                timerStop2 -= Time.deltaTime;
            } 
        } else {
            timerStop -= Time.deltaTime;
            Deplacement();
        }
    }
    void enableGameObject(){
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        isDead = true;
    }
    void Death(){
        if (currenthP <= 0)
            {
                enableGameObject();
                if (hasGenerated == false){
                    randomNumber = Random.Range(1, 4);
                    hasGenerated = true;
                }
                //Debug.Log(randomNumber);
                if (randomNumber == 1 && hasCreated == false){
                    Instantiate(upgrade, new Vector2(transform.position.x, transform.position.y), transform.rotation);
                    hasCreated = true;
            }
    }
    }
    void Shoot()
{
    if (UpgradesScript.isUpgrading == false && isDead == false)
    {
        if (timer <= 0)
        {
            GameObject fireballInstance;

            if (fireDirection == "right")
            {
                fireballInstance = Instantiate(enemy_fireball, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
            }
            else
            {
                fireballInstance = Instantiate(enemy_fireball, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
            }

            timer = 4f;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
}
