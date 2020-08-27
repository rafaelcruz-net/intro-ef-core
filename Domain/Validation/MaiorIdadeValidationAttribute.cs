using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Validation
{
    public class MaiorIdadeValidationAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            DateTime dataNascimento = (DateTime)value;
            DateTime dateTimeAgora = DateTime.Now.Date;

            var age = DateTime.Now.Date.Year - dataNascimento.Year;

            if (age < 18)
                return false;

            return true;
        }


    }
}
