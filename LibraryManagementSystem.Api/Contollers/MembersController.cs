using LibraryManagementSystem.Application.DTOs.Member;
using LibraryManagementSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _memberService.GetAllAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _memberService.GetByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MemberCreateUpdateDto dto)
        {
            await _memberService.AddMemberAsync(dto);

            return CreatedAtAction(nameof(GetAll), null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MemberCreateUpdateDto dto)
        {
            await _memberService.UpdateMemberAsync(id, dto);
            return Ok();
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _memberService.DeactivateMemberAsync(id);
            return Ok();
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            await _memberService.ActivateMemberAsync(id);
            return Ok();
        }
    }
}
