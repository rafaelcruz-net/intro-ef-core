using Domain;
using Repository;
using System;
using System.Collections.Generic;

namespace ApplicationService
{
    public class AlunoServices
    {
        private AlunoRepository Repository { get; set; }

        public AlunoServices(AlunoRepository repository)
        {
            this.Repository = repository;
        }

        public IEnumerable<Aluno> GetAll()
        {
            return Repository.GetAll();
        }

        public Aluno GetAlunoById(Guid id)
        {
            return Repository.GetAlunoById(id);
        }

        public void Save(Aluno aluno)
        {
            if (this.GetAlunoByEmail(aluno.Email) != null)
            {
                throw new Exception("Já existe um aluno com este email, por favor cadastre outro email");
            }

            var anoAtual = DateTime.Now.Date.Year;

            if ((anoAtual - aluno.DataNascimento.Date.Year) < 21)
            {
                throw new Exception("Para cadastrar um novo aluno ele deve no minimo 21 anos");
            }

            aluno.Status = Status.EM_CONFIRMACAO_EMAIL;

            this.Repository.Save(aluno);
        }

        public Aluno GetAlunoByEmail(string email)
        {
            return Repository.GetAlunoByEmail(email);
        }

        public void Delete(Guid id)
        {
            Repository.Delete(id);
        }

        public void Update(Guid id, Aluno aluno)
        {
            Repository.Update(id, aluno);
        }

    }
}
