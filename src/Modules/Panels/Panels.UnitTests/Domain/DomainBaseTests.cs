﻿using AutoFixture;

namespace Panels.UnitTests.Domain;

public class DomainBaseTests
{
    protected readonly Fixture Fixture;

    public DomainBaseTests()
    {
        Fixture = new Fixture();
    }
}