using System.Collections.Generic;
using Arbel.ReSharper.ConfigureAwaitPlugin.DaemonStage;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Bulbs;
using JetBrains.ReSharper.Feature.Services.Intentions;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.Util;

namespace Arbel.ReSharper.ConfigureAwaitPlugin.QuickFix
{
    [QuickFix]
    public sealed class ConsiderUsingConfigureAwaitQuickFix : IQuickFix
    {
        private readonly ConsiderUsingConfigureAwaitHighlighting _highlighting;

        public ConsiderUsingConfigureAwaitQuickFix([NotNull] ConsiderUsingConfigureAwaitHighlighting highlighting)
        {
            _highlighting = highlighting;
        }

        public IEnumerable<IntentionAction> CreateBulbItems()
        {
            return new[]
            {
                new ConsiderUsingConfigureAwaitBulbItem(_highlighting.Expression, false),
                new ConsiderUsingConfigureAwaitBulbItem(_highlighting.Expression, true),
            }.ToQuickFixIntentions();
        }

        public bool IsAvailable(IUserDataHolder cache)
        {
            return _highlighting.IsValid();
        }

    }
}