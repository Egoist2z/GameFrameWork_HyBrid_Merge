using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;



/// <summary>
/// ��Ϸ����������
/// </summary>
public class ProcedureMenu : ProcedureBase
{
    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);
        StartGame();                
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
    }

    private void StartGame() 
    {        
        GameEntry.HotUpdate.RunMethod("StartGame", "OpenMenuForm");        
    }    
}
