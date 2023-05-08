﻿using Ardalis.Specification;
using EnrolApp.Domain.Entities.Common;


namespace EnrolApp.Application.Features.Clients.Specifications;

public class ClienteByIdentificacionSpec : Specification<Cliente>
{
    public ClienteByIdentificacionSpec(string Identificacion)
    {
        Query.Where(p => p.Identificacion == Identificacion);
           
    }
}
