
using EMU.DT.DataService.Data;
using EMU.DT.Shared.DTOs;
using EMU.DT.Shared.Extensions;
using EMU.DT.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMU.DT.DataService.Controllers;

[ApiController]
[Route("api/data/[controller]")]
public class EmusController : ControllerBase
{
    private readonly DataDbContext _context;

    public EmusController(DataDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task&lt;ActionResult&lt;BaseResponse&lt;IEnumerable&lt;EmuDto&gt;&gt;&gt;&gt; GetEmus()
    {
        var emus = await _context.Emus.ToListAsync();
        var emuDtos = emus.Select(e =&gt; e.ToDto());
        return Ok(BaseResponse&lt;IEnumerable&lt;EmuDto&gt;&gt;.Success(emuDtos));
    }

    [HttpGet("{id}")]
    public async Task&lt;ActionResult&lt;BaseResponse&lt;EmuDto&gt;&gt;&gt; GetEmu(long id)
    {
        var emu = await _context.Emus.FindAsync(id);
        if (emu == null)
        {
            return NotFound(BaseResponse&lt;EmuDto&gt;.Fail("动车组不存在"));
        }
        return Ok(BaseResponse&lt;EmuDto&gt;.Success(emu.ToDto()));
    }

    [HttpPut("{id}")]
    public async Task&lt;IActionResult&gt; PutEmu(long id, EmuInfo emu)
    {
        if (id != emu.Id)
        {
            return BadRequest();
        }
        _context.Entry(emu).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmuExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    [HttpPost]
    public async Task&lt;ActionResult&lt;EmuInfo&gt;&gt; PostEmu(EmuInfo emu)
    {
        emu.Id = EMU.DT.Shared.Utils.IdGenerator.NextId();
        emu.CreatedAt = DateTime.UtcNow;
        _context.Emus.Add(emu);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetEmu", new { id = emu.Id }, emu);
    }

    [HttpDelete("{id}")]
    public async Task&lt;IActionResult&gt; DeleteEmu(long id)
    {
        var emu = await _context.Emus.FindAsync(id);
        if (emu == null)
        {
            return NotFound();
        }
        _context.Emus.Remove(emu);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool EmuExists(long id)
    {
        return _context.Emus.Any(e =&gt; e.Id == id);
    }
}
