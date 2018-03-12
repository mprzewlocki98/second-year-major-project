using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    //config
    public int config_textScrollSpeed;

    //interface
    public UnityEngine.UI.Text ui_name;
    public UnityEngine.UI.Text ui_content;
    public UnityEngine.UI.Image ui_portrait;

    //external variables
    public int nextSceneID;
    public GameObject[] actors;

    //internal variables
    private int click_counter = 0; //counts how far through cutscene the player is)
    private Action[] cutscene;

    private bool textScrolling = false;
    private string totaltext;
    private int scrollindex;
    private int tcounter = 0;

    public int timeout = 0;
    public int clickdelay = 0;



    void Start()
    {
        Action.sc = this;
        Cutscenes.actors = actors;
        Response.ui_name = ui_name;
        Response.ui_portrait = ui_portrait;

        cutscene = Cutscenes.getScene(nextSceneID);

        cutscene[0].Act();
    }

    void Update()
    {
        if (timeout > 0) { timeout--; }
        if (clickdelay > -1) { clickdelay--; } 
        if (clickdelay == 0) { OnMouseDown(); }
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


    public void OnMouseDown()
    {
        if (textScrolling == false && timeout <= 0)
        {
            click_counter++;
            if (click_counter < cutscene.Length)
            {
                cutscene[click_counter].Act();
            }
            else
            {
                Application.LoadLevel(nextSceneID);
            }
        }
        else
        {
            
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
}

//reads text out of textbox
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

}

//activates a unity animation as configured in the animationcontroller associated with the attatched gameobject
class Move : Action
{
    Animator a;
    int t;
    public Move(Animator a, int trigger)
    {
        this.a = a;
        t = trigger;
    }

    override public void Act()
    {
        a.SetInteger("State", t);
    }
}

//prevents any action for n frames
class Wait : Action
{
    int frames;
    public Wait(int frames)
    {
        this.frames = frames;
    }

    override public void Act()
    {
        sc.timeout = frames;
    }
}

//simulates a click after n frames
class DelayClick : Action
{
    int frames;
    public DelayClick(int frames)
    {
        this.frames = frames;
    }

    override public void Act()
    {
        sc.clickdelay = frames;
    }
}

//activates a gameobject
class Activate : Action
{
    GameObject g;
    public Activate(GameObject g)
    {
        this.g = g;
    }

    override public void Act()
    {
        g.SetActive(true);
    }         

}

//plays a sound
class PlaySound : Action
{
    
    public PlaySound()
    {
       
    }

    override public void Act()
    {
        
    }
    
}

//allows many animations and changing text simultaneously
class Multi : Action
{
    Action[] actions;
    public Multi(Action[] actions)
    {
        this.actions = actions;
    }

    override public void Act()
    {
        foreach (Action a in actions)
        {
            a.Act();
        }
    }
}



    static class Cutscenes
{
    public static GameObject[] actors;
    public static Action[] getScene(int i)
    {
        switch (i)
        {
            case 3:
                Animator mom = actors[0].GetComponent<Animator>();
                Animator child = actors[1].GetComponent<Animator>();
                Animator consult = actors[2].GetComponent<Animator>();
                Animator radiog = actors[3].GetComponent<Animator>();

                return new Action[] {
            new Multi(new Action[] {new Move(mom,1),new Move(child,1), new Response("Parent",null,"Hello, we are here for my child's scan.") }),
            new Multi(new Action[] {new Response("Child",null,"Hello!"),new Move(child,2) }),
            new Multi(new Action[] {new Response("Consultant",null,"Welcome to the nuclear medicine department." ),new Move(consult,1) }),
            new Multi(new Action[] {new Response("Consultant",null,"Please take a seat, someone will be with you shortly."),new Move(consult,2) }),
            new Multi(new Action[] {new Response("Parent",null,"Okay"),new Move(child,3),new Move(mom,2) }),
            new Multi(new Action[] {new Activate(actors[4]),new Wait(120),new DelayClick(121) }),
            new Multi(new Action[] {new Move(radiog,1), new Response("Radiographer",null,"Hello, I am one of the Radiographers who will be helping you today") }),
            new Multi(new Action[] {new Move(radiog,2),new Response("Radiographer",null,"Please follow the green lights to the scanning department.") }),
            new Activate(actors[5])
        };

            case 5: return new Action[] {
            new Response("Radiographer",null,"Here we are in the injection room"),
            new Response("Radiographer",null,"This is the superintendant radiographer, he will be overseeing your scan."),
            new Response("Superintendant",null,"Hello"),
            new Response("Parent",null,"Hi, nice to meet you."),
            new Response("Superintendant",null,"First we will start by removing any metal items from you, because metal interferes with the scan."),
            new Response("Radiographer",null,"Don't worry. You will get them back later!"),
};
		   case 7: return new Action[] {
				new Response("Superintendant",null,"Well done!"),				
                new Response("Superintendant",null,"This is my assistant, who will be conducting the injection"),
                new Response("Assistant",null,"We will put some cream on your shoulder and then we will give you the injection."),
                new Response("Child",null,"I-I'm scared..."),
                new Response("Parent",null,"Will it hurt at all doctor?"),                
                new Response("Assistant",null,"Not at all! That is what the cream is for."),
                new Response("Superintendant",null,"Now let's get started."),
            };

			case 9: return new Action[] {
				new Response("Assistant",null,"Good job! You were very brave."),
				new Response("Child",null,"That tickled!"),
                new Response("Parent",null,"So what does the injection do?"),
                new Response("Assistant",null,"We injected a tracer which will lets the scanning machine look inside their body"),                 
                new Response("Superintendant",null,"But first we will have to wait because the tracer must go around the body for a while"),			
               
            };

            case 11:
                return new Action[] {                
                new Response("Child",null,"Is it nearly time for the scan yet?"),
                new Response("Superintendant",null,"Yes, but first why don't you choose a movie you want to watch."),
                new Response("Assistant",null,"The scan takes a long time so you get to watch something"),
                new Response("Child",null,"Yay!"),
                new Response("Superintendant",null,"Try to stay still while you watch it so we can get an accurate scan"),
                new Response("Assistant",null,"So which movie would you like to watch?"),
            };

            case 13:
                return new Action[] {
                new Response("Assistant",null,"Now it's time for the scan!"),
                new Response("Kid",null,"I don't know if I want to..."),
                new Response("Parent",null,"It's OK, I will be here with you."),                
            };

            case 15:
                return new Action[] {
                new Response("Kid",null,"Yay, the scan is over!"),
                new Response("Nurse",null,"Well done! You are a very brave patient!"),
                new Response("Nurse",null,"Now we are going to have to remove the tracer."),
                new Response("Nurse",null,"Don't worry, it's very simple."),
                new Response("Nurse",null,"The tracer will be removed when you go to the toilet."),
                new Response("Nurse",null,"Drink as much water as you can to go to the toilet faster!"),
            };

            case 17:
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
