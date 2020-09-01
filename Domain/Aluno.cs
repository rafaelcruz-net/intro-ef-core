using Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Aluno
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Campo Nome deve ter no máximo 50 caracteres")]
        public String Nome { get; set; }

        [StringLength(50, ErrorMessage = "Campo Matricula deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage = "Campo Matricula é obrigatório")]
        public String Matricula { get; set; }

        [Required(ErrorMessage = "Campo Data de Nascimento é obrigatório")]
        [MaiorIdadeValidation(ErrorMessage = "O aluno deve ser maior que 18 anos para completar o cadastro")]
        public DateTime DataNascimento { get; set; }

        [StringLength(50, ErrorMessage = "Campo Email deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage = "Campo Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Campo Email não está em um formato correto")]
        public String Email { get; set; }

        [StringLength(14, ErrorMessage = "Campo CPF deve ter no máximo 11 caracteres")]
        [Required(ErrorMessage = "Campo CPF é obrigatório")]
        [RegularExpression(@"\d{3}.\d{3}.\d{3}-\d{2}", ErrorMessage = "CPF não está em um formato correto")]
        [CpfValidation(ErrorMessage = "Cpf Inválido")]
        public String CPF { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        NAO_ATIVO,
        ATIVO,
        EM_CONFIRMACAO_EMAIL
    }
}
