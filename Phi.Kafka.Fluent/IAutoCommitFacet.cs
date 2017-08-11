using System;
namespace Phi.Kafka.Fluent
{
    public interface IAutoCommitFacet
    {
        /// <summary>
        /// Disable Auto Commit.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Don't call this unless you mean it, since you can't "unconfigure" it without calling <see cref="IConsumerFacet{TKey, TValue}.SetAutoCommit(Action{IAutoCommitFacet})"/> again.</remarks>
        IAutoCommitFacet Disable();
        /// <summary>
        /// Set Auto Commit Interval
        /// </summary>
        /// <param name="interval">The interval between automatic commits.</param>
        /// <returns></returns>
        IAutoCommitFacet SetInterval(TimeSpan interval);
    }
}
