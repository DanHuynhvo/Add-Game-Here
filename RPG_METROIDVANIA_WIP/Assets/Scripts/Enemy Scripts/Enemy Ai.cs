using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAi", menuName = "Scriptable Objects/EnemyAi")]
public class EnemyAi : ScriptableObject
{
    void movement(){
        if(transform.position == pointA.position){
            _currentTarget = pointB.position;
        }
        else{
            _currentTarget = pointA.position;
        }

    }
    void tracking(){

    }
    void combat(){

    }
    void xp(){

    }
    void loot(){
        
    }
}
