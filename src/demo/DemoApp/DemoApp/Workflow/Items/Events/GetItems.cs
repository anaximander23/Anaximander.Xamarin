using System.Collections.Generic;
using DemoApp.Models;
using MediatR;

namespace DemoApp.Workflow.Items.Events
{
    public class GetItems : IRequest<IEnumerable<Item>>
    {
    }
}