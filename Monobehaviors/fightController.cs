using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;

public class fightController : MonoBehaviour
{

    private bool isFightOver = false;
    public GameObject hero_GO, monster_GO;
    public TextMeshPro hero_hp_TMP, monster_hp_TMP;
    private GameObject currentAttacker;
    private Animator theCurrentAnimator;
    private Monster theMonster;
    private bool shouldAttack = true ;
    private AudioSource attackSound;
    public TextMeshPro fightCommentaryTMP;

    // Start is called before the first frame update
    void Start()
    {       
        
            this.theMonster = new Monster("Pinky");
            this.fightCommentaryTMP.text ="";

            this.attackSound = this.gameObject.GetComponent<AudioSource>();
            this.hero_hp_TMP.text = "Current HP:" + MySingleton.thePlayer.getHP() + "  AC:"+ MySingleton.thePlayer.getAC();
            this.monster_hp_TMP.text = "Current HP:" + this.theMonster.getHP() + "  AC:"+ this.theMonster.getAC();
          int num = Random.Range(0,2); //who attacks first(flip coin)
          if(num==0)
          {
            this.currentAttacker = hero_GO;
          }
          else
          {
            this.currentAttacker = monster_GO;
          }
        

      StartCoroutine(fight());
    }

   
    private void tryAttack(Inhabitant attacker, Inhabitant defender)
    {
        this.fightCommentaryTMP.text ="";
        int attackRoll = Random.Range(0,20)+1;
        if(attackRoll>= defender.getAC())
        {
            int damageRoll = Random.Range(0,4)+3;
            defender.takeDamage(damageRoll);
            this.fightCommentaryTMP.color = Color.red;
            this.fightCommentaryTMP.text ="*HIT for "+ damageRoll+ "*";
            
            this.attackSound.Play();


        }
        else
        {
            this.fightCommentaryTMP.color = Color.magenta;
            this.fightCommentaryTMP.text ="Attack Missed!!";
        }

    }
    // Update is called once per frame

        IEnumerator fight()
    {   
        yield return new WaitForSeconds(1);
         if(this.shouldAttack)
        {
            this.theCurrentAnimator = this.currentAttacker.GetComponent<Animator>();
            this.theCurrentAnimator.SetTrigger("attack");
            if(currentAttacker== this.hero_GO)
        {
            this.currentAttacker = this.monster_GO;
            this.tryAttack(MySingleton.thePlayer,this.theMonster);
            this.monster_hp_TMP.text = "Current HP:" + this.theMonster.getHP() + "  AC:"+ this.theMonster.getAC();

            if(this.theMonster.getHP() <=0)
            {
                this.monster_GO.transform.Rotate(-90,0,0);
                this.fightCommentaryTMP.fontSize = 20;
                this.fightCommentaryTMP.color = Color.green;
                this.fightCommentaryTMP.text ="Hero Won!!";
                MySingleton.currentPellets++;
                this.isFightOver= true;
                this.shouldAttack = false;
            }
            else
            {
                StartCoroutine(fight());
            }
            
        }
        else
        {
            this.currentAttacker = this.hero_GO;
            this.tryAttack(this.theMonster, MySingleton.thePlayer);
            this.hero_hp_TMP.text = "Current HP:" + MySingleton.thePlayer.getHP() + "  AC:"+ MySingleton.thePlayer.getAC();
            
            if(MySingleton.thePlayer.getHP() <=0)
            {   
                this.hero_GO.transform.Rotate(-90,0,0);
                this.fightCommentaryTMP.fontSize = 20;
                this.fightCommentaryTMP.color = Color.black;
                this.fightCommentaryTMP.text ="Monster Won!!";
                this.isFightOver= true;
                this.shouldAttack = false;
            }
           else
            {
                StartCoroutine(fight());
            }
            
        }
       }
        yield return new WaitForSeconds(2);
    }
    void Update()
    {
       if(isFightOver && Input.GetKeyUp(KeyCode.Space))
       {
        MySingleton.thePlayer.resetStats();
        EditorSceneManager.LoadScene("Scene 1");


       }
        
        if(isFightOver&& Input.GetKeyUp(KeyCode.Escape))
        {
            EditorSceneManager.LoadScene("Shop");

        }
        


    }

}
