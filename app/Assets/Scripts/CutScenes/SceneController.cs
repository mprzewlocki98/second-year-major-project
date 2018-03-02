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
            new Response("Parent",null,"Hello, we are here for my child's scan."),
            new Response("Receptionist",null,"Please take a seat, someone will be with you shortly!"),
            new Response("Kid",null,"Tee hee hee!"),
            new Response("Nurse",null,"Hello, I am the nurse. Please follow the green lights to the scanning department!"),
        };

            case 2: return new Action[] {
            new Response("Nurse",null,"This is your doctor, he will be conducting your scan."),
            new Response("Doctor",null,"Hello, I'm the doctor."),
            new Response("Parent",null,"Hello, nice to meet you."),
            new Response("Nurse",null,"First we will start by removing any metal items from you, because metal interferes with the scan."),
            new Response("Nurse",null,"Don't worry. You will get them back later!"),
            new Response("Nurse",null,"Doctor, why don't you help them out?"),
};
		   case 3: return new Action[] {
				new Response("Nurse",null,"Well done!"),
				new Response("Nurse",null,"Now it is time for your injection, please come to the injection room!"),
				new Response("Nurse",null,"We will put some cream on your shoulder and then we will give you the injection."),
                new Response("Parent",null,"Will it hurt at all?"),
                new Response("Kid",null,"Waah, I hate pain!"),
                new Response("Nurse",null,"Not at all! That is what the cream is for."),
                new Response("Nurse",null,"Now let's get started."),
            };

			case 4: return new Action[] {
				new Response("Nurse",null,"Good job!"),
				new Response("Kid",null,"That tickles!"),
				new Response("Nurse",null,"Now we'll wait a bit for the injection to take effect."),
				new Response("Nurse",null,"The tracer in the injection will go to cells in your body."),
                new Response("Nurse",null,"Help the tracer on its way to the cells!"),
            };

            case 5:
                return new Action[] {
                new Response("Nurse",null,"Well done, you did it!"),
                new Response("Kid",null,"Is it very nearly time for the scan yet?"),
                new Response("Nurse",null,"Yes, but first why don't you choose a movie you want to watch before the scan?"),
                new Response("Nurse",null,"The scan will take some time and you will have to lie still."),
                new Response("Nurse",null,"But don't worry, you can watch a movie while the scan is happening!"),
                new Response("Parent",null,"Can I go in with my child?"),
                new Response("Nurse",null,"Yes, you can be with your child at all times."),
            };

            case 6:
                return new Action[] {
                new Response("Nurse",null,"Now it's time for the scan!"),
                new Response("Kid",null,"I don't know if I want to..."),
                new Response("Parent",null,"It's OK, I will be here with you."),
                new Response("Nurse",null,"See if you can help us scan!"),
            };

            case 7:
                return new Action[] {
                new Response("Kid",null,"Yay, the scan is over!"),
                new Response("Nurse",null,"Well done! You are a very brave patient!"),
                new Response("Nurse",null,"Now we are going to have to remove the tracer."),
                new Response("Nurse",null,"Don't worry, it's very simple."),
                new Response("Nurse",null,"The tracer will be removed when you go to the toilet."),
                new Response("Nurse",null,"Drink as much water as you can to go to the toilet faster!"),
            };

            case 8:
                return new Action[] {
                new Response("Nurse",null,"Good job!"),
                new Response("Parent",null,"Is there anything else we need to do now?"),
                new Response("Nurse",null,"No, that's it. The scan is over!"),
                new Response("Parent",null,"Let's go arrange the next appointment."),
                new Response("Kid",null,"Can we go home now?"),
                new Response("Parent",null,"In a few minutes!"),
                new Response("Nurse",null,"You've been a good patient! Have a sticker."),
                new Response("Kid",null,"Yay!"),
                new Response("Nurse",null,"Thanks for playing!"),
            };

            default: return new Action[] {new Response("Developer",null,"You shouldn't be seeing this"),};
        }
    }
}
