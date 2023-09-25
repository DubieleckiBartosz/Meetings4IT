using Notifications.Core.Domain.Templates;
using Notifications.Core.Domain.Templates.ValueTypes;

namespace Notifications.Core.Interfaces.Repositories;

public interface ITemplateRepository
{
    Task<Template?> TemplateByTypeAsync(TemplateType type, CancellationToken cancellationToken = default);
}