namespace Dwapi.ExtractsManagement.Core.Interfaces.Stage.Psmart
{
    public interface IPsmartStage:IStage
    {
        string Serial { get; set; }
        string Demographics { get; set; }
        string Encounters { get; set; }
    }
}