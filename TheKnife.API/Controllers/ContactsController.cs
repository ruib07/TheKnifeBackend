using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsService _contactsService;

        public ContactsController(ContactsService contactsService)
        {
            _contactsService=contactsService;
        }

        // GET api/contacts
        [HttpGet]
        [ProducesResponseType(typeof(List<ContactsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ContactsEfo>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<ContactsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ContactsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ContactsEfo>>> GetAllContactsAsync()
        {
            List<ContactsEfo> contacts = await _contactsService.GetAllContactsAsync();

            if (contacts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, contacts);
        }

        // GET api/comments/{id}
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ContactsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetContactByIdAsync(int id)
        {
            ContactsEfo contact = await _contactsService.GetContactByIdAsync(id);

            if (contact == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, contact);
        }

        // POST api/comments
        [HttpPost]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CommentsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ContactsEfo>> SendContactAsync([FromBody, Required] ContactsEfo contact)
        {
            if (ModelState.IsValid)
            {
                ContactsEfo newContact = await _contactsService.SendContactAsync(contact);
                return StatusCode(StatusCodes.Status201Created, newContact);
            }

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        // DELETE api/comments/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteContactAsync(int id)
        {
            try
            {
                await _contactsService.DeleteContactAsync(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Internal server error: {ex.Message}");
            }
        }
    }
}
