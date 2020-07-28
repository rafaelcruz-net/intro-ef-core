using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Mapeamento;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Context
{
    public class InfnetContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        
        public InfnetContext(DbContextOptions<InfnetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
