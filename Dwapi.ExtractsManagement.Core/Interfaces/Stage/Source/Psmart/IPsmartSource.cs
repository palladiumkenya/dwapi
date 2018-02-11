namespace Dwapi.ExtractsManagement.Core.Interfaces.Source.Psmart
{
    public interface IPsmartSource:ISource
    {
        int Serial { get; set; }
        string Demographics { get; set; }
        string Encounters { get; set; }
    }
}