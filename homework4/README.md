

# 牧师恶魔过河动作分离版



## 实现要求
- 设计一个裁判类，当游戏达到结束条件时，通知场景控制器游戏结束


## 实现细节

### judgment类

实现裁判类，只需要把之前在FirstController中判断输赢的函数封装成一个judgment类，然后再FirstController中调用裁判类的方法即可。

```c#
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
```

### FirstController类

在FirstController中需要在LoadResources中实例化CCActionManager和Judgment类，然后在移动角色函数中调用CCActionManager的方法来实现对角色的移动

```C#
private CCActionManager myActionManager;
public Judgment judgment;

judgment = new Judgment();
myActionManager = gameObject.AddComponent<CCActionManager>() as CCActionManager;

public void moveBoat(){
    if(boat.Empty_num() == 2){
        return;       
    }

    int[] personOnBoat = boat.Boat_person();  
    //boat.moveToBank();
    myActionManager.moveBoat(boat);
    for(int i = 0; i < 2; i++){
        if(personOnBoat[i] == -1){
            continue;
        }
        //devilpriest[personOnBoat[i]].moveBoat((boat.getState() == 1 ? 2 : 1), i);   
        myActionManager.moveDevilpriest(devilpriest[personOnBoat[i]], devilpriest[personOnBoat[i]].getDestinationOnBoat((boat.getState() == 1 ? 2 : 1), i));
    }

    if(judgment.iflose(boat, leftbank, rightbank) == true){
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
        devilpriest[index].setPositionOnBoat();
        myActionManager.moveDevilpriest(devilpriest[index], boat.Empty_Position());  
        leftbank.remove(devilpriest[index]);
        boat.put(devilpriest[index]);
    }
    //人在右岸，船在右岸，船有空位
    else if(devilpriest[index].getState() == 2 && boat.getState() == 2 && boat.Empty_num() > 0){   
        devilpriest[index].setPositionOnBoat();
        myActionManager.moveDevilpriest(devilpriest[index], boat.Empty_Position());
        rightbank.remove(devilpriest[index]);
        boat.put(devilpriest[index]);
    }
    //人在船上，船靠左岸
    else if(devilpriest[index].getState() == 0 && boat.getState() == 1){       
        devilpriest[index].setPositionOnLeftBank();
        myActionManager.moveDevilpriest(devilpriest[index], devilpriest[index].getPosOnLeftBank());  
        boat.remove(devilpriest[index]);
        leftbank.put(devilpriest[index]);
    }
    //人在船上，船靠右岸
    else if(devilpriest[index].getState() == 0 && boat.getState() == 2){       
        devilpriest[index].setPositionOnRightBank();
        myActionManager.moveDevilpriest(devilpriest[index], devilpriest[index].getPosOnRightBank()); 
        boat.remove(devilpriest[index]);
        rightbank.put(devilpriest[index]);
    }

    if(judgment.ifwin(boat, leftbank, rightbank) == true){
        for(int i = 0; i < 7; i++){
            clickGUI[i].click = false;
        }
        userGUI.gameState = 1;
        return;
    }
}
```

### 演示视频
![](/Priests-and-Devils.mp4)