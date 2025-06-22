
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class IdleState : IPlayerStates
//{
//    public void HandleJump(PlayerController player, InputAction.CallbackContext context)
//    {
//        if(context.performed && player.isGrounded)
//        {
//            player.SetState(new JumpState());
//        }
//    }

//    public void HandleMovement(PlayerController player, InputAction.CallbackContext context)
//    {
//        // if there are perform of input A or D, enters in Run State
//        if (context.performed && context.ReadValue<Vector2>().x != 0)
//        {
//            player.SetState(new RunState());
//        }
//    }


//    public void OnEnter(PlayerController player)
//    {
//        player.rb.linearVelocity = new Vector3(0, player.rb.linearVelocity.y, player.rb.linearVelocity.z); // cheak up, if player stand still
//    }

//    public void OnExit(PlayerController player)
//    {
       
//    }

//    public void PhysicsUpdate(PlayerController player)
//    {
//        player.rb.linearVelocity = new Vector3(0, player.rb.linearVelocity.y, player.rb.linearVelocity.z); // In IDLE, player must stand still on X axes
//    }

  
//}


//public class RunState : IPlayerStates
//{
//    public void HandleJump(PlayerController player, InputAction.CallbackContext context)
//    {
//        if (context.performed && player.isGrounded)
//        {
//            player.SetState(new JumpState());
//        }
//    }

//    public void HandleMovement(PlayerController player, InputAction.CallbackContext context)
//    {
//        if (context.canceled && player.movementInput.x == 0) // if player isn't pressing A or D button, go to Idle State
//        {
//            player.SetState(new IdleState());
//        }
//    }



//    public void OnEnter(PlayerController player)
//    {
  
//    }

//    public void OnExit(PlayerController player)
//    {
//    }

//    public void PhysicsUpdate(PlayerController player)
//    {
//        // uses force for moving
//        Vector3 force = new Vector3(player.movementInput.x * player.speed, 0f, 0f);
//        player.rb.AddForce(force, ForceMode.Force);
//    }

  
//}

//public class JumpState : IPlayerStates
//{
//    public void HandleJump(PlayerController player, InputAction.CallbackContext context)
//    {
//        //THERE IS WILL BE DOUBLE JUMP SOLUTION
//    }

//    public void HandleMovement(PlayerController player, InputAction.CallbackContext context)
//    {
        
//    }

//    public void OnEnter(PlayerController player)
//    {
//        player.rb.AddForce(Vector2.up * player.jumpStrenght, ForceMode.Impulse); // impliment Force for jumping
//    }

//    public void OnExit(PlayerController player)
//    {
        
//    }

//    public void PhysicsUpdate(PlayerController player)
//    {
//        // while player is not on the ground, his speed will be halved
//        Vector3 horizontalForce = new Vector3(player.movementInput.x * player.speed, 0, 0);
//        player.rb.AddForce(horizontalForce, ForceMode.Force);
//        // cheaking if player grounded
//        if (player.isGrounded)
//        {
//            // if he landed and have movement by X axis, goes to Run State
//            if (player.movementInput.x != 0)
//            {
//                player.SetState(new RunState());
//            }
//            else
//            {
//                player.SetState(new IdleState());
//            }
//        }
//    }

   
//}

//public class WallTouch : IPlayerStates
//{
//    string _message;
//    public WallTouch(string message)
//    {
//        _message = message;
//    }

//    public void HandleJump(PlayerController player, InputAction.CallbackContext context)
//    {
//        if (context.performed && player.isGrounded && _message == "right")
//        {
//            player.rb.AddForce(new Vector3(-1, 0, 0) * player.jumpStrenght, ForceMode.Impulse); // impliment Force for jumping
//            player.SetState(new JumpState());
//        }
//        else if (context.performed && player.isGrounded && _message == "left")
//        {
//            player.rb.AddForce(new Vector3(1, 0, 0) * player.jumpStrenght, ForceMode.Impulse); // impliment Force for jumping
//            player.SetState(new JumpState());
//        }
//        else
//        {
//            player.SetState(new JumpState());
//        }
       
//    }

//    public void HandleMovement(PlayerController player, InputAction.CallbackContext context)
//    {
     
//    }

//    public void OnEnter(PlayerController player)
//    {

//    }

//    public void OnExit(PlayerController player)
//    {
        
//    }

//    public void PhysicsUpdate(PlayerController player)
//    {
//        Vector3 horizontalForce = new Vector3(player.movementInput.x * player.speed, 0, 0);
//        player.rb.AddForce(horizontalForce, ForceMode.Force);
//        // cheaking if player grounded
//        if (player.isGrounded)
//        {
//            // if he landed and have movement by X axis, goes to Run State
//            if (player.movementInput.x != 0)
//            {
//                player.SetState(new RunState());
//            }
//            else
//            {
//                player.SetState(new IdleState());
//            }
//        }
//    }

   
//}
