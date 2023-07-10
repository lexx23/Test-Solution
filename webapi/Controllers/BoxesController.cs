using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class BoxesController : ControllerBase
{
    private readonly IBoxProvider _boxProvider;
    private readonly IBoxManager _boxManager;
    
    public BoxesController(IBoxProvider boxProvider, IBoxManager boxManager)
    {
        _boxProvider = boxProvider;
        _boxManager = boxManager;
    }

    [HttpGet]
    public async Task<IEnumerable<Box.Core.Box>> Get(CancellationToken token)
    {
        var boxes = await _boxProvider.GetListAsync(token);
        return boxes;
    }
    
    
    [HttpPost]
    public async Task<Box.Core.Box> Add([FromBody] Box.Web.BoxCreate box, CancellationToken token)
    {
        var result = new Box.Core.Box
        {
            Color = box.Color
        };

        await _boxManager.AddAsync(result, token);

        return result;
    }
    
    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] string id, CancellationToken token)
    {
        await _boxManager.DeleteAsync(id, token);
    }
}
