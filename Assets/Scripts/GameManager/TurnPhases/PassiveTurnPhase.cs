using System.Collections.Generic;

public abstract class PassiveTurnPhase : TurnPhase
{


    public PassiveTurnPhase(List<Player> players) : base(players)
    {
    }



    public override TurnPhase UpdatePhase()
    {
        return null;
    }
}