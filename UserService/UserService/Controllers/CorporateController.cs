using Microsoft.AspNetCore.Mvc;
using UserService.DTOs.CorporateDTOs;
using UserService.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/corporate")]
    public class CorporateController : ControllerBase
    {
        private readonly ICorporateRepository _repository;

        public CorporateController(ICorporateRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all Corporates
        /// </summary>
        /// <returns>List of Corporates</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public ActionResult GetAll()
        {
            var entities = _repository.Get();

            if (entities.Count == 0)
                return NoContent();

            return Ok(entities);
        }

        /// <summary>
        /// Get Corporate by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Corporate</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var entity = _repository.Get(id);

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Create new corporate
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>New Corporate</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public ActionResult PostCorporate([FromBody] CorporateCreateDto dto)
        {
            var entity = _repository.Create(dto);

            return Ok(entity);
        }

        /// <summary>
        /// Update corporate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Updated Corporate</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        public ActionResult PutCorporate(int id, CorporateCreateDto dto)
        {
            var entity = _repository.Update(id, dto);

            return Ok(entity);
        }

        /// <summary>
        /// Delete corporate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public ActionResult DeleteCorporate(int id)
        {
            _repository.Delete(id);

            return NoContent();
        }
    }
}
