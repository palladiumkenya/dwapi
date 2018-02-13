namespace Dwapi.ExtractsManagement.Core.Interfaces.Stage.Source.Psmart
{
    public interface IPsmartSource:ISource
    {
        int Serial { get; set; }
        string Demographics { get; set; }
        string Encounters { get; set; }
    }
}