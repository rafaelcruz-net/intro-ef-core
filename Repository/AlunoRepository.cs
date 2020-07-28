using Dapper;
using Domain;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class AlunoRepository
    {
        private InfnetContext Context { get; set; }

        public AlunoRepository(InfnetContext context)
        {
            this.Context = context;
        }

        public IEnumerable<Aluno> GetAll()
        {
            return Context.Alunos.AsEnumerable();

        }

        public Aluno GetAlunoById(Guid id)
        {
            return Context.Alunos.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Aluno aluno)
        {
            this.Context.Alunos.Add(aluno);
            this.Context.SaveChanges();
        }

        public Aluno GetAlunoByEmail(string email)
        {
            return Context.Alunos.FirstOrDefault(x => x.Email == email);
        }

        public void Delete(Guid id)
        {
            var aluno = Context.Alunos.FirstOrDefault(x => x.Id == id);
            this.Context.Alunos.Remove(aluno);
            this.Context.SaveChanges();
        }
    }
}
