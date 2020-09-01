using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Validation
{
    public class MaiorIdadeValidationAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-MaiorIdadeValidation", "O aluno deve ser maior de 18 anos");

        }

        public override bool IsValid(object value)
        {
            DateTime dataNascimento = (DateTime)value;

            var age = DateTime.Now.Date.Year - dataNascimento.Year;

            if (age < 18)
                return false;

            return true;
        }


    }
}
