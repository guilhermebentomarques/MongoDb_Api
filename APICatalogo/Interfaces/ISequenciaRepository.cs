using MongoDB.Driver;

namespace APICatalogo.Interfaces
{
    public interface ISequenciaRepository
    {
        long GetNextSequenceValue(string sequenceName);
        void Insert();
    }
}
