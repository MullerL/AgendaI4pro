using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaI4pro.Models
{
    public interface IContatoDAL
    {
        IEnumerable<Contato> GetAllContato();
        void AddContato(Contato contato);
        void UpdateContato(Contato contato);
        Contato GetContato(int? id);
        void DeleteContato(int? id);
    }
}
