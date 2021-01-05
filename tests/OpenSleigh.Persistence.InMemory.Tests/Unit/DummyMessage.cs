﻿using System;
using OpenSleigh.Core;

namespace OpenSleigh.Persistence.InMemory.Tests.Unit
{
    public record DummyMessage(Guid Id, Guid CorrelationId) : ICommand
    {
        public static DummyMessage New() => new DummyMessage(Guid.NewGuid(), Guid.NewGuid());
    }
}