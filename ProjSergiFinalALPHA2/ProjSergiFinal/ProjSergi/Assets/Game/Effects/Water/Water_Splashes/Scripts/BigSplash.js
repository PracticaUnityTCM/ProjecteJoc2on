#pragma strict

var BigSplash : GameObject;

private var splashFlag = 0;


function Start () 
{

    BigSplash.SetActive(false);

}

function Update () {

    if (Input.GetButtonDown("Fire1"))
    {

        if (splashFlag == 0)
        {
            TriggerSplash();
        }
       
    }


    
}

   
function TriggerSplash()
{
    
    splashFlag = 1;
    
    BigSplash.SetActive(true);

    yield WaitForSeconds (3.5);

    BigSplash.SetActive(false);

    splashFlag = 0;

}



