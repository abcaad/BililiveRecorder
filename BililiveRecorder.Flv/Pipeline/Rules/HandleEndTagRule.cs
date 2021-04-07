using System;
using System.Collections.Generic;

namespace BililiveRecorder.Flv.Pipeline.Rules
{
    /// <summary>
    /// 处理 end tag
    /// </summary>
    public class HandleEndTagRule : ISimpleProcessingRule
    {
        private static readonly ProcessingComment comment = new ProcessingComment(CommentType.Logging, "因收到 End Tag 分段");

        public void Run(FlvProcessingContext context, Action next)
        {
            context.PerActionRun(this.RunPerAction);
            next();
        }

        private IEnumerable<PipelineAction?> RunPerAction(FlvProcessingContext context, PipelineAction action)
        {
            yield return action;
            if (action is PipelineEndAction)
            {
                context.AddComment(comment);
                yield return PipelineNewFileAction.Instance;
            }
        }
    }
}
