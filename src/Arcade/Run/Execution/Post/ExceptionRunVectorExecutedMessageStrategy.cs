using System;
using Arcade.Run.Execution.Events;
using Arcade.Run.Execution.Messages;

namespace Arcade.Run.Execution.Post
{
    public sealed class ExceptionRunVectorExecutedMessageStrategy : IRunVectorExecutedMessageStrategy
    {
        public bool CanTreatRunVectorExecutedMessage(IRunVectorExecutedMessage runVectorExecutedMessage)
        {
            return runVectorExecutedMessage is ExceptionRunVectorExecutedMessage;
        }        
        
        public void TreatRunVectorExecutedMessage(IRunVectorExecutedMessage runVectorExecutedMessage, IFlowStackOrchestrator orchestrator)
        {
            var message = runVectorExecutedMessage as ExceptionRunVectorExecutedMessage;
            
            if(message == null) throw new InvalidOperationException("Unable to treat this type of RunVectorExecutedMessage. Check CanTreatRunVectorExecutedMessage first!");

            orchestrator.PropagateEvent(new FlowFailedEvent(message.RunId, message.CorrelationId, message.CausativeInput, message.Exception));
        }
    }
}