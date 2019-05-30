using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectMenu : MonoBehaviour
{

    [SerializeField] GameObject vehRotator;
    [SerializeField] GameObject persistentsPrefab;
    int currentCar = 0;
    GameObject p;

    enum Racers { Lynessia, Kaldor, Jaxxus, Carl};
    Racers currentRacer = Racers.Lynessia;

    [SerializeField] TMP_Text racerNameText;
    [SerializeField] TMP_Text racerBioText;

    string lynessiaBioText = "This Mauve skinned beauty hails from the Magic Dominion of Illandria Don’t let her curvaceous elegance fool you, being an Arch Magistra in her own right, this carnivorous woman is a force to be reckoned with.";
    string kaldorBioText = "A major hustler and gambling addict, straight out of the slums of Orbital Station OS14-556789, this extravagant womanizer blew his way into upper Aerikane society by pushing his his racing skills past the limits striving to obtain faster and faster speeds.";
    string jaxxusBioText = "A powerful racing warrior, hailing from the Tropical Jungles of Tandellar, A place packed full of dangerous beasts just lying in wait for easy prey.Jaxxus has been hunting and racing since he could walk. While usually bright and jolly, when he gets serious, his predatory nature comes out.";
    string carlBioText = "Straight from the Gladiatorial racing pits of Karth, not much is known about this Champion, it is said he was first captured from a primitive backwater world where he honed his racing skills on something called NASCAR viiddeeeoo gaammeeess, whatever that means.";

    // Start is called before the first frame update
    void Start()
    {
        SetRacer(currentCar);
        if (GameObject.Find("Persistents") == null)
        {
            p = Instantiate(persistentsPrefab);
            p.name = "Persistents";
        }
        else
            p = GameObject.Find("Persistents");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
            p.GetComponent<PathContainer>().setPlayerVehicle(currentCar);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
            p.GetComponent<PathContainer>().setPlayerVehicle(currentCar);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectRacer(currentCar);
        }
    }

    void MoveLeft()
    {
        currentCar--;
        if(currentCar < 0)
        {
            currentCar = 3;
        }
        SetRacer(currentCar);
        vehRotator.transform.Rotate(0, 90, 0);
    }

    void MoveRight()
    {
        currentCar++;
        if (currentCar > 3)
        {
            currentCar = 0;
        }
        SetRacer(currentCar);
        vehRotator.transform.Rotate(0, -90, 0);
    }

    void SetRacer(int n)
    {
        if (n == 0)
        {
            currentRacer = Racers.Lynessia;
            racerNameText.text = "Lynessia";
            racerBioText.text = lynessiaBioText;
        }
        else if (n == 1)
        {
            currentRacer = Racers.Kaldor;
            racerNameText.text = "Kaldor";
            racerBioText.text = kaldorBioText;

        }
        else if (n == 2)
        {
            currentRacer = Racers.Jaxxus;
            racerNameText.text = "Jaxxus";
            racerBioText.text = jaxxusBioText;

        }
        else if (n == 3)
        {
            currentRacer = Racers.Carl;
            racerNameText.text = "Carl";
            racerBioText.text = carlBioText;

        }
    }

    void SelectRacer(int index)
    {
        /*Whatever output functions you need go here, this is called with the Space Key Down
        the currentCar int has the current driver and will be between 0 and 3
        0 is Lynessia
        1 is Kaldor
        2 is Jaxxus
        3 is Carl */
        FindObjectOfType<ButtonManager>().LoadLevelOne();
    }

}
