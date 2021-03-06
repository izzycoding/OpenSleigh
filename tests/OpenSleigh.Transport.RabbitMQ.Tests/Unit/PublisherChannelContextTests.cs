using System;
using NSubstitute;
using RabbitMQ.Client;
using Xunit;

namespace OpenSleigh.Transport.RabbitMQ.Tests.Unit
{
    public class PublisherChannelContextTests
    {
        [Fact]
        public void ctor_should_throw_if_arguments_null()
        {
            var pool = NSubstitute.Substitute.For<IPublisherChannelContextPool>();
            var channel = NSubstitute.Substitute.For<IModel>();
            var references = new QueueReferences("exchange", "queue", "deadletterExch", "deadLetterQ");

            Assert.Throws<ArgumentNullException>(() => new PublisherChannelContext(null, references, pool));
            Assert.Throws<ArgumentNullException>(() => new PublisherChannelContext(channel, null, pool));
            Assert.Throws<ArgumentNullException>(() => new PublisherChannelContext(channel, references, null));
        }
        
        [Fact]
        public void Dispose_should_return_to_pool()
        {
            var pool = NSubstitute.Substitute.For<IPublisherChannelContextPool>();
            var channel = NSubstitute.Substitute.For<IModel>();
            var references = new QueueReferences("exchange", "queue", "deadletterExch", "deadLetterQ");

            var sut = new PublisherChannelContext(channel, references, pool);
            sut.Dispose();
            pool.Received(1)
                .Return(sut);
        }
    }
}