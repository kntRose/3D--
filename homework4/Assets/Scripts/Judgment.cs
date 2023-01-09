using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgment{
    public bool ifwin(BoatController boat, BankController leftBank, BankController rightBank){
        if(boat.Empty_num() == 2 && (leftBank.Devil_num() + leftBank.Priest_num() == 0) && (rightBank.Devil_num() + rightBank.Priest_num() == 6)){
            return true;
        }
        return false;
    }

    public bool iflose(BoatController boat, BankController leftBank, BankController rightBank){
        int countDevilLeft = leftBank.Devil_num();
        int countPriestLeft = leftBank.Priest_num();
        int countDevilRight = rightBank.Devil_num();
        int countPriestRight = rightBank.Priest_num();
        int []personOnBoat = boat.Boat_person();   
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
}
