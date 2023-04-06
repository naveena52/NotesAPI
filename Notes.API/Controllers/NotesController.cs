using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.API.Data;
using Notes.API.DTO;
using Notes.API.Models.DomainModel;
using Notes.API.Models.DTO;

namespace Notes.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext dbContext;
        public NotesController(NotesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult AddNote(AddNoteRequest addNoteRequest)
        {
            var note = new Models.DomainModel.Note
            {
                Title = addNoteRequest.Title,
                Description = addNoteRequest.Description,
                ColorHex = addNoteRequest.ColorHex,
                DateCreated = DateTime.Now
            };
            dbContext.Notes.Add(note);
            dbContext.SaveChanges();
            return Ok(note);
        }
        [HttpGet]
        public IActionResult GetAllNotes()
        {
            var notes = dbContext.Notes.ToList();
            var notesDTO = new List<Models.DTO.Note>();
            foreach (var note in notes) 
            {
                notesDTO.Add(new Models.DTO.Note
                {
                    Id = note.Id,
                    Title = note.Title,
                    Description = note.Description,
                    ColorHex = note.ColorHex,
                });
            }
            return Ok(notesDTO);    
        }
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id:Guid}")]
        public IActionResult GetNoteById(Guid id)
        {
            var noteDomainObject = dbContext.Notes.Find(id);
            if(noteDomainObject != null)
            {
                var noteDTO = new Models.DTO.Note
                {
                    Id = noteDomainObject.Id,
                    Title = noteDomainObject.Title,
                    Description = noteDomainObject.Description,
                    ColorHex = noteDomainObject.ColorHex,
                };
                return Ok(noteDTO);
            }
            return BadRequest();
        }
        [HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("{id:Guid}")]
        public IActionResult UpdateNote(Guid id,UpdateNotRequest updateNoteRequest)
        {
            var existingNote = dbContext.Notes.Find(id);
            if(existingNote != null)
            {
                existingNote.Title = updateNoteRequest.Title;
                existingNote.Description = updateNoteRequest.Description;
                existingNote.ColorHex = updateNoteRequest.ColorHex;
                dbContext.SaveChanges();
                return Ok(existingNote);
            }
            return BadRequest();

        }
        [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("{id:Guid}")]
        public IActionResult DeleteNote(Guid id)
        {

            var existingNote = dbContext.Notes.Find(id);
            if (existingNote != null)
            {
                dbContext.Notes.Remove(existingNote);
                dbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
