using AutoMapper;
using Cloupard.Application.Products.Commands.CreateProduct;
using Cloupard.Application.Products.Commands.DeleteProduct;
using Cloupard.Application.Products.Commands.UpdateProduct;
using Cloupard.Application.Products.Queries.GetProduct;
using Cloupard.Application.Products.Queries.GetProductList;
using Cloupard.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloupard.WebApi.Controllers;

[Authorize]
public class ProductController : BaseController
{
    private readonly IMapper _mapper;

    public ProductController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProductListVm>> GetAll()
    {
        var vm = await Mediator.Send(new GetProductListQuery());
        
        return Ok(vm);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetProductVm>> Get(Guid id)
    {
        var query = new GetProductQuery(id);
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto createProductDto)
    {
        var command = _mapper.Map<CreateProductCommand>(createProductDto);
        var productId = await Mediator.Send(command);
        return Ok(productId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto updateProductDto)
    {
        var command = _mapper.Map<UpdateProductCommand>(updateProductDto);
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteProductCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}