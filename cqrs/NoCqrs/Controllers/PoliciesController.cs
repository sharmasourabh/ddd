using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NoCqrs.Services;

namespace NoCqrs.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]/")]
  public class PoliciesController : ControllerBase
  {
    private readonly PolicyService policyService;

    public PoliciesController(PolicyService policyService)
    {
      this.policyService = policyService;
    }

    /*[HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
      return new string[] { "value1", "value2" };
    }*/

    [HttpPost]
    public IActionResult CreatePolicy([FromBody] CreatePolicyRequest request)
    {
      var result = policyService.CreatePolicy(request);
      return Ok(result);
    }

    [HttpPost("BuyAdditionalCover")]
    public IActionResult BuyAdditionalCover([FromBody] BuyAdditionalCoverRequest request)
    {
      var result = policyService.BuyAdditionalCover(request);
      return Ok(result);
    }

    [HttpPost("ConfirmBuyAdditionalCover")]
    public IActionResult Post([FromBody] ConfirmBuyAdditionalCoverRequest request)
    {
      var result = policyService.ConfirmBuyAdditionalCover(request);
      return Ok(result);
    }

    [HttpPost("Terminate")]
    public IActionResult Terminate([FromBody] TerminatePolicyRequest request)
    {
      var result = policyService.TerminatePolicy(request);
      return Ok(result);
    }

    [HttpPost("ConfirmTermination")]
    public IActionResult ConfirmTermination([FromBody] ConfirmTerminationRequest request)
    {
      var result = policyService.ConfirmTermination(request);
      return Ok(result);
    }

    [HttpPost("CancelLastAnnex")]
    public IActionResult CancelLastAnnex([FromBody] CancelLastAnnexRequest request)
    {
      var result = policyService.CancelLastAnnex(request);
      return Ok(result);
    }

    [HttpGet("{policyNumber}")]
    public IActionResult GetPolicyDetails([FromRoute] string policyNumber)
    {
      var result = policyService.GetPolicyDetails(policyNumber);
      return Ok(result);
    }


    [HttpPost("find")]
    public IActionResult Find([FromBody] SearchPolicyRequest searchPolicyRequest)
    {
      var result = policyService.SearchPolicies(searchPolicyRequest);
      return Ok(result);
    }
  }
}