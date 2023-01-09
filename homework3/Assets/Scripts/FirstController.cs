using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction{
    public DevilpriestController[] devilpriest;
	public BoatController boat;
	public BankController leftbank;
    public BankController rightbank;
    UserGUI userGUI;
	UserGUI []clickGUI = new UserGUI[7];

    void Awake(){
		SSDirector director = SSDirector.getInstance();
		director.setFPS(60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources();
		userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
		userGUI.gameState = 0;
	}

    public void LoadResources(){
		boat = new BoatController();
		boat.boat = Instantiate(Resources.Load("Prefabs/Boat"), new Vector3(-4, -2, 0), Quaternion.identity) as GameObject;
		clickGUI[6] = boat.boat.AddComponent<UserGUI>() as UserGUI;
		boat.setName("boat");
		boat.move = boat.boat.AddComponent(typeof(Move)) as Move;

		leftbank = new BankController("left");
		leftbank.bank = Instantiate(Resources.Load("Prefabs/Bank"), new Vector3(-15, -1.5f, 0), Quaternion.identity) as GameObject;
		leftbank.setName("LeftBank");
		
		rightbank = new BankController("right");
		rightbank.bank = Instantiate(Resources.Load("Prefabs/Bank"), new Vector3(15, -1.5f, 0), Quaternion.identity) as GameObject;
		rightbank.setName("RightBank");

		string []name = {"Devil", "Priest"};
		devilpriest = new DevilpriestController[6];
		for(int i = 0; i < 2; i++){
			for(int j = 0; j < 3; j++){
                devilpriest[i * 3 + j] = new DevilpriestController(name[i], i * 3 + j);
                devilpriest[i * 3 + j].devilpriest = Instantiate(Resources.Load("Prefabs/"+name[i]), Vector3.zero, Quaternion.identity) as GameObject;
                devilpriest[i * 3 + j].setName(name[i] + j);
                devilpriest[i * 3 + j].devilpriest.transform.position = new Vector3((float)(-20 + (i * 3 + j) * 2), 2f, 0);
                leftbank.put(devilpriest[i * 3 + j]);
                clickGUI[i * 3 + j] = devilpriest[i * 3 + j].devilpriest.AddComponent<UserGUI>() as UserGUI;
                devilpriest[i * 3 + j].move = devilpriest[i * 3 + j].devilpriest.AddComponent(typeof(Move)) as Move;
            }
		}
	}

	public bool iflose(){
		int countDevilLeft = leftbank.Devil_num();
        int countPriestLeft = leftbank.Priest_num();
		int countDevilRight = rightbank.Devil_num();
        int countPriestRight = rightbank.Priest_num();
		int[] personOnBoat = boat.Boat_person();  
		int d = 0, p = 0;

		for(int i = 0; i < 2; i++){
			if(personOnBoat[i] < 3 && personOnBoat[i] >= 0){
                d++;
            }  
			else if(personOnBoat[i] >= 3){
                p++;
            } 
		}

		if(boat.getState() == 1){
			countDevilLeft += d;
			countPriestLeft += p;
		}
		else if(boat.getState() == 2){
			countDevilRight += d;
			countPriestRight += p;
		}

		if((countDevilLeft > countPriestLeft && countPriestLeft != 0) || (countDevilRight > countPriestRight && countPriestRight != 0)){
			return true;
		}
		return false;
	}

	public bool ifwin(){
		if(boat.Empty_num() == 2 && (leftbank.Devil_num() + leftbank.Priest_num() == 0) 
           && (rightbank.Devil_num() + rightbank.Priest_num() == 6)){
            return true;
        }
		return false;
	}

	#region IUserAction implementation
	public void moveBoat(){
		if(boat.Empty_num() == 2){
            return;       
        }

		int[] personOnBoat = boat.Boat_person();  
		boat.moveToBank();
		for(int i = 0; i < 2; i++){
			if(personOnBoat[i] == -1){
                continue;
            }
			devilpriest[personOnBoat[i]].moveBoat((boat.getState() == 1 ? 2 : 1), i);   
		}

		if(iflose() == true){
			for(int i = 0; i < 7; i++){
				clickGUI[i].click = false;
			}
			userGUI.gameState = -1;
			return;
		}
	}

	public void moveDevilpriest(int index){
		//人在左岸，船在左岸，船有空位
		if(devilpriest[index].getState() == 1 && boat.getState() == 1 && boat.Empty_num() > 0){   
			devilpriest[index].setPositionOnBoat(boat.Empty_Position());
			leftbank.remove(devilpriest[index]);
			boat.put(devilpriest[index]);
		}
		//人在右岸，船在右岸，船有空位
		else if(devilpriest[index].getState() == 2 && boat.getState() == 2 && boat.Empty_num() > 0){   
			devilpriest[index].setPositionOnBoat(boat.Empty_Position());
			rightbank.remove(devilpriest[index]);
			boat.put(devilpriest[index]);
		}
		//人在船上，船靠左岸
		else if(devilpriest[index].getState() == 0 && boat.getState() == 1){       
			devilpriest[index].setPositionOnLeftBank();
			boat.remove(devilpriest[index]);
			leftbank.put(devilpriest[index]);
		}
		//人在船上，船靠右岸
		else if(devilpriest[index].getState() == 0 && boat.getState() == 2){       
			devilpriest[index].setPositionOnRightBank();
			boat.remove(devilpriest[index]);
			rightbank.put(devilpriest[index]);
		}

		if(ifwin() == true){
			for(int i = 0; i < 7; i++){
				clickGUI[i].click = false;
			}
			userGUI.gameState = 1;
			return;
		}
	}

	public void restart(){
		boat.reset();
		leftbank.reset();
		rightbank.reset();
		for(int i = 0; i < 6; i++){
			devilpriest[i].reset();
		}
		userGUI.gameState = 0;
		for(int i = 0; i < 7; i++){
			clickGUI[i].click = true;
		}
	}
	#endregion
}
