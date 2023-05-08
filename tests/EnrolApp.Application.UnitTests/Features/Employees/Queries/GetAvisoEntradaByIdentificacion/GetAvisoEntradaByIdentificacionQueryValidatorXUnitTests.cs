using EnrolApp.Application.Features.Employees.Queries.GetAvisoEntradaByIdentificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using FluentValidation.Results;

namespace EnrolApp.Application.UnitTests.Features.Employees.Queries.GetAvisoEntradaByIdentificacion
{
    public class GetAvisoEntradaByIdentificacionQueryValidatorXUnitTests
    {
        private readonly GetAvisoEntradaByIdentificacionQueryValidator validator;

        public GetAvisoEntradaByIdentificacionQueryValidatorXUnitTests()
        {
            validator = new GetAvisoEntradaByIdentificacionQueryValidator();
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("0956982ddd", false)]
        [InlineData("0956982536", true)]
        //[InlineData("526987568545", false)]
        public void GetAvisoEntradaByIdentificacionQueryValidator_ReturnTrueIdentificacion(string identificacion, bool expected)
        {
            var model = new GetAvisoEntradaByIdentificacionQuery(identificacion);

            ValidationResult result = validator.Validate(model);

            result.IsValid.ShouldBe(expected, result.ToString());
        }
    }
}
