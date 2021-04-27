using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeatureSwitches.Models;

namespace FeatureSwitches.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class featureController : ControllerBase
    {
        private readonly FeatureDBContext _context;

        public featureController(FeatureDBContext context)
        {
            _context = context;
        }

        // GET: /feature?email=XXX&featureName=XXX
        [HttpGet]
        public async Task<ActionResult<clsCanAccess>> GetFeatureAccess(string email, string featureName)
        {
            var fa = await _context.FeatureAccesses.Where(x => x.Email == email && x.FeatureName == featureName).ToListAsync();

            if (fa == null)
            {
                return NotFound();
            }
            if (fa.Count >= 1)
            {
                if (fa.FirstOrDefault().Enable == true)
                    return new clsCanAccess { canAccess = true };
                else
                    return new clsCanAccess { canAccess = false };
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<FeatureAccess>> PostFeatureAccess(FeatureAccess featureAccess)
        {
            try
            {
                _context.FeatureAccesses.Add(featureAccess);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("Cannot insert duplicate key"))
                {
                    return StatusCode(304);
                }
            }
            return Ok();
        }
    }
}
