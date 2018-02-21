using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    //config
    public int config_textScrollSpeed;

    //interface
    public UnityEngine.UI.Text ui_name;
    public UnityEngine.UI.Text ui_content;
    public UnityEngine.UI.Image ui_portrait;

    //external variables
    public int nextSceneID;
    public Sprite background;
    public Sprite[] actors;

    //internal variables
    private int click_counter = 0; //counts how far through cutscene the player is)
    private Action[] cutscene;

    private bool actorLerp = false;

    private bool textScrolling = false;
    private string totaltext;    
    private int scrollindex;
    private int tcounter = 0;

    

    void Start () {
        Action.sc = this;
        Response.ui_name = ui_name;
        Response.ui_portrait = ui_portrait;

        cutscene = Cutscenes.getScene(nextSceneID);

        cutscene[0].Act();
     }

    void Update () {
        
        if (textScrolling)            
        {
            tcounter++;
            if (tcounter > config_textScrollSpeed)
            {
                tcounter = 0;
                if (scrollindex < totaltext.Length)
                {
                    scrollindex++;
                    ui_content.text = totaltext.Substring(0, scrollindex);
                }
                else
                {
                    textScrolling = false;
                    tcounter = 0;
                }
            }
        }
	}

    void OnMouseDown()
    {
        if (textScrolling == false && actorLerp == false)
        {
            click_counter++;
            if (click_counter < cutscene.Length)
            {
                cutscene[click_counter].Act();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            if(textScrolling == true)
            {
                //stop scrolling, instantly show
            }

            if(actorLerp == true)
            {
                //stop lerp, instant move
            }
        }
    }

    public void InitiateTextScrolling(string t)
    {
        textScrolling = true;
        totaltext = t;
        scrollindex = 0;
    }
    
}

abstract class Action
{
    public static SceneController sc;
    public abstract void Act(); //start an action
    public abstract void Force(); //skip to the end of an action

}

class Response : Action
{
    public static UnityEngine.UI.Text ui_name;
    public static UnityEngine.UI.Image ui_portrait;

    private string name;        
    private Sprite portrait;     
    private string content;    
    
    public Response(string name, Sprite portrait, string content)
    {
        this.name = name;
        this.portrait = portrait;
        this.content = content;
    }

    override public void Act() //start text scrolling
    {
        ui_name.text = name;
        ui_portrait.sprite = portrait;
        sc.InitiateTextScrolling(content);
    }

    override public void Force() //skip text scroll and set text
    {   
        
    }
}

static class Cutscenes
{
    public static Action[] getScene(int i)
    {
        switch (i)
        {
            case 1: return new Action[] {
            new Response("Parent",null,"Hi, we're here for my childs CT scan"),
            new Response("Receptionist",null,"Please take a seat"),
            new Response("Kid",null,"ASHDOIASJDPOJASDPOAISJD"),
            new Response("Nurse",null,"Thanks for waiting come in"),
        };

            case 2: return new Action[] {
            new Response("Nurse",null,"This is your doctor, he will be conducting your scan"),
            new Response("Parent",null,"Hi nice to meet you"),
            new Response("Kid",null,"HEYAHERERERASDASDASDASDASDASDAD"),
            new Response("Nurse",null,"First we will start by removing anything metal from you, because metal interferes with the scan"),
            new Response("Nurse",null,"Don't worry you will get them back later"),
            new Response("Nurse",null,"Doctor, why don't you help them out"),
};
		   case 3: return new Action[] {
				new Response("Nurse",null,"Well done!"),
				new Response("Kid",null,"HEYAHERERERASDASDASDASDASDASDAD"),
				new Response("Nurse",null,"Then we introduce a injection meal game, which is what we are going to do next."),
				new Response("Nurse",null,"Try this game, see if you can pass it"),
			};


			case 4: return new Action[] {
				new Response("Nurse",null,"Good job!"),
				new Response("Kid",null,"Hahahahahahahahaha"),
				new Response("Nurse",null,"Next we have go to CT room, before that try CT scan game. It will help you know more about this scan."),
				new Response("Nurse",null,"Let's go!"),
			};
            default: return new Action[] {new Response("Developer",null,"You shouldn't be seeing this"),};
        }
    }
}
