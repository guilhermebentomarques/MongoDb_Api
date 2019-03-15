using APICatalogo.Interfaces;
using APICatalogo.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace APICatalogo.Repository
{
    public class SequenciaRepository : ISequenciaRepository
    {
        private readonly CatalogoContext _context = null;

        public SequenciaRepository(IOptions<Settings> settings)
        {
            _context = new CatalogoContext(settings);
        }

        public void Insert()
        {
            try
            {
                Sequencia seq = new Sequencia();
                seq.Name = "Produto";
                seq.Value = 1;
                _context.Sequencias.InsertOneAsync(seq);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public long GetNextSequenceValue(string sequenceName)
        {
            var filter = Builders<Sequencia>.Filter.Eq(a => a.Name, sequenceName);
            var update = Builders<Sequencia>.Update.Inc(a => a.Value, 1);
            var sequence = _context.Sequencias.FindOneAndUpdate(filter, update);
            if (sequence == null)
            {
                this.Insert();
                filter = Builders<Sequencia>.Filter.Eq(a => a.Name, sequenceName);
                update = Builders<Sequencia>.Update.Inc(a => a.Value, 1);
                sequence = _context.Sequencias.FindOneAndUpdate(filter, update);
            }

            return sequence.Value;
        }
    }
}
