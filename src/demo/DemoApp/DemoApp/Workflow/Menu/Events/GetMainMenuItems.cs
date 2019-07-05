using System.Collections.Generic;
using MediatR;

namespace DemoApp.Workflow.Menu.Events
{
    public class GetMainMenuItems : IRequest<IEnumerable<string>>
    {
    }
}