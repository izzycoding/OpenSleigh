using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using OpenSleigh.Core.Messaging;

namespace OpenSleigh.Core.DependencyInjection
{
    public interface ISagaConfigurator<TS, in TD>
        where TS : Saga<TD>
        where TD : SagaState
    {
        IServiceCollection Services { get; }
        ISagaConfigurator<TS, TD> UseStateFactory<TM>(Func<TM, TD> stateFactory)
            where TM : IMessage;
    }

    [ExcludeFromCodeCoverage]
    internal class SagaConfigurator<TS, TD> : ISagaConfigurator<TS, TD>
        where TS : Saga<TD>
        where TD : SagaState
    {
        public SagaConfigurator(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceCollection Services { get; }

        public ISagaConfigurator<TS, TD> UseStateFactory<TM>(Func<TM, TD> stateFactory)
            where TM : IMessage
        {
            var messageType = typeof(TM);
            var stateType = typeof(TD);

            var factoryInterfaceType = typeof(ISagaStateFactory<,>).MakeGenericType(messageType, stateType);
            var factory = new LambdaSagaStateFactory<TM, TD>(stateFactory);

            var descriptor = ServiceDescriptor.Singleton(factoryInterfaceType, factory);
            this.Services.Replace(descriptor);

            return this;
        }
    }
}