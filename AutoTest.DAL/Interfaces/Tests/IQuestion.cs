﻿using AutoTest.Domain.Entities.Tests;

namespace AutoTest.DAL.Interfaces.Tests;

public interface IQuestion : IRepository<Question>
{
    IQueryable<Question> GetAllFullInformation();
}
