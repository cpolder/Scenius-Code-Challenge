namespace Company.Consumers
{
    using MassTransit;
    using Scenius.CodeTest.Consumer.Consumers;

    public class ConsumerConsumerDefinition :
        ConsumerDefinition<PostCalculationConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PostCalculationConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}