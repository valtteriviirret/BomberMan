using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    EnemyController enemy;

    bool primary = false;
    bool secondary = false;

    bool rightleft = true;

    public ChaseState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateState()
    { 
        HuntPlayer();
    }

    void HuntPlayer()
    {
        //Debug.Log(enemy.direction);

        if(primary)
        {
            Primary();
        }

        if(secondary)
        {
            Secondary();
        }
           
        if(enemy.currentSpeed == 0)
        {
            // see if there is bomb near by
            if(enemy.BombVision() != -1)
            {
                rightleft = true;

                // if not space on the opposite direction of the bomb
                if(!enemy.LookDirection(enemy.OppositeDirection(enemy.BombVision()), true))
                {
                    if(rightleft)
                    {
                        // escape the bomb
                        enemy.GoRightOrLeft();
                        rightleft = false;
                        Debug.Log("enemy is going right or left");
                    }
                }
            }

            // if there is destructible tile in enemy's direction
            else if(!enemy.LookDirection(enemy.direction, false))
            {
                enemy.DropBomb();
                enemy.GoOpposite();
                Debug.Log("going opposite direction");
            }

            // if there is any other tile in enemy's direction
            else if(!enemy.LookDirection(enemy.direction, true))
            {
                Debug.Log("going going left or right direction");
                // just testing value
                enemy.GoRightOrLeft();
            }

            // if there is nothing in front of enemy's primary direction
            else
            {
                Debug.Log("going primary direction");
                primary = true;
                secondary = false;
                if(enemy.currentSpeed == 0)
                {
                    secondary = true;
                    primary = false;
                }
            }


            /*
            else if(enemy.PlayerSecondaryDirectionEmpty() && !primary)
            {
                // if enemy is already going opposite direction
                if(enemy.currentSpeed == 0)
                {
                    Debug.Log("going secondary direction");
                    secondary = true;
                }
            }
            */
        }
    }

    void Primary()
    {
        enemy.GoPrimaryDirection();
    }

    void Secondary()
    {
        enemy.GoSecondaryDirection();
    }

    public void ToNormalState()
    {
    }

    public void ToInitialState() {}
    public void ToChaseState() {}
}
